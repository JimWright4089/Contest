

import java.awt.Point;
import java.io.PrintStream;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.List;
import java.util.PriorityQueue;
import java.util.Scanner;

/**
 * Solution to Hopscotch with n up to 1000.
 */
public class hopscotch1000_vanb
{

    /** The input. */
    private static Scanner sc;

    /** The output. */
    private static PrintStream ps;

    /**
     * A comparator to compare points by X coordinate.
     */
    private class ByX implements Comparator<Point>
    {

        /**
         * Compare two points by X coordinate.
         *
         * @param a A point
         * @param b Another point
         * @return Standard for compareTo
         */
        @Override
        public int compare( Point a, Point b )
        {
            return a.x-b.x;
        }
    }

    /**
     * A comparator to compare points by Y coordinate.
     */
    private class ByY implements Comparator<Point>
    {

        /**
         * Compare two points by Y coordinate.
         *
         * @param a A point
         * @param b Another point
         * @return Standard for compareTo
         */
        @Override
        public int compare( Point a, Point b )
        {
            return a.y-b.y;
        }
    }

    /**
     * Find the closest point.
     *
     * @param p the p
     * @param points the points
     * @param comparator the comparator
     * @return the point
     */
    private Point findClosestPoint( Point p, Point points[], Comparator<Point> comparator  )
    {
        // Use binary search to find the closest point in a list of points.
        int index = Arrays.binarySearch( points, p, comparator );

        // Java's binarySearch returns the index on an exact match, -(insertionpoint-1) if no exact match
        if( index<0 )
        {
            // If no exact match, then this is the index of the first value in the list greater than p
            index = -(index+1);

            // It could be off the end of the list if p>everything in the list
            if( index>=points.length ) --index;
            // Otherwise, we have to also look at the item immediately previous (if it exists).
            else if( index>0 )
            {
                // Are we checking X coords?
                if( comparator instanceof ByX )
                {
                    if( Math.abs(points[index-1].x-p.x)<Math.abs(points[index].x-p.x)) --index;
                }
                // otherwise, Y coords
                else if( Math.abs(points[index-1].y-p.y)<Math.abs(points[index].y-p.y)) --index;
            }
        }

        return points[index];
    }

    /**
     * Do it.
     */
    private void doit()
    {
        int n = sc.nextInt();
        int k = sc.nextInt();

        // For every i from 1 to k, we'll keep a list of all cells in the matrix with value i.
        List<Point>[] cells = new ArrayList[k+1];
        for( int i=1; i<=k; i++ ) cells[i] = new ArrayList<Point>();

        // Also for each i, we'll keep a list of those points sorted by X coord, and also by Y coord.
        Point[][] cellsByX = new Point[k+1][];
        Point[][] cellsByY = new Point[k+1][];

        // distances[i][j] will be the minimum distance from some 1 cell to cell (i,j)
        int distances[][] = new int[n][n];

        // Read the matrix, populate cells[], and put a signal value in distances[i][j]
        for( int i=0; i<n; i++ ) for( int j=0; j<n; j++ )
        {
            cells[sc.nextInt()].add( new Point(i,j) );
            distances[i][j] = Integer.MAX_VALUE;
        }

        // Create the sorted lists cellsByX and cellsByY
        ByX xComparator = new ByX();
        ByY yComparator = new ByY();
        for( int i=1; i<=k; i++ )
        {
            // First X
            Point byX[] = (Point[])cells[i].toArray( new Point[cells[i].size()] );
            Arrays.sort( byX, xComparator );
            cellsByX[i] = byX;

            // Then Y
            Point byY[] = Arrays.copyOf( byX, byX.length );
            Arrays.sort( byY, yComparator );
            cellsByY[i] = byY;
        }

        // All the 1 cells have distance 0.
        for( Point cell1 : cells[1] ) distances[cell1.x][cell1.y] = 0;

        // For cells with values 2 through k, find the closest cell with the previous value
        for( int i=2; i<=k; i++ )
        {
            // If this happens, we cannot make it to the end
            if( cells[i].isEmpty() ) break;

            // Remember the sorted lists of cells with the previous value
            Point lastXs[] = cellsByX[i-1];
            Point lastYs[] = cellsByY[i-1];

            // For each cell with the value i
            for( Point cell : cells[i] )
            {
                // Find the bext by X
                Point closestX = findClosestPoint( cell, lastXs, xComparator );
                int distanceX = distances[closestX.x][closestX.y] + (closestX.x-cell.x)*(closestX.x-cell.x);

                // Find the best by Y
                Point closestY = findClosestPoint( cell, lastYs, yComparator );
                int distanceY = distances[closestY.x][closestY.y] + (closestY.y-cell.y)*(closestY.y-cell.y);

                // Use the best between the two
                distances[cell.x][cell.y] = Math.min( distanceX, distanceY );
            }
        }

        // Find the answer - which is the minimum distance to a k cell
        int answer = Integer.MAX_VALUE;
        for( Point cellk : cells[k] )
        {
            int d = distances[cellk.x][cellk.y];
            if( d<answer ) answer = d;
        }

        // Print the answer - if it's our signal, we never got there.
        ps.println( answer==Integer.MAX_VALUE ? -1 : answer  );
    }

    /**
     * The main method.
     *
     * @param args the arguments (unused)
     */
    public static void main( String[] args )
    {
        sc = new Scanner(System.in);
        ps = System.out;
        new hopscotch1000_vanb().doit();
    }

}
