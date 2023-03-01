using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2015ProbC
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] delims = { ' ',':'};
            List<person> mPeople = new List<person>();

            string numberOfString = Console.ReadLine();
            int numberOfTest;
            int.TryParse(numberOfString, out numberOfTest);

            for (int index = 0; index < numberOfTest; index++)
            {
                string line = Console.ReadLine();
                string[] parts = line.Split(delims);
                person newPerson = new person();

                newPerson.mName = parts[0];

                for(int i=0;i<(parts.Length - 3);i++)
                {
                    newPerson.add(parts[i+2]);
                }
                if(newPerson.mLoc > person.mMaxLoc)
                {
                    person.mMaxLoc = newPerson.mLoc;
                }

                mPeople.Add(newPerson);
            }

            for(int i=0;i<mPeople.Count;i++)
            {
                mPeople[i].update();
            }

            mPeople.Sort(
                delegate(person a, person b)
                {
                    if (a.mSort == b.mSort)
                    {
                        return a.mName.CompareTo(b.mName);
                    }
                    else
                    {
                        return b.mSort.CompareTo(a.mSort);
                    }
                }
                );

            for (int i = 0; i < mPeople.Count; i++)
            {
                Console.WriteLine(mPeople[i].mName);
            }
        }
    }

    class person
    {
        public const int MAX_VALUES = 50;
        public const int UPPER = 1;
        public const int MIDDLE = 0;
        public const int LOWER = -1;

        public static int mMaxLoc = -1;

        public string mName = "";
        public int[] mValue;
        public int mLoc = 0;
        public string mSort = "";

        public person()
        {
            mValue = new int[MAX_VALUES];
        }

        public void add(string value)
        {
            if("upper" == value)
            {
                mValue[mLoc] = UPPER;
            }
            else
            {
                if ("lower" == value)
                {
                    mValue[mLoc] = LOWER;
                }
                else
                {
                    mValue[mLoc] = MIDDLE;
                }
            }
            mLoc++;
        }

        public void update()
        {
            int adj = (mMaxLoc - mLoc);
            for(int j=mLoc-1;j>=0;j--)
            {
                mValue[adj + j] = mValue[j];
            }

            for(int i=0;i<adj;i++)
            {
                mValue[i] = MIDDLE;
            }

            mLoc = mMaxLoc;

            for(int i=(mLoc-1);i>=0;i--)
            {
                mSort += (char)('b' + mValue[i]);
            }
        }
    }
}
