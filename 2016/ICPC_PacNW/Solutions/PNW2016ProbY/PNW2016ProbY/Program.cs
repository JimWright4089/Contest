using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2016ProbY
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            string line = Console.ReadLine();
            char[] delim = { ' ' };
            int.TryParse(line, out count);
            line = Console.ReadLine();
            string[] parts = line.Split(delim);
            int totalCount = 1;
            int last = 0;
            int lastDelta = 0;

            int.TryParse(parts[0], out last);

            for (int i = 1; i < count;i++ )
            {
                int cur = 0;
                int.TryParse(parts[i], out cur);

                if(last != cur)
                {
                    int delta = cur - last;

                    if ((lastDelta == 0) || ((delta * lastDelta) < 0))
                    {
                        totalCount++;
                    }

                    lastDelta = delta;
                    last = cur;
                }
            }

            Console.WriteLine(totalCount);
        }
    }
}
