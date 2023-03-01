
import java.io.*;
import java.util.*;
import java.awt.geom.*;

/**
 * Solution to Triangle
 * 
 * @author vanb
 */
public class gears_vanb
{
    public Scanner sc;
    public PrintStream ps;
    
    public class Move
    {
        public int gear, direction;
        
        public Move( int gear, int direction )
        {
            this.gear = gear;
            this.direction = direction;
        }
    }
    
    public class Gear
    {
        public int x, y, r, direction;
        public LinkedList<Integer> edges = new LinkedList<Integer>();
        
        public Gear( int x, int y, int r )
        {
            this.x = x;
            this.y = y;
            this.r = r;
            this.direction = 0;
        }
    }
    
    public int gcd( int a, int b )
    {
        return b==0 ? a : gcd( b, a%b );
    }
        
    /**
     * Driver.
     * @throws Exception
     */
    public void doit() throws Exception
    {
        sc = new Scanner( System.in );
        ps = System.out;
        
        int n = sc.nextInt();
        Gear gears[] = new Gear[n];
        
        for( int i=0; i<n; i++ )
        {
            int x = sc.nextInt();
            int y = sc.nextInt();
            int r = sc.nextInt();
            
            gears[i] = new Gear( x, y, r );
            for( int j=0; j<i; j++ )
            {
                // If the distance between centers is equal to the sum of radii,
                // then the gears mesh. To keep things in integers, we'll
                // compare distance^2 instead of distance.
                long dx = gears[j].x - x;
                long dy = gears[j].y - y;
                long sr = gears[j].r + r;
                if( dx*dx + dy*dy == sr*sr )
                {
                    gears[i].edges.add( j );
                    gears[j].edges.add( i );
                }
            }
        }
        
        // Can the source gear turn freely?
        boolean free = true;
        LinkedList<Move> q = new LinkedList<Move>();
        q.add( new Move( 0, 1 ) );
        while( free && !q.isEmpty() )
        {
            Move m = q.removeFirst();
            
            // If we haven't visited this gear yet, then
            // set its direction and go to its neighbors.
            if( gears[m.gear].direction==0 )
            {
                gears[m.gear].direction = m.direction;
                for( int i : gears[m.gear].edges )
                {
                    q.add( new Move( i, -m.direction ) );
                }
            }
            else 
            {
                // If we have visited it, make sure
                // it's turning in the right direction.
                free = gears[m.gear].direction == m.direction;
            }
        }
        
        // If free is false, then the source gear cannot turn freely.
        if( !free ) ps.println( "The input gear cannot move." );
        // If the target gear's direction hasn't been set, then we can't reach it.
        else if( gears[n-1].direction==0 ) ps.println( "The input gear is not connected to the output gear." );
        else
        {
            // Otherwise, the turning ratio is just based on the teeth,
            // which in turn is based on the radius
            // (which is weird - it OUGHT to be based on the circumference.)
            int factor = gcd( gears[0].r, gears[n-1].r );
            int a = gears[0].r / factor * gears[n-1].direction;
            int b = gears[n-1].r / factor;
            ps.println( a + ":" + b );
        }
        
    }
    
    /**
     * @param args
     */
    public static void main( String[] args ) throws Exception
    {
        new gears_vanb().doit();        
    }   
}
