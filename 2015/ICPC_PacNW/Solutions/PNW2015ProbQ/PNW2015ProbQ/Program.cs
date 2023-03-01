using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2015ProbQ
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            int number = 0;
            int temp;
            string word = Console.ReadLine();
            int.TryParse(word, out number);
            int[] teams = new int[number];

            for(int i=0;i<number;i++)
            {
                string line = Console.ReadLine();
                int.TryParse(line, out teams[i]);
            }

/*
            for (int i = teams.Length-1; i >= 0;i-- )
            {
                for(int j=0;j<i;j++)
                {
                    if (teams[j] < teams[j + 1])
                    {
                        temp = teams[j + 1];
                        teams[j + 1] = teams[j];
                        teams[j] = temp;
                    }
                }
            }
*/

            int gap = number / 2;
            while(gap > 0)
            {
                int j;
                for(int i = gap; i < number; i++) 
                {
 
                    temp = teams[i];
                    j = i;
 
                    while(j >= gap && teams[j - gap] > temp) 
                    {
                        teams[j] = teams[j - gap];
                        j -= gap;
                    }
 
                    teams[j] = temp;
                }
 
                gap = gap/2;
            }

            int smallest = 400000000;

            for (int i = 0; i < number / 2;i++ )
            {
                int numb = teams[i]+teams[number-i-1];
                if(smallest > numb)
                {
                    smallest = numb;
                }
            }

            Console.WriteLine(smallest);
            Console.WriteLine((DateTime.Now - start).TotalMilliseconds);
        }
    }
}
