using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2016ProbX
{
    class Program
    {
        private static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            char[] delims = { ' ', ':' };
            int[] w1 = new int[2];
            int[] w2 = new int[2];
            int[] w3 = new int[2];

            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);

            int.TryParse(parts[0], out w1[0]);
            int.TryParse(parts[1], out w1[1]);

            numberOfString = Console.ReadLine();
            parts = numberOfString.Split(delims);

            int.TryParse(parts[0], out w2[0]);
            int.TryParse(parts[1], out w2[1]);

            numberOfString = Console.ReadLine();
            parts = numberOfString.Split(delims);

            int.TryParse(parts[0], out w3[0]);
            int.TryParse(parts[1], out w3[1]);

            int xTotal = w1[0] + w2[0] + w3[0];
            int yTotal = w1[1] + w2[1] + w3[1];
            bool found = false;

            if ((w1[1] == w2[1]) && (w2[1] == w3[1]))
            {
                if(w1[1]==xTotal)
                {
                    Console.WriteLine("YES");
                    found = true;
                }
            }
            else
            {
                if ((w1[0] == w2[0]) && (w2[0] == w3[0]))
                {
                    if (w1[0] == yTotal)
                    {
                        Console.WriteLine("YES");
                        found = true;
                    }
                }
            }

            if (true == Find(w1[0], w2[0], w3[0], w1[1], w2[1], w3[1]))
            {
                Console.WriteLine("YES");
                found = true;
            }
            else
            if (true == Find(w2[0], w1[0], w3[0], w2[1], w1[1], w3[1]))
            {
                Console.WriteLine("YES");
                found = true;
            }
            else
                if (true == Find(w3[0], w2[0], w1[0], w3[1], w2[1], w1[1]))
            {
                Console.WriteLine("YES");
                found = true;
            }
                else
                    if (true == Find(w1[1], w2[0], w3[0], w1[0], w2[1], w3[1]))
            {
                Console.WriteLine("YES");
                found = true;
            }
                    else
                        if (true == Find(w2[1], w1[0], w3[0], w2[0], w1[1], w3[1]))
            {
                Console.WriteLine("YES");
                found = true;
            }
                        else
                            if (true == Find(w3[1], w2[0], w1[0], w3[0], w2[1], w1[1]))
            {
                Console.WriteLine("YES");
                found = true;
            }

            if (false == found)
            {
                Console.WriteLine("NO");
            }

            //Console.WriteLine(DateTime.Now.Subtract(start).TotalMilliseconds);
        }

        static bool Find(int x1, int x2, int x3, int y1, int y2, int y3)
        {
            if (x1 == (x2 + x3))
            {
                if (y2 == y3)
                {
                    if (x1 == (y1 + y2))
                    {
                        return true;
                    }
                }
            }
            if (y1 == (y2 + y3))
            {
                if (x2 == x3)
                {
                    if (y1 == (x1 + x2))
                    {
                        return true;
                    }
                }
            }

            if (x1 == (y2 + x3))
            {
                if (x2 == y3)
                {
                    if (x1 == (y1 + x2))
                    {
                        return true;
                    }
                }
            }
            if (y1 == (x2 + y3))
            {
                if (y2 == x3)
                {
                    if (y1 == (x1 + y2))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

    }
}
