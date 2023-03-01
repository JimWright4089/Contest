using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2015ProbM
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] delims = { ' ', ':' };

            string numberOfString = Console.ReadLine();
            string[] parts = numberOfString.Split(delims);
            int numberOfOperators;
            List<OperLine> mOpers = new List<OperLine>();

            int.TryParse(parts[0], out numberOfOperators);

            for(int i=0;i<numberOfOperators;i++)
            {
                mOpers.Add(new OperLine(Console.ReadLine()));
            }

            int count = 0;
            for(int i=1;i<=100;i++)
            {
                int num = i;
                for(int j=0;j<mOpers.Count;j++)
                {
                    if(false == mOpers[j].Preform(ref num))
                    {
                        count++;
                        break;
                    }
                }
            }

            Console.WriteLine(count);
        }
    }

    public class OperLine
    {
        public enum TheOper
        {
            ADD,
            SUBTRACT,
            MULTIPLY,
            DIVIDE
        }

        public TheOper mOperator = TheOper.ADD;
        public int mValue = 0;
        public double mDoubleValue = 0.0;

        public OperLine()
        {

        }
    
        public OperLine(string line)
        {
            char[] delims = { ' ', ':' };
            string[] parts = line.Split(delims);

            mOperator = ToEnum(parts[0]);
            int.TryParse(parts[1], out mValue);
            mDoubleValue = (double)mValue;
        }

        private TheOper ToEnum(string value)
        {
            switch(value)
            {
                case ("ADD"):
                    return TheOper.ADD;
                case ("SUBTRACT"):
                    return TheOper.SUBTRACT;
                case ("DIVIDE"):
                    return TheOper.DIVIDE;
                case ("MULTIPLY"):
                    return TheOper.MULTIPLY;
            }
            return TheOper.ADD;
        }

        public bool Preform(ref int num)
        {
            bool returnValue = true;
            int intResult = num;
            double doubleResult = (double)num;

            switch(mOperator)
            {
                case (TheOper.ADD):
                    intResult += mValue;
                    doubleResult += mDoubleValue;
                    break;
                case (TheOper.SUBTRACT):
                    intResult -= mValue;
                    doubleResult -= mDoubleValue;
                    break;
                case (TheOper.MULTIPLY):
                    intResult *= mValue;
                    doubleResult *= mDoubleValue;
                    break;
                case (TheOper.DIVIDE):
                    if(0 == mValue)
                    {
                        return false;
                    }
                    intResult /= mValue;
                    doubleResult /= mDoubleValue;
                    break;
            }

            if(0 > intResult)
            {
                return false;
            }

            if(((double)intResult) != doubleResult)
            {
                return false;
            }
            num = intResult;

            return returnValue;
        }

    }


}
