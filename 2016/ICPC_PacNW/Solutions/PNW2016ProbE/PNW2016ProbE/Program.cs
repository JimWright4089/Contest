using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2016ProbE
{
    class Program
    {
        static double CIRCLE_RADIANS = 6.283185307179586476925286766559;

        static void Main(string[] args)
        {
            char[] delims = { ' ', ':' };
            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);
            int numTrees = 0;
            int numControl = 0;
            List<Point> myTrees = new List<Point>();
            List<Point> otherTrees = new List<Point>();

            int.TryParse(parts[0], out numTrees);
            int.TryParse(parts[1], out numControl);

            long minX = long.MaxValue;
            long minY = long.MaxValue;
            long maxX = long.MinValue;
            long maxY = long.MinValue;

            for(int i=0;i<numTrees;i++)
            {
                numberOfString = Console.ReadLine();
                parts = numberOfString.Split(delims);
                long x=0;
                long y=0;

                long.TryParse(parts[0], out x);
                long.TryParse(parts[1], out y);

                if (minX > x)
                {
                    minX = x;
                }
                if (minY > y)
                {
                    minY = y;
                }
                if (maxX < x)
                {
                    maxX = x;
                }
                if (maxY < y)
                {
                    maxY = y;
                }

                Point newPoint = new Point(x,y);

                if(i<numControl)
                {
                    myTrees.Add(newPoint);
                }
                else
                {
                    otherTrees.Add(newPoint);
                }
            }

            long centX = (maxX - minX) / 2;
            long centY = (maxY - minY) / 2;

            for (int i = 0; i < otherTrees.Count; i++)
            {
                otherTrees[i].Center(centX, centY);
            }

            for (int i = 0; i < myTrees.Count; i++)
            {
                myTrees[i].Center(centX, centY);
            }

            double max = 0;

            for(int i=0;i<otherTrees.Count;i++)
            {
                List<Point> newPoints = new List<Point>();
                newPoints.Add(otherTrees[i]);
                for(int j=0;j<myTrees.Count;j++)
                {
                    newPoints.Add(myTrees[j]);
                }
                
                newPoints.Sort(
                    delegate(Point p1, Point p2)
                    {
                        return SortCornersClockwise(p1, p2);
                    }
                );
                newPoints.Add(newPoints[0]);
                
//                newPoints.Add(otherTrees[i]);

                double cur = FindArea(newPoints);
//                double cur = area(newPoints);
                if (cur > max)
                {
                    max = cur;
                }
            }
            Console.Out.WriteLine(max.ToString("f1"));
        }

        public static int SortCornersClockwise(Point A, Point B)
        {
            //  Variables to Store the atans
            double aTanA, aTanB;

            //  Reference Point
            Point reference = new Point();

            //  Fetch the atans
            aTanA = Math.Atan2(A.mY - reference.mY, A.mX - reference.mX);
            aTanB = Math.Atan2(B.mY - reference.mY, B.mX - reference.mX);

            //  Determine next point in Clockwise rotation
            if (aTanA < aTanB) return -1;
            else if (aTanB < aTanA) return 1;

            if (A.mX > B.mX)
            {
                return 1;
            }
            if (A.mX < B.mX)
            {
                return -1;
            }
            if (A.mY > B.mY)
            {
                return 1;
            }
            if (A.mY < B.mY)
            {
                return -1;
            }
            return 0;
        }



        public static int Compare(Point pointA, Point pointB)
        {
            Point CenterPoint = new Point(pointA, pointB);

            if (pointA.mX - CenterPoint.mX >= 0 && pointB.mX - CenterPoint.mX < 0)
                return 1;
            if (pointA.mX - CenterPoint.mX < 0 && pointB.mX - CenterPoint.mX >= 0)
                return -1;

            if (pointA.mX - CenterPoint.mX == 0 && pointB.mX - CenterPoint.mX == 0)
            {
                if (pointA.mY - CenterPoint.mY >= 0 || pointB.mY - CenterPoint.mY >= 0)
                    if (pointA.mY > pointB.mY)
                        return 1;
                    else return -1;
                if (pointB.mY > pointA.mY)
                    return 1;
                else return -1;
            }

            // compute the cross product of vectors (CenterPoint -> a) x (CenterPoint -> b)
            double det = (pointA.mX - CenterPoint.mX) * (pointB.mY - CenterPoint.mY) -
                             (pointB.mX - CenterPoint.mX) * (pointA.mY - CenterPoint.mY);
            if (det < 0)
                return 1;
            if (det > 0)
                return -1;

            // points a and b are on the same line from the CenterPoint
            // check which point is closer to the CenterPoint
            double d1 = (pointA.mX - CenterPoint.mX) * (pointA.mX - CenterPoint.mX) +
                            (pointA.mY - CenterPoint.mY) * (pointA.mY - CenterPoint.mY);
            double d2 = (pointB.mX - CenterPoint.mX) * (pointB.mX - CenterPoint.mX) +
                            (pointB.mY - CenterPoint.mY) * (pointB.mY - CenterPoint.mY);
            if (d1 > d2)
                return 1;
            else return -1;
        }



        static double FindArea(List<Point> points)
        {
            double area = 0;

            for (int i = 0; i < points.Count - 1;i++)
            {
                area += ((points[i].mX * points[i + 1].mY - points[i + 1].mX * points[i].mY))/2;
            }

            return area;
        }

        static double angleOf(double x, double y) 
        {
            double  dist=Math.Sqrt(x*x+y*y) ;

            if (y >= 0.0)
            {
                return Math.Acos(x / dist);
            }
            else
            {
                return Math.Acos(-x / dist) + .5 * CIRCLE_RADIANS;
            }
        }
    }

    class Point
    {
        public long mX = 0;
        public long mY = 0;

        public Point()
        {

        }

        public Point(Point a, Point b)
        {
            mX = (a.mX + b.mX) / 2;
            mY = (a.mY + b.mY) / 2;
        }

        public Point(long x, long y)
        {
            mX = x;
            mY = y;
        }

        public void Center(long x,long y)
        {
            mX -= x;
            mY -= y;
        }

        public override string ToString()
        {
            return "(" + mX.ToString() + "," + mY.ToString() + ")";
        }

    }


#if xxx0
        static bool less(Point a, Point b)
        {
            Point center = new Point(a,b);

            if (a.mX - center.mX >= 0 && b.mX - center.mX < 0)
                return true;
            if (a.mX - center.mX < 0 && b.mX - center.mX >= 0)
                return false;
            if (a.mX - center.mX == 0 && b.mX - center.mX == 0)
            {
                if (a.mY - center.mY >= 0 || b.mY - center.mY >= 0)
                    return a.mY > b.mY;
                return b.mY > a.mY;
            }

            // compute the cross product of vectors (center -> a) x (center -> b)
            int det = (a.mX - center.mX) * (b.mY - center.mY) - (b.mX - center.mX) * (a.mY - center.mY);
            if (det < 0)
                return true;
            if (det > 0)
                return false;

            // points a and b are on the same line from the center
            // check which point is closer to the center
            int d1 = (a.mX - center.mX) * (a.mX - center.mX) + (a.mY - center.mY) * (a.mY - center.mY);
            int d2 = (b.mX - center.mX) * (b.mX - center.mX) + (b.mY - center.mY) * (b.mY - center.mY);
            return d1 > d2;
        }

        static long cross(Point a, Point b, Point c)
        {
            return (b.mX - a.mX) * (c.mY - a.mY) - (b.mY - a.mY) * (c.mX - a.mX);
        }

        static long area(Point a, Point b)
        {
            return a.mX * b.mY - b.mX * a.mY;
        }

        public static List<Point> convexHull(List<Point> points) 
        {
            int n = points.Count;
            if (n <= 1)
            {
                return points;
            }

            points.Sort(
                delegate(Point a, Point b)
                {
                    return a.mX.CompareTo(b.mX) != 0 ? a.mX.CompareTo(b.mX) : a.mY.CompareTo(b.mY);
                }
            );

            Point[] h = new Point[n * 2];
            int cnt = 0;
            for (int i = 0; i < n; h[cnt++] = points[i++])
            {
                while (cnt > 1 && cross(h[cnt - 2], h[cnt - 1], points[i]) >= 0)
                {
                    --cnt;
                }
            }

            for (int i = n - 2, t = cnt; i >= 0; h[cnt++] = points[i--])
            {
                while (cnt > t && cross(h[cnt - 2], h[cnt - 1], points[i]) >= 0)
                {
                    --cnt;
                }
            }

            List<Point> newList = new List<Point>();

            for(int i=0;i<n*2;i++)
            {
                newList.Add(h[i]);
            }

            return newList;
            //return Arrays.copyOf(h, cnt - 1 - (h[0].x == h[1].x && h[0].y == h[1].y ? 1 : 0));
        }

        public static double areax(List<Point> points)
        {
            double sum = 0;
            double prevcolat = 0;
            double prevaz = 0;
            double colat0 = 0;
            double az0 = 0;
            for (int i = 0; i < points.Count; i++)
            {
                double colat = 2 * Math.Atan2(
                    Math.Sqrt(Math.Pow(
                    Math.Sin(points[i].mX * Math.PI / 180 / 2), 2) +
                    Math.Cos(points[i].mX * Math.PI / 180) *
                    Math.Pow(Math.Sin(points[i].mY * Math.PI / 180 / 2), 2)),
                    Math.Sqrt(1 - Math.Pow(
                    Math.Sin(points[i].mX * Math.PI / 180 / 2), 2) -
                    Math.Cos(points[i].mX * Math.PI / 180) *
                    Math.Pow(Math.Sin(points[i].mY * Math.PI / 180 / 2), 2)));
                double az = 0;
                if (points[i].mX >= 90)
                {
                    az = 0;
                }
                else if (points[i].mX <= -90)
                {
                    az = Math.PI;
                }
                else
                {
                    az = Math.Atan2(Math.Cos(points[i].mX * Math.PI / 180) *
                        Math.Sin(points[i].mY * Math.PI / 180), Math.Sin(points[i].mX * Math.PI / 180)) % (2 * Math.PI);
                }
                if (i == 0)
                {
                    colat0 = colat;
                    az0 = az;
                }
                if (i > 0 && i < points.Count)
                {
                    sum = sum + (1 - Math.Cos(prevcolat + (colat - prevcolat) / 2)) * 
                        Math.PI * ((Math.Abs(az - prevaz) / Math.PI) - 2 * 
                        Math.Ceiling(((Math.Abs(az - prevaz) / Math.PI) - 1) / 2)) * Math.Sign(az - prevaz);
                }
                prevcolat = colat;
                prevaz = az;
            }
            sum = sum + (1 - Math.Cos(prevcolat + (colat0 - prevcolat) / 2)) * (az0 - prevaz);
            return 5.10072E14 * Math.Min(Math.Abs(sum) / 4 / Math.PI, 1 - Math.Abs(sum) / 4 / Math.PI);
        }


#endif

}
