import java.awt.Point;
import java.io.PrintStream;
import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;
import java.util.List;
import java.util.PriorityQueue;
import java.util.Scanner;

/**
 * Solution to
 * 
 * @author vanb
 */
public class hopscotch_vanb
{
    /** Input. */
    private static Scanner sc;
    
    /** Output. */
    private static PrintStream ps;
    
    private class State implements Comparable<State>
    {
        public int k;
        public Point p;
        public long distance;
        public State(int k, Point p, long distance)
        {
            super();
            this.k = k;
            this.p = p;
            this.distance = distance;
        }
        @Override
        public int compareTo( State o )
        {
            // TODO Auto-generated method stub
            return Long.compare(distance,o.distance);
        }
        
        @Override
        public String toString()
        {
            return "[" + k + "," + p + "," + distance + "]";
        }
    }
    
    /**
     * Do it!
     */
    private void doit()
    {
        int n = sc.nextInt();
        int k = sc.nextInt();
        
        List<Point> tiles[] = new ArrayList[k+1];
        for( int i=0; i<=k; i++ ) tiles[i] = new ArrayList<Point>();
        
        for( int i=0; i<n; i++ ) for( int j=0; j<n; j++ ) 
        {
            int x = sc.nextInt();
            tiles[x].add( new Point( i, j ) );
        }
        
        long answer = -1L;
        HashSet<Point> visited = new HashSet<Point>(n*n);
        PriorityQueue<State> queue = new PriorityQueue<State>(n*n);
        for( Point p : tiles[1] ) queue.add( new State( 1, p, 0 ) );
        while( !queue.isEmpty() )
        {
            State s = queue.poll();
            if( s.k==k )
            {
                answer = s.distance;
                break;
            }
            
            visited.add( s.p );
            for( Point p : tiles[s.k+1] ) if( !visited.contains( p ) )
            {
                queue.add( new State( s.k+1, p, s.distance+Math.min( (p.x-s.p.x)*(p.x-s.p.x), (p.y-s.p.y)*(p.y-s.p.y) ) ) );
            }
        }
        
        ps.println( answer );
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
        
        new hopscotch_vanb().doit();
    }

}
