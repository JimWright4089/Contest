using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2016ProbC
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            char[] delims = { ' ', ':' };
            int addCount = 0;

            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);
            List<int> cams = new List<int>();

            int numOfHouses = 0;
            int numOfCam = 0;
            int coverage = 0;

            int.TryParse(parts[0], out numOfHouses);
            int.TryParse(parts[1], out numOfCam);
            int.TryParse(parts[2], out coverage);

            bool[] houseCams = new bool[numOfHouses+1];

            for (int i = 0; i <= numOfCam; i++)
            {
                houseCams[i] = false;
            }

            for (int i = 0; i < numOfCam;i++)
            {
                numberOfString = Console.ReadLine();
                int num = 0;
                int.TryParse(numberOfString, out num);
                cams.Add(num);
                houseCams[num] = true;
            }

            int curCount = 0;

            for (int i = 1; i <= coverage-1; i++)
            {
                if (true == houseCams[i])
                {
                    curCount++;
                }
            }

            for (int i = 1; i <= (numOfHouses - coverage)+1; i++)
            {
                if(true == houseCams[i-1])
                {
                    curCount--;
                }

                if(true == houseCams[i+coverage-1])
                {
                    curCount++;
                }

                if (curCount == 0)
                {
                    curCount += 2;
                    addCount += 2;
                    houseCams[(i + coverage) - 1] = true;
                    houseCams[(i + coverage) - 2] = true;
                }

                if (curCount == 1)
                {
                    curCount++;
                    addCount++;
                    if(false == houseCams[(i + coverage) - 1])
                    {
                        houseCams[(i + coverage) - 1] = true;
                    }
                    else
                    {
                        houseCams[(i + coverage) - 2] = true;
                    }
                }
            }
            Console.WriteLine(addCount);
            //Console.WriteLine(DateTime.Now.Subtract(start).TotalMilliseconds);
        }

        static void Mainx(string[] args)
        {
            DateTime start = DateTime.Now;
            char[] delims = { ' ', ':' };
            int addCount = 0;

            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);
            List<int> cams = new List<int>();

            int numOfHouses = 0;
            int numOfCam = 0;
            int coverage = 0;

            int.TryParse(parts[0], out numOfHouses);
            int.TryParse(parts[1], out numOfCam);
            int.TryParse(parts[2], out coverage);

            bool[] houseCams = new bool[numOfHouses + 1];

            for (int i = 0; i <= numOfCam; i++)
            {
                houseCams[i] = false;
            }

            for (int i = 0; i < numOfCam; i++)
            {
                numberOfString = Console.ReadLine();
                int num = 0;
                int.TryParse(numberOfString, out num);
                cams.Add(num);
                houseCams[num] = true;
            }

            for (int i = 1; i <= (numOfHouses - coverage); i++)
            {
                int count = 0;
                for (int j = i; j < (i + coverage); j++)
                {
                    if (true == houseCams[j])
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    count += 2;
                    addCount += 2;
                    houseCams[(i + coverage) - 1] = true;
                    houseCams[(i + coverage) - 2] = true;
                }

                if (count == 1)
                {
                    count++;
                    addCount++;
                    if (false == houseCams[(i + coverage) - 1])
                    {
                        houseCams[(i + coverage) - 1] = true;
                    }
                    else
                    {
                        houseCams[(i + coverage) - 2] = true;
                    }
                }
            }
            Console.WriteLine(addCount);
//            Console.WriteLine(DateTime.Now.Subtract(start).TotalMilliseconds);
        }
    }
}
