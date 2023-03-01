import java.awt.Point;
import java.io.PrintStream;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

/**
 * Solution to Diagonals.
 *
 * @author vanb
 */
public class diagonals_vanb
{
    /** Input. */
    private static Scanner sc;
    
    /** Output. */
    private static PrintStream ps;
    
    /**  The final output. */
    private char slants[][];
    
    /**  The number of slants each Intersection needs. */
    private int needs[][];
            
    
    /**
     * Gets at a slant in the grid, or returns X is the coords are off the grid.
     *
     * @param i the row
     * @param j the column
     * @return the content of slants[i][j], or 'X' if (i,j) is off the grid.
     */
    private char getat( int i, int j )
    {
        return i<0 || i>=slants.length || j<0 || j>=slants.length ? 'X' : slants[i][j];
    }
    
    /**
     * Test if an intersection is satisfied, or has too many slants!
     * While you're at it, if it can be filled, fill it, and check its neighbors.
     *
     * @param i the row
     * @param j the column
     * @param changes a list of the changes so far
     * @return true, if successful
     */
    private boolean test( int i, int j, List<Point> changes )
    {
        boolean ok = true;
        
        int touches = 0;
        int unknowns = 0;
       
        // Get the Northwest, Southwest, Northeast and Southeast slants
        char nw = getat( i-1, j-1 );
        char sw = getat( i, j-1 );
        char ne = getat( i-1, j );
        char se = getat( i, j );
        
        // Count unknowns and touches
        if( nw=='.' ) ++unknowns;
        else if( nw=='\\' ) ++touches;
        
        if( sw=='.' ) ++unknowns;
        else if( sw=='/' ) ++touches;
        
        if( ne=='.' ) ++unknowns;
        else if( ne=='/' ) ++touches;
        
        if( se=='.' ) ++unknowns;
        else if( se=='\\' ) ++touches;
        
        // If the number of touches is too many or too few, we fail.
        if( touches>needs[i][j] || (touches<needs[i][j] && unknowns==0 && needs[i][j] < 10) ) ok = false;
        // If we've got unknowns to play with, see if we can set them.
        else if( unknowns>0 )
        {
            // These are the neighbors we need to go to if we place a slant.
            boolean tests[][] = new boolean[3][3];
            Arrays.fill( tests[0], false );
            Arrays.fill( tests[1], false );
            Arrays.fill( tests[2], false );

            // If we have all we need, then we can set the unknowns to point away.
            if( touches==needs[i][j] )
            {
                if( nw=='.'  ) 
                {
                    ok &= place( i-1, j-1, '/', changes );
                    tests[0][0] = true;
                    tests[0][1] = true;
                    tests[1][0] = true;
                }
                if( sw=='.'  ) 
                {
                    ok &= place( i, j-1, '\\', changes );  
                    tests[1][0] = true;
                    tests[2][0] = true;
                    tests[2][1] = true;
                }
                if( ne=='.'  )
                {
                    ok &= place( i-1, j, '\\', changes ); 
                    tests[0][1] = true;
                    tests[0][2] = true;
                    tests[1][2] = true;
                }
                if( se=='.'  ) 
                {
                    ok &= place( i, j, '/', changes );
                    tests[1][2] = true;
                    tests[2][1] = true;
                    tests[2][2] = true;
                }
            }
            // Otherwise, if the number of unknowns is exactly what we currently need, make them point here.
            else if( unknowns==needs[i][j]-touches )
            {
                if( nw=='.'  ) 
                {
                    ok &= place( i-1, j-1, '\\', changes );
                    tests[0][0] = true;
                    tests[0][1] = true;
                    tests[1][0] = true;
                }
                if( sw=='.'  ) 
                {
                    ok &= place( i, j-1, '/', changes );  
                    tests[1][0] = true;
                    tests[2][0] = true;
                    tests[2][1] = true;
                }
                if( ne=='.'  )
                {
                    ok &= place( i-1, j, '/', changes ); 
                    tests[0][1] = true;
                    tests[0][2] = true;
                    tests[1][2] = true;
                }
                if( se=='.'  ) 
                {
                    ok &= place( i, j, '\\', changes );
                    tests[1][2] = true;
                    tests[2][1] = true;
                    tests[2][2] = true;
                }
            }            
            
            // If we set any slants, test the affected intersections.
            for( int di=-1; di<=1 && ok; di++) for( int dj=-1; dj<=1 && ok; dj++ )
            {
                if( tests[di+1][dj+1] ) ok &= test( i+di, j+dj, changes ); 
            }
        }
        
        return ok;
    }
    
    /**
     * Place a slant in the grid.
     *
     * @param i the row
     * @param j the column
     * @param slant the slant
     * @param changes a list of the changes
     * @return true, if successful
     */
    private boolean place( int i, int j, char slant, List<Point> changes ) 
    { 
        // Place the slant
        slants[i][j] = slant;
        
        // This is a new change!
        changes.add( new Point( i, j ) );
        
        // There used to be some checking code here, but no longer.
        return true; 
    }
    
    /** We'll use a DFS to check for cycles. This will tell us if an intersection has already been visited by the DFS. */
    private boolean visited[][];
    
    /**
     * Checks if the node is the root of a tree.
     *
     * @param i the row
     * @param j the column
     * @param fromi the row we came from
     * @param fromj the column we came form
     * @return true, if is tree
     */
    private boolean isTree( int i, int j, int fromi, int fromj )
    {
        // If we got back to a node we already visited, there's a cycle! It's not a tree!
        boolean ok = !visited[i][j];
                
        if( ok )
        {
            // visit this node
            visited[i][j] = true;
            
            // Traverse 'edges' except back to the node we came from.
            if( (i-1!=fromi || j-1!=fromj) && getat( i-1, j-1 )=='\\' ) ok &= isTree( i-1, j-1, i, j );
            if( ok && (i-1!=fromi || j+1!=fromj) && getat( i-1, j )=='/' ) ok &= isTree( i-1, j+1, i, j );
            if( ok && (i+1!=fromi || j-1!=fromj) && getat( i, j-1 )=='/' ) ok &= isTree( i+1, j-1, i, j );
            if( ok && (i+1!=fromi || j+1!=fromj) && getat( i, j )=='\\' ) ok &= isTree( i+1, j+1, i, j );
        }
        return ok;
    }
    
    /** Our choices of slants. */
    private static final char choices[] = { '/', '\\' };
    
    /**
     * Check to see if we can do this.
     *
     * @param level the level, which is row+column*n
     */
    private void cando( int level )
    {
        if( level==0 )
        {
            // We'll use a simple DFS to check for cycles
            for( boolean row[] : visited ) Arrays.fill( row, false );
            boolean ok = true;
            for( int i=0; i<visited.length; i++ ) for( int j = 0; j<visited.length; j++ ) 
            {
                if( !visited[i][j] ) ok &= isTree( i, j, -1, -1 );
            }
            
            // No cycles? Then we've got the (unique) answer!
            if( ok )
            {
                for( char row[] : slants ) ps.println( new String( row ) );
                ps.close();
            }
        }
        else
        {
            level--;
            
            // Extract row/column form the level
            int i = level/slants.length;
            int j = level%slants.length;
            
            // Keep track of changes, so we can undo
            List<Point> changes = new ArrayList<Point>(slants.length*slants.length);
             
            // Is this slant not yet chosen?
            if( slants[i][j]=='.' )
            {
                // Go through our choices.
                for( char choice : choices )
                {
                    changes.clear();
                    
                    // If everything works out, go down a level
                    if( place( i, j, choice, changes )
                            && test( i, j, changes ) 
                            && test( i+1, j, changes ) 
                            && test( i, j+1, changes ) 
                            && test( i+1, j+1, changes ) ) cando( level );
                    
                    // Undo the changes!
                    for( Point p : changes ) slants[p.x][p.y] = '.';
                }
            }
            // If this slant is already chosen, just go down a level.
            else cando( level );            
        }
    }
    
    /**
     * Do it!.
     */
    private void doit()
    {
        int n = sc.nextInt();
        
        slants = new char[n][n];
        needs = new int[n+1][n+1];
        visited = new boolean[n+1][n+1];
        
        // Read the intersections, use 100 (virtually infinity) for unknowns
        for( int i=0; i<n+1; i++ )
        {
            char line[] = sc.next().toCharArray();
            for( int j=0; j<n+1; j++ )
            {
                needs[i][j] = line[j]=='+' ? 100 : (int)(line[j]-'0');
            }
        }
        
        // Fill the output array with '.', which signifies that a slant needs to be there but hasn't been chosen yet.
        for( int i=0; i<n; i++ ) Arrays.fill( slants[i], '.' );
        
        // OK, there's plenty we can tell before we even do the backtracking.
        List<Point> changes = new ArrayList<Point>(n*n);
        for( int i=0; i<n+1; i++ ) for( int j=0; j<n+1; j++ ) 
        {
            test( i, j, changes );
        }
        
        // NOW do the backtracking, and that's it!
        cando( n*n );        
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
        
        new diagonals_vanb().doit();
    }

}
