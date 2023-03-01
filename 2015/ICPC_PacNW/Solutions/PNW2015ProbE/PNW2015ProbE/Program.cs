using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2015ProbE
{
    class Program
    {
        static void Main(string[] args)
        {
            string numberOfString = Console.ReadLine();
            int numberOfTest;
            int.TryParse(numberOfString, out numberOfTest);
            List<long> players = new List<long>();
            long maxInt = 30000000;

            for (int index = 0; index < numberOfTest; index++)
            {
                string line = Console.ReadLine();
                long number;
                long.TryParse(line, out number);
                players.Add(number);
            }

            players.Sort(
                delegate(long a, long b)
                {
                    if(a==b)
                    {
                        return 0;
                    }

                    if(a>b)
                    {
                        return 1;
                    }
                    return -1;
                }
            );

            for(int i=0;i<(players.Count/2);i++)
            {
                long number = players[i] + players[players.Count - (i + 1)];
                if(number<maxInt)
                {
                    maxInt = number;
                }
            }

            Console.WriteLine(maxInt);
        }
    }
}
