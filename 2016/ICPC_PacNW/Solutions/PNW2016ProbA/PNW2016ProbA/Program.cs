using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2016ProbA
{
    class Program
    {
        static string mLine = "";
        static int mMax = 0;

        static void Main(string[] args)
        {
            mLine = Console.ReadLine();
            
            countLetters(0, ' ', 0);

            mMax = 26 - mMax;

            Console.WriteLine(mMax);
        }

        static void countLetters(int curLoc, char curChar, int score)
        {
            for (int i = curLoc; i < mLine.Length;i++)
            {
                if(mLine[i] > curChar)
                {
                    if(score+1>mMax)
                    {
                        mMax = score+1;
                    }
                    countLetters(i + 1, mLine[i], score + 1);
                }
            }
        }
    }
}
