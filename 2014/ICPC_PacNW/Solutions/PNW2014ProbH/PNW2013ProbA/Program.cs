using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace PNW2013ProbA
{
    class shortCut
    {
        class Line
        {
            public int mLeft;
            public int mDone;
            public int mScore;
            public int mReturn;

            public Line(int left, int done, int score, int retrn)
            {
                mLeft = left;
                mDone = done;
                mScore = score;
                mReturn = retrn;
            }
        }

        List <Line> mLines = new List<Line>();

        public int find(int left, int done, int score)
        {
            for(int i =0;i<mLines.Count;i++)
            {
                if( (left == mLines[i].mLeft )&&
                    (done == mLines[i].mDone )&&
                    (score == mLines[i].mScore ))
                {
                    return mLines[i].mReturn;
                }
            }
            return -2;
        }

        public void add(int left, int done, int score, int retrn)
        {
            mLines.Add(new Line(left, done, score, retrn));
        }
    }


    class Program
    {
        static List<int> mScoring = new List<int>();
        static shortCut mShortCut = new shortCut();

        static void Main(string[] args)
        {
            string numberOfString = Console.ReadLine();
            int numberOfTest;
            int.TryParse(numberOfString,out numberOfTest);

            for(int index=0;index<numberOfTest;index++)
            {
                RunTest(index+1);
            }
        }

        static int findScore(int pushUpsLeft, int pushUpsDone, int score)
        {
            Console.WriteLine("fs:" + pushUpsLeft.ToString() + " " + pushUpsDone.ToString() + " " + score.ToString());

            int returnValue = mShortCut.find(pushUpsLeft,pushUpsDone,score);

            if(-2 != returnValue)
            {
                Console.WriteLine("rv:" + returnValue.ToString());
                return returnValue;
            }
            returnValue = -1;

            int curScore = -1;

            for(int i=0;i<mScoring.Count;i++)
            {
                int totalPushUps = mScoring[i] + pushUpsDone+score;
                if ( ( totalPushUps ) == pushUpsLeft )
                {
                    mShortCut.add(pushUpsLeft, pushUpsDone, score, score + mScoring[i]);
                    return score + mScoring[i];
                }
                if ( totalPushUps < pushUpsLeft )
                {
                    returnValue = findScore(pushUpsLeft,totalPushUps,score+mScoring[i]);

                    if(-1 != returnValue)
                    {
                        if(returnValue>curScore)
                        {
                            curScore = returnValue;
                        }
//                        returnValue = -1;
                    }
                }
            }

            if(curScore>-1)
            {
                mShortCut.add(pushUpsLeft, pushUpsDone, score, curScore);
                return curScore;
            }

            mShortCut.add(pushUpsLeft, pushUpsDone, score, returnValue);
            return returnValue;
        }

        static void RunTest(int theCase)
        {
            string text;
            int score;
            int pushUps;
            int numberofScoring;

            text = Console.ReadLine();
            string[] textTokens = text.Split(new char[1] { ' ' });
            int.TryParse(textTokens[0], out pushUps);
            int.TryParse(textTokens[1], out numberofScoring);

            mScoring.Clear();
            mShortCut = new shortCut();
            text = Console.ReadLine();
            textTokens = text.Split(new char[1] { ' ' });

            for ( int index=0; index < numberofScoring; index++ )
            {
                int type;

                int.TryParse(textTokens[index],out type);
                mScoring.Add(type);
            }

            score = findScore(pushUps,0,0);
            Console.WriteLine(score.ToString());
        }
    }
}
