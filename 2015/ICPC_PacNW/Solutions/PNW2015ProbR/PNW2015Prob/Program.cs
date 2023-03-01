using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2015Prob
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            int number = 0;
            Name temp;
            string word = Console.ReadLine();
            int.TryParse(word, out number);
            Name[] names = new Name[number];

            for (int i = 0; i < number; i++)
            {
                string line = Console.ReadLine();
                names[i] = new Name(line);
            }

            for (int i = names.Length - 1; i >= 0; i--)
            {
                for(int j=0;j<i;j++)
                {
                    if (names[j].mLast == names[j + 1].mLast)
                    {
                        if (1 == names[j].mFirst.CompareTo(names[j + 1].mFirst))
                        {
                            temp = names[j + 1];
                            names[j + 1] = names[j];
                            names[j] = temp;
                        }
                    }

                    if (1 == names[j].mLast.CompareTo(names[j + 1].mLast))
                    {
                        temp = names[j + 1];
                        names[j + 1] = names[j];
                        names[j] = temp;
                    }
                }
            }

            for (int i = 0; i < number; i++)
            {
                Console.WriteLine(names[i].mFirst + " " +  names[i].mLast);
            }
            Console.WriteLine((DateTime.Now - start).TotalMilliseconds);
        }
    }

    public class Name
    {
        public string mFirst = "";
        public string mLast = "";

        public Name()
        { }

        public Name(string line)
        {
            char[] delims = { ' ', '\n' };
            string[] parts = line.Split(delims);

            mFirst = parts[0];
            mLast = parts[1];
        }

    }

}
