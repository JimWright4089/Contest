import java.io.PrintStream;
import java.util.Arrays;
import java.util.Scanner;

/**
 * Solution to Scaling Recipe
 * 
 * @author vanb
 */
public class scalingrecipe_vanb
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
        int n = sc.nextInt();
        int x = sc.nextInt();
        int y = sc.nextInt();
        
        // The amount we need is a*y/x.
        while( n-->0 )
        {
            int a = sc.nextInt();
            ps.println( a*y/x );
        }
        sc.close();
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
        
        new scalingrecipe_vanb().doit();
    }

}
