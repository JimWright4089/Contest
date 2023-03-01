using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2016ProbU
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            long total = 0;
            long big = 0;
            long sum = 0;
            string line = Console.ReadLine();
            char[] delim = { ' ' };
            int.TryParse(line, out count);

            long[] scks = new long[count];

            for (int i = 0; i < count;i++)
            {
                line = Console.ReadLine();
                long.TryParse(line, out scks[i]);
                sum += scks[i];
                big = Math.Max(big, scks[i]);
            }

            bool done = false;

            while(false == done)
            {
                done = true;
                long big1 = 0;
                int big1i = 0;
                long sum2 = 0;

                for (int i = 0; i < count; i++)
                {
                    sum2 += scks[i];
                    if (big1 < scks[i])
                    {
                        big1 = scks[i];
                        big1i = i;
                    }
                }

                if(sum2 != scks[big1i])
                {
                    int j = (big1i + 1) % count;
                    done = false;
                    while(scks[big1i]>0)
                    {
                        if(scks[j]>0)
                        {
                            total++;
                            scks[j]--;
                            scks[big1i]--;
                            sum2 -= 2;
                        }

                        if (scks[big1i] == sum2)
                        {
                            break;
                        }

                        j = (j + 1) % count;
                        if(j==big1i)
                        {
                            j = (j + 1) % count;
                        }
                    }
                }
            }
            Console.WriteLine(total);
        }

        static void Mainx(string[] args)
        {
            long count = 0;
            long total = 0;
            long big = 0;
            long sum = 0;
            string line = Console.ReadLine();
            char[] delim = { ' ' };
            long.TryParse(line, out count);

            long[] scks = new long[count];

            for (int i = 0; i < count; i++)
            {
                line = Console.ReadLine();
                long.TryParse(line, out scks[i]);
                sum += scks[i];
                big = Math.Max(big, scks[i]);
            }

            bool done = false;

            while (false == done)
            {
                done = true;
                long big1 = 0;
                int big1i = 0;
                long big2 = 0;
                int big2i = 0;

                for (int i = 0; i < count; i++)
                {
                    if (big1 < scks[i])
                    {
                        if (big1 > big2)
                        {
                            big2 = big1;
                            big2i = big1i;
                        }
                        big1 = scks[i];
                        big1i = i;
                    }
                    else
                    {
                        if (big2 < scks[i])
                        {
                            big2 = scks[i];
                            big2i = i;
                        }
                    }
                }

                if (big2 > 0)
                {
                    done = false;

                    total += big2;
                    scks[big2i] -= big2;
                    scks[big1i] -= big2;
                }
            }
            Console.WriteLine(total);
        }


    }
}
