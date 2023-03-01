using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace PNW2013ProbA
{
    class Program
    {
        static void Main(string[] args)
        {
            string numberOfString = Console.ReadLine();
            int numberOfTest;
            int.TryParse(numberOfString,out numberOfTest);

            for(int index=0;index<numberOfTest;index++)
            {
                RunTest();
            }
        }

        const int NUM = 1;
        const int QUESTION = 2;
        const int OPER = 3;
        const int BAD = 4;

        static int getType(char ch)
        {
            int returnValue = BAD;

            if ( ( '-' == ch ) || ( '+' == ch ) || ( '*' == ch ) || ( '=' == ch ) )
            {
                returnValue = OPER;
            }
            else
            {
                if('?' == ch)
                {
                    returnValue = QUESTION;
                }
                else
                {
                        if((ch>='0')&&(ch<='9'))
                        {
                            returnValue = NUM;
                        }
                }
            }


            return returnValue;
        }

        const int FIRST_NUM = 0;
        const int SECOND_NUM = 1;
        const int THIRD_NUM = 2;
        const int OPER_NUM = 3;

        static int solve(string num1, string op, string num2, string num3, string numbers)
        {
            int returnValue = -1;

            for(int index=0;index<10;index++)
            {
                if('X' != numbers[index])
                {
                    char ch = numbers[index];
                    num1 = num1.Replace('?', ch);
                    num2 = num2.Replace('?', ch);
                    num3 = num3.Replace('?', ch);

                    bool badNumber = false;

                    if ( ( num1.Length > 1 ) && ( '0' == num1[0] ) )
                    {
                        badNumber = true;
                    }
                    if ( ( num2.Length > 1 ) && ( '0' == num2[0] ) )
                    {
                        badNumber = true;
                    }
                    if ( ( num3.Length > 1 ) && ( '0' == num3[0] ) )
                    {
                        badNumber = true;
                    }

                    if(false == badNumber)
                    {
                        int inum1 = 0;
                        int inum2 = 0;
                        int inum3 = 0;

                        int.TryParse(num1, out inum1);
                        int.TryParse(num2, out inum2);
                        int.TryParse(num3, out inum3);

    //                    Console.WriteLine(num1 + " " + op + " " + num2 + " " + num3 + " " + numbers);

                        if ( ( "+" == op ) && ( ( inum1 + inum2 ) == inum3 ) )
                        {
                            return ch - '0';
                        }

                        if ( ( "-" == op ) && ( ( inum1 - inum2 ) == inum3 ) )
                        {
                            return ch - '0';
                        }

                        if ( ( "*" == op ) && ( ( inum1 * inum2 ) == inum3 ) )
                        {
                            return ch - '0';
                        }
                    }
                    num1 = num1.Replace(ch, '?');
                    num2 = num2.Replace(ch, '?');
                    num3 = num3.Replace(ch, '?');
                }

            }
            return returnValue;
        }

        static void RunTest()
        {
            string text;
            int count=0;
            string num1="";
            string num2="";
            string num3="";
            string op="";
            string numbers = "0123456789";
            int state = FIRST_NUM;

            text = Console.ReadLine();

            for(int index=0;index<text.Length;index++)
            {
                char ch = text[index];
                int type = getType(ch);

                if(NUM == type)
                {
                    numbers = numbers.Replace(ch,'X');
                }

                switch(state)
                {
                    case ( FIRST_NUM ):
                        switch ( type )
                        {
                            case ( OPER ):
                                if(""==num1)
                                {
                                    num1+=ch;
                                }
                                else
                                {
                                    op += ch;
                                    state = SECOND_NUM;
                                }
                                break;
                            case ( NUM ):
                                num1 += ch;
                                break;
                            case ( QUESTION ):
                                num1 += ch;
                                break;
                        }
                        break;
                    case ( SECOND_NUM ):
                        switch ( type )
                        {
                            case ( OPER ):
                                if(""==num2)
                                {
                                    num2+=ch;
                                }
                                else
                                {
                                    state = THIRD_NUM;
                                }
                                break;
                            case ( NUM ):
                                num2 += ch;
                                state = SECOND_NUM;
                                break;
                            case ( QUESTION ):
                                num2 += ch;
                                state = SECOND_NUM;
                                break;
                        }
                        break;
                    case ( THIRD_NUM ):
                        switch ( type )
                        {
                            case ( OPER ):
                                num3 += ch;
                                break;
                            case ( NUM ):
                                num3 += ch;
                                break;
                            case ( QUESTION ):
                                num3 += ch;
                                break;
                        }
                        break;
                    case ( OPER_NUM ):
                        switch ( type )
                        {
                            case ( OPER ):
                                op += ch;
                                break;
                            case ( NUM ):
                                num2 += ch;
                                state = SECOND_NUM;
                                break;
                            case ( QUESTION ):
                                num2 += ch;
                                state = SECOND_NUM;
                                break;
                        }
                        break;
                }
            }

//            Console.WriteLine(">"+num1 + " " + op + " " + num2 + " " + num3 + " " + numbers);
            count = solve(num1,op,num2,num3,numbers);
            Console.WriteLine(count.ToString());
        }
    }
}
