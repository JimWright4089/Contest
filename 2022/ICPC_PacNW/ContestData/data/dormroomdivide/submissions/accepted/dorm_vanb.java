
import java.io.PrintStream;
import java.util.Arrays;
import java.util.Scanner;

/**
 * Solution to Dorm Room Divide
 */
public class dorm_vanb
{
    /** The input. */
    private static Scanner sc;

    /** The output. */
    private static PrintStream ps;
    
    private static final double EPSILON = 1e-7;
    
    /**
     * Area of a triangle with vertices (x1,y1), (x2,y2) and (x3,y3)
     * 
     * @param x1
     * @param y1
     * @param x2
     * @param y2
     * @param x3
     * @param y3
     * @return Area
     */
    private static double triarea( double x1, double y1, double x2, double y2, double x3, double y3 )
    {
        return Math.abs( (x2-x1)*(y2+y1) + (x3-x2)*(y3+y2) + (x1-x3)*(y1+y3) )/2.0;   
    }
    
    /**
     * Do it.
     */
    private void doit()
    {
        int n = sc.nextInt();
        double xs[] = new double[n];
        double ys[] = new double[n];
        for( int i=0; i<n; i++ )
        {
            xs[i] = sc.nextInt();
            ys[i] = sc.nextInt();
        }
        
        // Compute the areas of every incomplete polygon P0-->P1-->Pi-->P0
        // for every i from 3 to n-1
        double areas[] = new double[n];
        areas[0] = areas[1] = 0.0; 
        double area = (xs[1]-xs[0])*(ys[1]+ys[0])/2.0;
        for( int i=2; i<n; i++)
        {
            area += (xs[i]-xs[i-1])*(ys[i]+ys[i-1])/2.0;
            areas[i] = Math.abs(area + (xs[0]-xs[i])*(ys[0]+ys[i])/2.0);
        }
        
        double half = areas[n-1]/2.0;
        double x=0, y=0;
        int index = Arrays.binarySearch( areas, half );
        System.err.println( index );
        if( index>=0 )
        {
            x = xs[index];
            y = ys[index];
        }
        else
        {
            index = -(index+1);
            double hix = xs[index];
            double hiy = ys[index];
            double lox = xs[index-1];
            double loy = ys[index-1];
            double doorx = xs[0];
            double doory = ys[0];
            double basex = lox;
            double basey = loy;

            for(;;)
            {
                double midx = (lox+hix)/2.0;          
                double midy = (loy+hiy)/2.0;
                double areamid = triarea( doorx, doory, basex, basey, midx, midy )+areas[index-1];
                System.err.println( midx + " " + midy + " " + areamid + " " + half );
                if( Math.abs( areamid-half )<EPSILON )
                {
                    x = midx;
                    y = midy;
                    break;
                }
                else if( areamid<half )
                {
                    lox = midx;
                    loy = midy; 
                }
                else
                {
                    hix = midx;
                    hiy = midy;
                }
            }
        }
        
        ps.println( x + " " + y );
    }

    /**
     * The main method.
     *
     * @param args the arguments (unused)
     */
    public static void main( String[] args )
    {
        sc = new Scanner( System.in );
        ps = System.out;
        new dorm_vanb().doit();
        //System.err.println( triarea( 7.0, 1.0, 8.0, 3.0, 5.0, 5.0 ) );
        //System.err.println( triarea( 7.0, 1.0, 5.0, 5.0, 2.0, 3.0 ) );
    }

}
