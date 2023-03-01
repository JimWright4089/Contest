using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2015ProbH
{
    class Program
    {
        static int xy2d(int n, int x, int y)
        {
            int rx, ry, s, d = 0;
            for (s = n / 2; s > 0; s /= 2)
            {
                rx = ((x & s) > 0) ? 1 : 0;
                ry = ((y & s) > 0) ? 1 : 0;
                d += s * s * ((3 * rx) ^ ry);
                rot(s, ref x, ref y, rx, ry);
            }
            return d;
        }


        static void rot(int n, ref int x, ref int y, int rx, int ry)
        {
            if (ry == 0)
            {
                if (rx == 1)
                {
                    x = n - 1 - x;  //;
                    y = n - 1 - y;
                }

                //Swap x and y
                int t = x;
                x = y;
                y = t;
            }
        }

        static void Main(string[] args)
        {
            List<city> mCities = new List<city>();
            char[] delims = { ' ', ':' };

            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);
            int numberOfCities;
            int S;
            int S2;
            double root;
            int minX = 999999;
            int minY = 999999;
            int maxX = -1;
            int maxY = -1;

            int.TryParse(parts[0], out numberOfCities);
            int.TryParse(parts[1], out S);

            for (int index = 0; index < numberOfCities; index++)
            {
                string line = Console.ReadLine();
                string[] parts2 = line.Split(delims);
                city newCity = new city();
                newCity.name = parts2[2];
                int.TryParse(parts2[0], out newCity.x);
                int.TryParse(parts2[1], out newCity.y);

                if (newCity.x > maxX)
                {
                    maxX = newCity.x;
                }

                if (newCity.x < minX)
                {
                    minX = newCity.x;
                }

                if (newCity.y > maxY)
                {
                    maxY = newCity.y;
                }

                if (newCity.y < minY)
                {
                    minY = newCity.y;
                }

                mCities.Add(newCity);
            }

            root = Math.Sqrt(mCities.Count);
            S2 = (int)root;
            root -= (S2);
            if(0.00 != root)
            {
                S2++;
            }


            for (int i = 0; i < mCities.Count;i++)
            {
                mCities[i].x -= minX;
                mCities[i].y -= minY;
                mCities[i].d = xy2d(S2, mCities[i].x, mCities[i].y);

            }

            mCities.Sort(
                delegate(city a, city b)
                {
                    return a.d.CompareTo(b.d);
                }
                );
        }
    }

    class city
    {
        public int x=0;
        public int y=0;
        public int d = 0;
        public string name = "";

        public override string ToString()
        {
            return d.ToString() + " " + name + " [" + x.ToString() + "," + y.ToString() + "]";
        }

    }
}




namespace HilbertExtensions
{
    /// <summary>
    /// Convert between Hilbert index and N-dimensional points.
    /// 
    /// The Hilbert index is expressed as an array of transposed bits. 
    /// 
    /// Example: 5 bits for each of n=3 coordinates.
    /// 15-bit Hilbert integer = A B C D E F G H I J K L M N O is stored
    /// as its Transpose                        ^
    /// X[0] = A D G J M                    X[2]|  7
    /// X[1] = B E H K N        <------->       | /X[1]
    /// X[2] = C F I L O                   axes |/
    ///        high low                         0------> X[0]
    ///        
    /// NOTE: This algorithm is derived from work done by John Skilling and published in "Programming the Hilbert curve".
    /// (c) 2004 American Institute of Physics.
    /// 
    /// </summary>
    public static class HilbertCurveTransform
    {
        /// <summary>
        /// Convert the Hilbert index into an N-dimensional point expressed as a vector of uints.
        ///
        /// Note: In Skilling's paper, this function is named TransposetoAxes.
        /// </summary>
        /// <param name="transposedIndex">The Hilbert index stored in transposed form.</param>
        /// <param name="bits">Number of bits per coordinate.</param>
        /// <returns>Coordinate vector.</returns>
        public static uint[] HilbertAxes(this uint[] transposedIndex, int bits)
        {
            var X = (uint[])transposedIndex.Clone();
            int n = X.Length; // n: Number of dimensions
            uint N = 2U << (bits - 1), P, Q, t;
            int i;
            // Gray decode by H ^ (H/2)
            t = X[n - 1] >> 1;
            // Corrected error in Skilling's paper on the following line. The appendix had i >= 0 leading to negative array index.
            for (i = n - 1; i > 0; i--)
                X[i] ^= X[i - 1];
            X[0] ^= t;
            // Undo excess work
            for (Q = 2; Q != N; Q <<= 1)
            {
                P = Q - 1;
                for (i = n - 1; i >= 0; i--)
                    if ((X[i] & Q) != 0U)
                        X[0] ^= P; // invert
                    else
                    {
                        t = (X[0] ^ X[i]) & P;
                        X[0] ^= t;
                        X[i] ^= t;
                    }
            } // exchange
            return X;
        }

        /// <summary>
        /// Given the axes (coordinates) of a point in N-Dimensional space, find the distance to that point along the Hilbert curve.
        /// That distance will be transposed; broken into pieces and distributed into an array.
        /// 
        /// The number of dimensions is the length of the hilbertAxes array.
        ///
        /// Note: In Skilling's paper, this function is called AxestoTranspose.
        /// </summary>
        /// <param name="hilbertAxes">Point in N-space.</param>
        /// <param name="bits">Depth of the Hilbert curve. If bits is one, this is the top-level Hilbert curve.</param>
        /// <returns>The Hilbert distance (or index) as a transposed Hilbert index.</returns>
        public static uint[] HilbertIndexTransposed(this uint[] hilbertAxes, int bits)
        {
            var X = (uint[])hilbertAxes.Clone();
            var n = hilbertAxes.Length; // n: Number of dimensions
            uint M = 1U << (bits - 1), P, Q, t;
            int i;
            // Inverse undo
            for (Q = M; Q > 1; Q >>= 1)
            {
                P = Q - 1;
                for (i = 0; i < n; i++)
                    if ((X[i] & Q) != 0)
                        X[0] ^= P; // invert
                    else
                    {
                        t = (X[0] ^ X[i]) & P;
                        X[0] ^= t;
                        X[i] ^= t;
                    }
            } // exchange
            // Gray encode
            for (i = 1; i < n; i++)
                X[i] ^= X[i - 1];
            t = 0;
            for (Q = M; Q > 1; Q >>= 1)
                if ((X[n - 1] & Q) != 0)
                    t ^= Q - 1;
            for (i = 0; i < n; i++)
                X[i] ^= t;

            return X;
        }

    }
}
