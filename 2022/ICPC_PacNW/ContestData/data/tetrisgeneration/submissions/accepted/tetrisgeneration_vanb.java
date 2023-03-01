import java.io.PrintStream;
import java.util.Arrays;
import java.util.Scanner;

/**
 * Solution to Tetris Generation.
 *
 * @author vanb
 */
public class tetrisgeneration_vanb
{
    /** Input. */
    private static Scanner sc;
    
    /** Output. */
    private static PrintStream ps;
    
    /** The Constant tetrominos. */
    private static final String tetrominos = "JLSZIOT";
    
    /** Which tetrominos have been seen in a segment? */
    private static boolean seen[] = new boolean[7];
    
    /** The input string. */
    private char s[];
    
    /**
     * Test a segment for repeated tetrominos
     *
     * @param lo the lo index
     * @param hi the hi index - 1
     * @return true, if no repeated tetrominos
     */
    private boolean test( int lo, int hi )
    {
        boolean ok = true;
        Arrays.fill( seen, false );
        for( int i=lo; i<hi; i++ )
        {
            // We'll identify a tetromino by its position in this string
            int index = tetrominos.indexOf( s[i] );
            
            // If a tetromino has already been seen in this segment, we fail.
            if( seen[index] )
            {
                ok = false;
                break;
            }
            
            // We saw it!
            seen[index] = true;
        }
        
        return ok;
    }
    
    /**
     * Do it!.
     */
    private void doit()
    {
        int t = sc.nextInt();
        
        while( t-->0 )
        {
            s = sc.next().toCharArray();
            
            boolean ok = false;
            
            // We've got to test (up to) 7 different start points
            // We've got to be careful not to go off the end of the string
            for( int start=0; start<Math.min( 7, s.length ) && !ok; start++ )
            {
                // Test the start
                ok = test( 0, start );
                
                // Test each successive 7 character segment
                for( int i=start; i<s.length; i+=7 )
                {
                    // Again, don't go off the end of the string
                    ok &= test( i, Math.min( i+7, s.length ) );
                }
                
                // If we found a good one, great! Break!
                if( ok ) break;
            }
            
            ps.println( ok ? 1 : 0 );
        }
        
    }
        
    /**
     * Main.
     *
     * @param args unused
     * @throws Exception the exception
     */
    public static void main( String[] args ) throws Exception
    {
        sc = new Scanner( System.in );
        ps = System.out;
        
        new tetrisgeneration_vanb().doit();
    }

}
