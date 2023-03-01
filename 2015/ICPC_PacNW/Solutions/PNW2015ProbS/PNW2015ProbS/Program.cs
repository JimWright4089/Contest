using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2015ProbS
{
    class Program
    {
        static int mNumber = 0;
        static Waves[] mWaves;
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            string word = Console.ReadLine();
            int.TryParse(word, out mNumber);
            Waves temp;

            mWaves = new Waves[mNumber];
            int[] points = new int[mNumber+2];

            for (int i = 0; i < mNumber; i++)
            {
                string line = Console.ReadLine();
                mWaves[i] = new Waves(line);
                points[i] = 0;
            }

            int gap = mNumber / 2;
            while (gap > 0)
            {
                int j;
                for (int i = gap; i < mNumber; i++)
                {

                    temp = mWaves[i];
                    j = i;

                    while (j >= gap && (mWaves[j - gap].mStart > temp.mStart))
                    {
                        mWaves[j] = mWaves[j - gap];
                        j -= gap;
                    }

                    mWaves[j] = temp;
                }

                gap = gap / 2;
            }

//            int max = GetNext(0,0);
            int max = 0;

            for (int i = mNumber-1; i >= 0; i--)
            {
                int j;
                for (j = i; j < mNumber; j++)
                {
                    if(mWaves[j].mStart >= mWaves[i].mEnd)
                    {
                        break;
                    }
                }
                points[i] = Math.Max(points[i + 1], points[j] + mWaves[i].mPoints);
            }

            Console.WriteLine(points[0]);
            Console.WriteLine((DateTime.Now - start).TotalMilliseconds);
        }

        static int GetNext(int time,int startIndex)
        {
//            Console.WriteLine(">>"+time.ToString()+" "+startIndex.ToString());
            int max = 0;
            for (int i = startIndex; i < mWaves.Length;i++)
            {
                if(mWaves[i].mStart >= time)
                {
                    Console.WriteLine(">>"+time.ToString()+" "+startIndex.ToString());
                    int num = mWaves[i].mPoints + GetNext(mWaves[i].mEnd, i);
                    if(num>max)
                    {
                        max = num;
                    }
                }
            }

            return max;
        }
    }

    public class Waves
    {
        public int mStart = 0;
        public int mPoints = 0;
        public int mLength = 0;
        public int mEnd = 0;

        public Waves()
        {

        }

        public Waves(string line)
        {
            char[] delims = { ' ', '\n' };
            string[] parts = line.Split(delims);

            int.TryParse(parts[0], out mStart);
            int.TryParse(parts[1], out mPoints);
            int.TryParse(parts[2], out mLength);

            mEnd = mStart + mLength;
        }

        public override string ToString()
        {
            return mStart.ToString() + " " + mPoints.ToString() + " " +
                mLength.ToString() + " " + mEnd.ToString();
        }

    }

}
