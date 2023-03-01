using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2015ProbD
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] delims = { ' ' };
            double a1, b1, c1, a2, b2, c2;
            double A1, B1, C1, A2, B2, C2;

            string line1 = Console.ReadLine();
            string line2 = Console.ReadLine();
            string[] parts1 = line1.Split(delims);
            string[] parts2 = line2.Split(delims);

            double.TryParse(parts1[0], out a1);
            double.TryParse(parts1[1], out b1);
            double.TryParse(parts1[2], out c1);
            double.TryParse(parts2[0], out a2);
            double.TryParse(parts2[1], out b2);
            double.TryParse(parts2[2], out c2);

            C1 = findAngle(a1, b1, c1);
            A1 = findAngle(b1, c1, a1);
            B1 = findAngle(c1, a1, b1);

            C2 = findAngle(a2, b2, c2);
            A2 = findAngle(b2, c2, a2);
            B2 = findAngle(c2, a2, b2);

            /*
            Console.Write(a1.ToString() + " " + b1.ToString() + " " + c1.ToString() + " ");
            Console.Write(a2.ToString() + " " + b2.ToString() + " " + c2.ToString() + " ");
            Console.Write(A1.ToString() + " " + B1.ToString() + " " + C1.ToString() + " ");
            Console.Write(A2.ToString() + " " + B2.ToString() + " " + C2.ToString() + " ");
            Console.WriteLine("");
            */
            
            if (180.0 == (A1 + A2))
            {
                if (b1 == b2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (c1 == c2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (b1 == c2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (c1 == b2)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }
            if (180.0 == (B1 + B2))
            {
                if (a1 == a2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (c1 == c2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (a1 == c2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (c1 == a2)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }
            if (180.0 == (C1 + C2))
            {
                if (b1 == b2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (a1 == a2)
                {
                    Console.WriteLine("YES");
                    return;
                }

                if (a1 == b2)
                {
                    Console.WriteLine("YES");
                    return;
                }

                if (b1 == a2)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }

            if (180.0 == (A1 + B2))
            {
                if (c1 == a2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (b1 == c2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (b1 == a2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (c1 == c2)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }

            if (180.0 == (A1 + C2))
            {
                if (c1 == b2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (b1 == a2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (b1 == b2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (c1 == a2)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }

            if (180.0 == (B1 + A2))
            {
                if (c1 == b2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (a1 == c2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (a1 == b2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (c1 == c2)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }

            if (180.0 == (B1 + C2))
            {
                if (c1 == b2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (a1 == a2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (a1 == b2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (c1 == a2)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }

            if (180.0 == (C1 + A2))
            {
                if (a1 == b2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (b1 == c2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (a1 == c2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (b1 == b2)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }

            if (180.0 == (C1 + B2))
            {
                if (a1 == a2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (b1 == c2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (a1 == c2)
                {
                    Console.WriteLine("YES");
                    return;
                }
                if (b1 == a2)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }

            Console.WriteLine("NO");
        }

        static double findAngle(double a, double b, double c)
        {
            double a2 = Math.Pow(a, 2.0);
            double b2 = Math.Pow(b, 2.0);
            double c2 = Math.Pow(c, 2.0);
            double part1 = -2 * a * b;
            double results = 0.0;

            double part2 = (c2 - (a2 + b2))/part1;

            results = (Math.Acos(part2)*180.0)/Math.PI;

            return results;
        }


    }
}
