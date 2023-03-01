using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2016ProbL
{
    class Program
    {
        static List<Point> mPoints = new List<Point>();

        static long cross(Point a, Point b, Point c)
        {
            return (b.mX - a.mX) * (c.mY - a.mY) - (b.mY - a.mY) * (c.mX - a.mX);
        }

        static void Main(string[] args)
        {
            char[] delims = { ' ', ':' };
            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);
            long total = 0;

            long.TryParse(numberOfString, out total);

            for (long i = 0; i < total; i++)
            {
                mPoints.Add(new Point(Console.ReadLine()));
            }

            string turns = Console.ReadLine();

            bool[] marked = new bool[total];
            Point best = mPoints[0];
            int bestIdx = 0;

            for (int i = 1; i < total; i++) 
            {
                if (mPoints[i].mX < best.mX || (mPoints[i].mX == best.mX && mPoints[i].mY < best.mY)) 
                {
                    best = mPoints[i];
                    bestIdx = i;
                }
            }

            Console.Out.Write(bestIdx + 1);
            marked[bestIdx] = true;

            for (int i = 0; i < turns.Length; i++)
            {
                int sign = turns[i] == 'L' ? -1 : 1;
                Point next = null;
                int nextIdx = -1;

                for (int j = 0; j < total; j++)
                {
                    if (marked[j]) continue;
                    if (next == null || cross(best, next, mPoints[j]) * sign > 0) 
                    {
                        next = mPoints[j];
                        nextIdx = j;
                    }
                }
                Console.Out.Write(" " + (nextIdx + 1));
                marked[nextIdx] = true;
                best = next;
            }

            for (int i = 0; i < total; i++) 
            {
                if (!marked[i]) Console.Out.WriteLine(" " + (i + 1));
            }
        }


        static void Mainx(string[] args)
        {
            char[] delims = { ' ', ':' };
            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);
            long total = 0;

            long.TryParse(numberOfString, out total);

            for (long i = 0; i < total; i++)
            {
                mPoints.Add(new Point(Console.ReadLine()));
            }

            string turns = Console.ReadLine();

            Point curPoint = mPoints[0];
            curPoint.mUsed = true;

            Console.Out.Write("1 ");
            int index = 0;
            double curAngle = 0;

            for (int i = 0; i < turns.Length; i++)
            {

                if ('L' == turns[i])
                {
                    index = FindNextClockWise(curPoint, curAngle, true);
                }
                else
                {
                    index = FindNextClockWise(curPoint, curAngle, false);
                }

                curAngle = curPoint.Angle(mPoints[index]);
                curPoint = mPoints[index];
                curPoint.mUsed = true;
                Console.Out.Write((index + 1).ToString() + " ");
            }

            for (int i = 0; i < mPoints.Count; i++)
            {
                if (false == mPoints[i].mUsed)
                {
                    Console.Out.WriteLine((i + 1).ToString());
                    break;
                }
            }
            Console.Out.WriteLine("");
        }

        static int FindNextClockWise(Point curPoint, double dir, bool way)
        {
            double angle = 400;
            double maxAngle = 400;
            int index = 0;

            for (int i = 0; i < mPoints.Count; i++)
            {
                if (false == mPoints[i].mUsed)
                {
                    angle = curPoint.Angle(mPoints[i]);
                    angle += dir;
                    angle = angle % 360;
                    if(true == way)
                    {
                        angle = 360 - angle;
                    }
                    

                    if (angle < maxAngle)
                    {
                        maxAngle = angle;
                        index = i;
                    }
                }
            }
            return index;
        }
    }

    class Point
    {
        public long mX = 0;
        public long mY = 0;
        public bool mUsed = false;

        public Point()
        {

        }

        public Point(long x, long y)
        {
            mX = x;
            mY = y;
        }

        public Point(string line)
        {
            char[] delims = { ' ', ':' };
            string[] parts = line.Split(delims);

            long.TryParse(parts[0], out mX);
            long.TryParse(parts[1], out mY);
        }

        public double Angle(Point other)
        {
            double dx = other.mX - mX;
            double dy = other.mY - mY;

            double ang = Math.Atan2(dy, dx);

            ang = (ang * 180) / Math.PI;

            ang = 360-((ang + 270) % 360);

            return ang;
        }

        public override string ToString()
        {
            return "(" + mX.ToString() + "," + mY.ToString() + ")";
        }
    }
}
