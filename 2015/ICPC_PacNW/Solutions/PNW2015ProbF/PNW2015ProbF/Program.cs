using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2015ProbF
{
    class Program
    {
        static List<bstate> allb = new List<bstate>();
        static int[] rots = {4,1,4,2,2,4,4,4};
                                 // 123123123     123123123    123123123    123123123    123123123    123123123    123123123    123123123
        static string[,] items = {{ "XXX X  X ", " X XXX X ", " XXXX  X ", " XX X XX ", "XX  X  XX", "X  X  XXX", "XX  XX X ", "X  XX  XX" },
                                  { "  XXXX  X", " X XXX X ", " X XXX  X", "X  XXX  X", "  XXXXX  ", "XXXX  X  ", "  XXXX X ", " XXXX X  " },
                                  { " X  XXXXX", " X XXX X ", " X  XXXX ", " XX X XX ", "XX  X  XX", "XXX  X  X", " X XX  XX", "XX  XX  X" },
                                  { "X  XXXX  ", " X XXX X ", "X  XXX X ", " XX X XX ", "XX  X  XX", "  X  XXXX", " X XXXX  ", "  X XXXX " }};
        static char[] letters = { 'T', 'X', 'R', 'S', 'Z', 'V', '7', 'W' };
        const int MAX_LETTERS = 8;
        static string line;
        static int maxLoc = -1;

        static bool run(bstate state, ref int loc, ref bool forever)
        {
            if (true == state.done())
            {
                //Console.WriteLine("Packed");
                forever = false;
                return false;
            }

            if(loc>maxLoc)
            {
                maxLoc = loc;
            }

            int letNum = 0;
            char letter = line[loc % line.Length];
            for (int i = 0; i < MAX_LETTERS; i++)
            {
                if (letter == letters[i])
                {
                    letNum = i;
                    break;
                }
            }

            state.nextL = letNum;

            for (int i = 0; i < allb.Count;i++ )
            {
                if(state.equalss(allb[i]))
                {
                    //Console.WriteLine("Trimmed");
                    return false;
                }
            }

            if(2 == loc)
            {
                Console.WriteLine("Run:");
                state.print();
            }

            bstate newState = new bstate();
            for (int i = 0; i < 30;i++)
            {
                newState.b[i] = state.b[i];
            }
            newState.nextL = state.nextL;
            allb.Add(newState);

            for (int i = 0; i < rots[letNum]; i++)
            {
                //Console.WriteLine("Loc:" + loc.ToString() + " R:" + i);
                string pLine = items[i, letNum];
                newState = new bstate();
                for (int i1 = 0; i1 < 30; i1++)
                {
                    newState.b[i1] = state.b[i1];
                }
                newState.nextL = state.nextL;

                if (true == newState.fit(pLine))
                {
//                    Console.WriteLine("trying");
//                    state.print();
                    loc++;
                    if (true == run(newState, ref loc, ref forever))
                    {
                        return true;
                    }
                    loc--;
                }
            }
            //Console.WriteLine("Going back");
            return false;
        }


        static void Main(string[] args)
        {
            int loc = 0;
            bool forever = true;
            bstate state = new bstate();
            line = Console.ReadLine();

            run(state, ref loc, ref forever);

            if(true == forever)
            {
                Console.WriteLine("forever");
            }
            else
            {
                Console.WriteLine(maxLoc);
            }
        }
    }

    class bstate
    {
        public int nextL = -1;
        public char[] b;

        public bstate()
        {
            //   123456789012345678901234567890
            b = new char[30];
            for (int i = 0; i < 30;i++)
            {
                b[i] = ' ';
            }
        }

        public bool equalss(bstate tb)
        {
            if(tb.nextL != nextL)
            {
                return false;
            }

            for (int i = 0; i < 30; i++)
            {
                if(b[i] != tb.b[i])
                {
                    return false;
                }
            }

            return true;
        }

        public bool done()
        {
            if((b[0]!=' ')||(b[1]!=' ')||(b[2]!=' '))
            {
                return true;
            }
            return false;
        }

        public void print()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write(b[(i * 3)]);
                Console.Write(b[(i * 3) + 1]);
                Console.WriteLine(b[(i * 3) + 2]);
            }
            Console.WriteLine("");
        }

        public bool fit(string line)
        {
            int loc = -1;
            bool fit = true;
            for (int i = 0; i < 8;i++)
            {
                for(int j=0;j<9;j++)
                {
                    if(('X' == line[j])&&('X' == b[(i*3)+j]))
                    {
                        fit = false;
                        break;
                    }
                }

                if(true == fit)
                {
                    loc = i;
                }
                else
                {
                    break;
                }
            }
            if((false == fit)&&(-1 == loc))
            {
//                Console.WriteLine("Can't Fit");
                return false;
            }

            for (int j = 0; j < 9; j++)
            {
                if ('X' == line[j])
                {
                    b[(loc * 3) + j] = 'X';
                }
            }

            bool deleted = true;

            while (true == deleted)
            {
                deleted = false;
                for (int i = 9; i >= 0; i--)
                {
                    if(('X' == b[(i*3)])&&('X' == b[(i*3)+1])&&('X' == b[(i*3)+2]))
                    {
                        for(int j=i;j>=1;j--)
                        {
                            b[(j * 3)] = b[((j - 1) * 3)];
                            b[(j * 3)+1] = b[((j - 1) * 3)+1];
                            b[(j * 3)+2] = b[((j - 1) * 3)+2];
                        }
                        b[0] = ' ';
                        b[1] = ' ';
                        b[2] = ' ';
                        deleted = true;
                        break;
                    }
                }
            }

            return true;
        }
    }
}
