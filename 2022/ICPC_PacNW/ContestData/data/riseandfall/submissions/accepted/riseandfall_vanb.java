import java.io.PrintStream;
import java.util.Arrays;
import java.util.Scanner;

/**
 * Solution to Rise and Fall
 * 
 * @author vanb
 */
public class riseandfall_vanb
{
    /** Input. */
    private static Scanner sc;
    
    /** Output. */
    private static PrintStream ps;
    
    /**
     * Do it!
     */
    private void doit()
    {
        int t = sc.nextInt();
        while( t-->0 )
        {
            char digits[] = sc.next().toCharArray();
            char answer[] = new char[digits.length];
            char next = '0';
            boolean rising = true;
            boolean fixed = false;
            for( int i=0; i<digits.length; i++ )
            {
                char digit = digits[i];
                
                // If we're falling, and the next digit in the number is bigger than the one we've got,
                // then we need to fix the one we've got and keep using it.
                // Anything else would make the Rainbow Number smaller when it doesn't need to be.
                if( !rising && next<digit )
                {
                    fixed = true;
                }
                // If we're rising and the next digit is smaller, then we've started falling.
                else if( rising && digit<next ) 
                {
                    rising = false;
                    next = digit;
                }
                // If we haven't fixed a digit, and we're rising or the next digit is smaller,
                // then we've got to take the next digit.
                if( !fixed && (rising || digit<next) ) next = digit; 
                answer[i] = next;
            }
            
            ps.println( new String(answer) );
        }
    }
        
    /**
     * Main.
     * 
     * @param args unused
     * @throws Exception
     */
    public static void main( String[] args ) throws Exception
    {
        sc = new Scanner( System.in );
        ps = System.out;
        
        new riseandfall_vanb().doit();
    }

}
