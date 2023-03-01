using System;
using System.Collections.Generic;

namespace PNW2016ProbI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            char[] delims = { ' ', ':' };

            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);
            List<house> housesPos = new List<house>();
            List<house> housesNeg = new List<house>();

            long numOfHouses = 0;
            long amountofMail = 0;

            long.TryParse(parts[0], out numOfHouses);
            long.TryParse(parts[1], out amountofMail);

            for (long i = 0; i < numOfHouses; i++)
            {
                house newHouse = new house(Console.ReadLine());

                if (newHouse.mLocation > 0)
                {
                    housesPos.Add(newHouse);
                }
                else
                {
                    newHouse.mLocation *= -1;
                    housesNeg.Add(newHouse);
                }
            }

            Console.WriteLine((findLength(housesPos, amountofMail) +
            findLength(housesNeg, amountofMail)).ToString());

//            Console.WriteLine(DateTime.Now.Subtract(start).TotalMilliseconds);
        }

        public static long findLength(List<house> theHouses, long total)
        {
            long returnValue = 0;

            theHouses.Sort(delegate(house a, house b)
            {
                if (a.mLocation < 0)
                {
                    return a.mLocation.CompareTo(b.mLocation);
                }
                else
                {
                    return b.mLocation.CompareTo(a.mLocation);
                }
            });

            long leftOver = 0;
            long lastLoc = 0;
            for (int i = 0; i < theHouses.Count; i++)
            {
                if(leftOver>0)
                {
                    long increase = Math.Min(total - leftOver, theHouses[i].mAmount);
                    leftOver += increase;
                    theHouses[i].mAmount -= increase;
                    if(leftOver == total)
                    {
                        returnValue += 2 * lastLoc;
                        leftOver = 0;
                    }
                }

                if (theHouses[i].mAmount>0)
                {
                    long complete = theHouses[i].mAmount / total;
                    theHouses[i].mAmount %= total;
                    returnValue += 2 * complete * theHouses[i].mLocation;
                }

                if ((leftOver == 0) && (theHouses[i].mAmount>0))
                {
                    lastLoc = theHouses[i].mLocation;
                    leftOver += theHouses[i].mAmount;
                    theHouses[i].mAmount = 0;
                }
            }
            if(leftOver>0)
            {
                returnValue += 2 * lastLoc;
            }

            return returnValue;
        }
    }

    public class house
    {
        public long mLocation = 0;
        public long mAmount = 0;

        public house()
        {
        }

        public house(string line)
        {
            char[] delims = { ' ', ':' };
            string[] parts = line.Split(delims);

            long.TryParse(parts[0], out mLocation);
            long.TryParse(parts[1], out mAmount);
        }

        public override string ToString()
        {
            return mLocation.ToString() + " " + mAmount.ToString();
        }
    }
}

/*
            for (long i = 0; i < theHouses.Count;i++)
            {
                if(leftOver == theHouses[i].mAmount)
                {
                    theHouses[i].mAmount = 0;
                    leftOver = 0;
                    returnValue += theHouses[i].mLocation;
                }
                else
                {
                    if(leftOver > theHouses[i].mAmount)
                    {
                        leftOver -= theHouses[i].mAmount;
                        theHouses[i].mAmount = 0;
                        returnValue += theHouses[i].mLocation;
                    }
                }

                while(theHouses[i].mAmount>0)
                {
                    if(theHouses[i].mAmount>=total)
                    {
                        returnValue += theHouses[i].mLocation * 2;
                        theHouses[i].mAmount -= total;
                    }
                    else
                    {
                        leftOver =
                    }
                }

                double numberOfVisits = (double)theHouses[i].mAmount / (double)total;
                long visitsI = (long)numberOfVisits;

                if(visitsI>0)
                {
                    returnValue += (theHouses[i].mLocation * visitsI) * 2;
                }

                if((numberOfVisits-(double)visitsI)>0)
                {
                }
            }

*/