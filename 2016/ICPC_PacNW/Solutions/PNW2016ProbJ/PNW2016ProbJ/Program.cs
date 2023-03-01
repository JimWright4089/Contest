using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2016ProbJ
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            char[] delim = { ' ' };
            string[] parts = line.Split(delim);
            long numOfItems = 0;
            long numOfCus = 0;
            long minCost = long.MaxValue;

            long.TryParse(parts[0], out numOfItems);
            long.TryParse(parts[1], out numOfCus);

            long[] cost = new long[numOfItems];

            line = Console.ReadLine();
            parts = line.Split(delim);

            for (long i = 0; i < numOfItems; i++)
            {
                long.TryParse(parts[i], out cost[i]);
                if(cost[i]<minCost)
                {
                    minCost = cost[i];
                }
            }

            long chunk = 512 ;
            long nchunks = (numOfItems + chunk - 1) / chunk ;

            long[] mar = new long[nchunks] ;
            for (int i=0; i<nchunks; i++) 
            {
                mar[i] = cost[i*chunk] ;
                for (long j = i * chunk + 1; j < numOfItems && j < (i + 1) * chunk; j++)
                {
                    mar[i] = Math.Min(mar[i], cost[j]) ;
                }
            }

            for (long i = 0; i < numOfCus;i++)
            {
                line = Console.ReadLine();
                parts = line.Split(delim);

                long dols = 0;
                long start = 0;
                long end = 0;

                long.TryParse(parts[0], out dols);
                long.TryParse(parts[1], out start);
                long.TryParse(parts[2], out end);
                start--;

                while ((start < end) && (dols > 0) && (dols >= minCost))
                {
                    while (start < end && (start & (chunk - 1)) == 0 && dols < mar[start / chunk])
                    {
                        start += chunk;
                    }

                    if ((start < end) && (dols >= cost[start]))
                    {
                        dols = dols % cost[start];
                    }

                    if (start > end)
                    {
                        start--;
                    }
                    else
                    {
                        start++;
                    }
                }
                /*
                if (dols >= cost[start])
                {
                    long num = dols / cost[start];
                    dols -= (num * cost[start]);
                }
                */
                Console.WriteLine(dols);
            }
        }
    }
}
