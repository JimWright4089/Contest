using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2015ProbP
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] letters = new int[26];
            string word = Console.ReadLine();
            int temp = -1;

            for (int i = 0; i < 26;i++)
            {
                letters[i] = 0;
            }

            for (int i = 0; i < word.Length; i++)
            {
                int ch = word[i];
                ch -= 'a';
                if ((ch >= 0) && (ch <= 26))
                {
                    letters[ch]++;
                }
                else
                {
                    Console.WriteLine("Bad letter:" + ch.ToString());
                }
            }

            for (int i = letters.Length-1; i >= 0;i-- )
            {
                for(int j=0;j<i;j++)
                {
                    if(letters[j] < letters[j+1])
                    {
                        temp = letters[j+1];
                        letters[j + 1] = letters[j];
                        letters[j] = temp;
                    }
                }
            }

            Console.WriteLine(word.Length - letters[0] - letters[1]);
        }
    }
}
