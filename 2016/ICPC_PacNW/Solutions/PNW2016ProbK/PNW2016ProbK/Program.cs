using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2016ProbK
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] delims = { ' ', ':' };
            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);
            int total = 0;
            long ranked = 0;
            long totalTeams = (1 << total);

            int.TryParse(parts[0], out total);
            long.TryParse(parts[1], out ranked);

            double ans = 0.0;
            for (int round = 1; round <= total; round++) 
            {
                double cur = 1.0;
                for (int j = 1; j < (1 << round); j++) 
                {
                    cur *= totalTeams - j - ranked + 1;
                    cur /= totalTeams - j;
                }
                ans += cur;
            }
            Console.Out.WriteLine(ans.ToString("f5"));
        }
    }
}
