using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2015ProbO
{
    class Program
    {
        static int MAX_MOVES = -1;

        static int[,] mMaze;
        static int[,] mMazePath;
        static int mWidth = 0;
        static int mHeight = 0;
        static int mPath = MAX_MOVES;
        static int[] dx={ 0,-1, 1, 0};
        static int[] dy={-1, 0, 0, 1};
        static bool[,] mVisit;

        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            string line = Console.ReadLine();
            char[] delims = { ' ', '\n' };
            string[] parts = line.Split(delims);
            Queue<pair> theQueue = new Queue<pair>();

            int.TryParse(parts[0], out mHeight);
            int.TryParse(parts[1], out mWidth);

            mMaze = new int[mHeight, mWidth];
            mMazePath = new int[mHeight, mWidth];

            for (int i = 0; i < mHeight; i++)
            {
                line = Console.ReadLine();

                for (int j = 0; j < mWidth; j++)
                {                     
                    char ch = line[j];
                    mMaze[i, j] = ch - '0';
                    mMazePath[i, j] = MAX_MOVES;
                }
            }

            theQueue.Enqueue(new pair(0, 0));
            mMazePath[0, 0] = 0;
            while(theQueue.Count>0)
            {
                int newX = 0;
                int newY = 0;

                pair newPair = theQueue.Dequeue();
                int length = mMaze[newPair.mY, newPair.mX];
                Console.WriteLine(newPair + " " + length.ToString("d3") + " " + mMazePath[newPair.mY, newPair.mX].ToString("d3"));
                for (int i = 0; i < 4; i++)
                {
                    newX = newPair.mX+(dx[i]*length);
                    newY = newPair.mY+(dy[i]*length);
                    if((false == isOutside(newX, newY))&&(mMazePath[newY,newX] == MAX_MOVES))
                    {
                        mMazePath[newY, newX] = mMazePath[newPair.mY, newPair.mX] + 1;
                        theQueue.Enqueue(new pair(newX, newY));
                    }
                }
            }

            if (MAX_MOVES == mMazePath[mHeight-1, mWidth-1])
            {
                Console.WriteLine("No Path");
            }
            else
            {
                Console.WriteLine(mMazePath[mHeight - 1, mWidth - 1]);
            }

            /*
            for (int i = 0; i < mHeight;i++)
            {
                for(int j=0;j<mWidth;j++)
                {
                    Console.Write(mMazePath[i, j].ToString("D3")+" ");
                }
                Console.WriteLine(" ");
            }
            */
            Console.WriteLine((DateTime.Now - start).TotalMilliseconds);
        }

        static void MainX(string[] args)
        {
            DateTime start = DateTime.Now;
            string line = Console.ReadLine();
            char[] delims = {' ','\n'};
            string[] parts = line.Split(delims);

            int.TryParse(parts[0], out mHeight);
            int.TryParse(parts[1], out mWidth);

            mMaze = new int[mHeight,mWidth];
            mMazePath = new int[mHeight, mWidth];
            mVisit = new bool[mHeight, mWidth];

            for(int i=0;i<mHeight;i++)
            {
                line = Console.ReadLine();

                for (int j = 0; j < mWidth; j++)
                {
                    char ch = line[j];
                    mMaze[i, j] = ch - '0';
                    mMazePath[i, j] = MAX_MOVES;
                    mVisit[i, j] = false;
                }
            }

            mVisit[0, 0] = true;
            FindPath(0, 0, 0);

            if (MAX_MOVES == mPath)
            {
                Console.WriteLine("No Path");
            }
            else
            {
                Console.WriteLine(mPath);
            }
            Console.WriteLine((DateTime.Now - start).TotalMilliseconds);
        }

        static void FindPath(int x, int y, int path)
        {
            if (path < mPath)
            {

                if ((x == (mWidth - 1)) && (y == (mHeight - 1)))
                {
                    if (path < mPath)
                    {
                        mPath = path;
                        return;
                    }
                }

                int moves = mMaze[y, x];
                int newX = x;
                int newY = y;

                if ((0 != moves) && (mMazePath[y, x] > path))
                {
                    mMazePath[y, x] = path;
                    for (int i = 0; i < 4; i++)
                    {
                        switch (i)
                        {
                            case (0):
                                newX = x + moves;
                                newY = y;
                                break;
                            case (1):
                                newX = x;
                                newY = y + moves;
                                break;
                            case (2):
                                newX = x - moves;
                                newY = y;
                                break;
                            case (3):
                                newX = x;
                                newY = y - moves;
                                break;
                        }

                        if (false == isOutside(newX, newY))
                        {
                            mVisit[newY, newX] = true;
                            FindPath(newX, newY, path + 1);
                            mVisit[newY, newX] = false;
                        }
                    }
                }
            }
        }

        static bool isOutside(int x, int y)
        {
            if (x < 0)
            {
                return true;
            }
            if (y < 0)
            {
                return true;
            }
            if (x >= mWidth)
            {
                return true;
            }
            if (y >= mHeight)
            {
                return true;
            }

            return false;
        }

        static bool isOutsideX(int x, int y)
        {
            if (x < 0)
            {
                return true;
            }
            if (y < 0)
            {
                return true;
            }
            if (x >= mWidth)
            {
                return true;
            }
            if (y >= mHeight)
            {
                return true;
            }

            if (true == mVisit[y, x])
            {
                return true;
            }

            return false;
        }
    }

    public class pair
    {
        public int mX = 0;
        public int mY = 0;

        public pair()
        {

        }

        public pair(int x, int y)
        {
            mX = x;
            mY = y;
        }

        public override string ToString()
        {
            return "("+mX.ToString("d3") + " " + mY.ToString("d3")+")";
        }
    }
}
