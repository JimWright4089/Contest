using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2016ProbW
{
    class Program
    {
        private static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            char[] delims = { ' ', ':' };
            int[] d1 = new int[6];
            int[] d2 = new int[6];

            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);

            for(int i=0;i<6;i++)
            {
                int.TryParse(parts[i], out d1[i]);
            }

            numberOfString = Console.ReadLine();
            parts = numberOfString.Split(delims);

            for (int i = 0; i < 6; i++)
            {
                int.TryParse(parts[i], out d2[i]);
            }

            double total = 0;
            double d1Total = 0;
            for (int i = 0; i < 6; i++)
            {
                for(int j=0;j<6;j++)
                {
                    if(d1[i]!=d2[j])
                    {
                        total+=1.0;
                        if(d1[i]>d2[j])
                        {
                            d1Total+=1.0;
                        }
                    }
                }
            }

            double ratio = d1Total / total;


            Console.WriteLine(ratio.ToString("f5"));

//            Console.WriteLine(DateTime.Now.Subtract(start).TotalMilliseconds);
        }
    }
}
