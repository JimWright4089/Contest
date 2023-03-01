import java.io.PrintStream;
import java.util.Arrays;
import java.util.Scanner;

/**
 * Solution to Double Password
 * 
 * @author vanb
 */
public class doublepassword_vanb
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
        char pwd1[] = sc.next().toCharArray();
        char pwd2[] = sc.next().toCharArray();
        
        // The count doubles for each mismatch
        int count = 1;
        for( int i=0; i<Math.min( pwd1.length, pwd2.length ); i++ )
        {
            if( pwd1[i] != pwd2[i] ) count *= 2;
        }
        
        System.out.println( count );
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
        
        new doublepassword_vanb().doit();
    }

}
