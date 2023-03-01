using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2017ProbI
{
    class Program
    {
        static List<House> mPosHouses = new List<House>();
        static List<House> mNegHouses = new List<House>();
        static long mLetCanCarry = 0;

        static void Main(string[] args)
        {
            char[] delims = { ' ', ':' };
            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);
            int total = 0;

            int.TryParse(parts[0], out total);
            long.TryParse(parts[1], out mLetCanCarry);


            for(int i=0;i<total;i++)
            {
                numberOfString = Console.ReadLine();
                parts = numberOfString.Split(delims);

                long loc = 0;
                long letters = 0;

                long.TryParse(parts[0], out loc);
                long.TryParse(parts[1], out letters);

                if (loc < 0)
                {
                    House newHouse = new House();
                    newHouse = new House(loc * -1, letters);
                    mNegHouses.Add(newHouse);
                }
                else
                {
                    House newHouse = new House();
                    newHouse = new House(loc, letters);
                    mPosHouses.Add(newHouse);
                }
            }

            long totalLen = FindLength(mPosHouses);
            totalLen += FindLength(mNegHouses);

            Console.Out.WriteLine(totalLen);

        }

        static long FindLength(List<House> houses)
        {
            long length = 0;
            long leftover = 0;
            bool found = true;
            while(true == found)
            {
                found = false;

                long longestLoc = -1;
                long longestLetters = 0;
                int longestIndex = 0;

                for(int i=0;i<houses.Count;i++)
                {
                    if(houses[i].mLetters>0)
                    {
                        if(longestLoc<houses[i].mLoc)
                        {
                            longestLoc = houses[i].mLoc;
                            longestLetters = houses[i].mLetters;
                            longestIndex = i;
                        }
                    }
                }

                if(-1 != longestLoc)
                {
                    found = true;
                    if(longestLetters>mLetCanCarry)
                    {
                        houses[longestIndex].mLetters -= mLetCanCarry;
                        length += longestLoc * 2;
                    }
                    else
                    {
                        length += longestLoc;
                        leftover = mLetCanCarry - houses[longestIndex].mLetters;
                        houses[longestIndex].mLetters = 0;

                        while(leftover>0)
                        {
                            long longestLocTemp = -1;
                            long longestLettersTemp = 0;
                            int longestIndexTemp = 0;

                            for (int j = 0; j < houses.Count; j++)
                            {
                                if (houses[j].mLetters > 0)
                                {
                                    if (longestLocTemp < houses[j].mLoc)
                                    {
                                        longestLocTemp = houses[j].mLoc;
                                        longestLettersTemp = houses[j].mLetters;
                                        longestIndexTemp = j;
                                    }
                                }
                            }

                            // Got back to office
                            if(-1 == longestLocTemp)
                            {
                                length += longestLoc;
                                break;
                            }

                            length += longestLoc - longestLocTemp;
                            if (houses[longestIndexTemp].mLetters > leftover)
                            {
                                houses[longestIndexTemp].mLetters -= leftover;
                                leftover = 0;
                                length += longestLocTemp;
                            }
                            else
                            {
                                leftover = leftover - houses[longestIndexTemp].mLetters;
                                houses[longestIndexTemp].mLetters = 0;
                                longestLoc = longestLocTemp;
                            }
                        }
                    }
                }

            }
            return length;
        }

    }

    class House
    {
        public long mLoc = 0;
        public long mLetters = 0;

        public House()
        {

        }

        public House(long loc, long letters)
        {
            mLoc = loc;
            mLetters = letters;
        }

    }

}
