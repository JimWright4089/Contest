using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2016ProbQ
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] delims = { ' ', ':' };
            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);
            long total = 0;
            long number = 0;

            long.TryParse(parts[0], out total);
            long.TryParse(parts[1], out number);
            long[] times = new long[total];
            long[] curTimes = new long[number];
            int loc = 0;

            for(int i=0;i<number;i++)
            {
                numberOfString = Console.ReadLine();
                long.TryParse(numberOfString, out curTimes[i]);
            }

            for(int i=0;i<total-number;i++)
            {
                long smallest = long.MaxValue;
                long smallIndex = 0;
                for(int j=0;j<number;j++)
                {
                    if(-1 != curTimes[j])
                    {
                        if(smallest > curTimes[j])
                        {
                            smallest = curTimes[j];
                            smallIndex = j;
                        }
                    }
                }
                times[loc] = smallest;

                loc++;
                numberOfString = Console.ReadLine();
                long.TryParse(numberOfString, out curTimes[smallIndex]);
            }

            for (int i = 0; i < number; i++)
            {
                long smallest = long.MaxValue;
                long smallIndex = 0;
                for (int j = 0; j < number; j++)
                {
                    if (-1 != curTimes[j])
                    {
                        if (smallest > curTimes[j])
                        {
                            smallest = curTimes[j];
                            smallIndex = j;
                        }
                    }
                }
                times[loc] = smallest;
                loc++;
                curTimes[smallIndex] = -1;
            }

            long theTime = 0;
            for (int i = 0; i < total; i++)
            {
                times[i] += theTime;
                theTime = times[i];
            }

            long sumTotal = 0;

            for (int i = 0; i < total; i++)
            {
                sumTotal += times[i];
            }
            Console.Out.WriteLine(sumTotal);
        }
    }
}
