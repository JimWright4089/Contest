import java.io.*;
import java.util.*;

/**
 * Recursive backtracking solution.
 * Make sure that the constraints are not exceeded with each placement and that constraints
 * are satisfied perfectly if future placements cannot change intersection counts.
 * Only do the cycle checking if \/ is placed.
 *
 * @author Finn Lidbetter
 */

public class diag_finn {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        int n = Integer.parseInt(br.readLine());
        int[][] gridCounts = new int[n+1][n+1];
        for (int i=0; i<n+1; i++) {
            char[] row = br.readLine().toCharArray();
            for (int j=0; j<n+1; j++) {
                if (row[j]=='+') {
                    gridCounts[i][j] = -1;
                } else {
                    gridCounts[i][j] = (int)(row[j]-'0');
                }
            }
        }
        boolean[][][][] adj = new boolean[n+1][n+1][n+1][n+1];
        int[][] currentCounts = new int[n+1][n+1];
        char[][] solution = new char[n][n];
        solve(gridCounts, currentCounts, solution, adj, 0, 0);
        StringBuilder sb = new StringBuilder();
        for (int r=0; r<solution.length; r++) {
            for (int c=0; c<solution[r].length; c++) {
                sb.append(solution[r][c]);
            }
            sb.append("\n");
        }
        System.out.print(sb);
    }
    static boolean solve(int[][] gridCounts, int[][] currentCounts, char[][] solution, boolean[][][][] adj, int row, int col) {
        int n = solution.length;
        // Try placing '\'
        solution[row][col] = '\\';
        currentCounts[row][col]++;
        currentCounts[row+1][col+1]++;
        adj[row][col][row+1][col+1] = true;
        adj[row+1][col+1][row][col] = true;
        boolean conditionsMet = checkConditions(gridCounts, currentCounts, solution, adj, row, col);
        if (conditionsMet) {
            if (row==n-1 && col==n-1) {
                return true;
            }
            int nextRow = row;
            int nextCol = col+1;
            if (col==n-1) {
                nextRow = row+1;
                nextCol = 0;
            }
            if (solve(gridCounts, currentCounts, solution, adj, nextRow, nextCol)) {
                return true;
            }
        }

        adj[row][col][row+1][col+1] = false;
        adj[row+1][col+1][row][col] = false;
        currentCounts[row+1][col+1]--;
        currentCounts[row][col]--;
        solution[row][col] = '0';

        // Try placing '/'
        solution[row][col] = '/';
        currentCounts[row][col+1]++;
        currentCounts[row+1][col]++;
        adj[row][col+1][row+1][col] = true;
        adj[row+1][col][row][col+1] = true;
        conditionsMet = checkConditions(gridCounts, currentCounts, solution, adj, row, col);
        if (conditionsMet) {
            if (row==n-1 && col==n-1) {
                return true;
            }
            int nextRow = row;
            int nextCol = col+1;
            if (col==n-1) {
                nextRow = row+1;
                nextCol = 0;
            }
            if (solve(gridCounts, currentCounts, solution, adj, nextRow, nextCol)) {
                return true;
            }
        }

        adj[row][col+1][row+1][col] = false;
        adj[row+1][col][row][col+1] = false;
        currentCounts[row][col+1]--;
        currentCounts[row+1][col]--;
        solution[row][col] = '0';
        return false;
    }
    static boolean checkConditions(int[][] gridCounts, int[][] currentCounts, char[][] solution, boolean[][][][] adj, int row, int col) {
        int n = solution.length;
        if (gridCounts[row][col]!=-1 && gridCounts[row][col]!=currentCounts[row][col]) {
            return false;
        }
        if (col==n-1 && gridCounts[row][col+1]!=-1 && currentCounts[row][col+1]!=gridCounts[row][col+1]) {
            return false;
        }
        if (row==n-1 && gridCounts[row+1][col]!=-1 && currentCounts[row+1][col]!=gridCounts[row+1][col]) {
            return false;
        }
        if (row==n-1 && col==n-1 && gridCounts[row+1][col+1]!=-1 && currentCounts[row+1][col+1]!=gridCounts[row+1][col+1]) {
            return false;
        }
        if (col>0 && solution[row][col]=='/' && solution[row][col-1]=='\\') {
            if (hasCycle(adj, row+1, col)) {
                return false;
            }
        }
        int[] dr = new int[]{0,0,1,1};
        int[] dc = new int[]{0,1,0,1};
        for (int i=0; i<4; i++) {
            int r = row + dr[i];
            int c = col + dc[i];
            if (gridCounts[r][c]!=-1 && currentCounts[r][c]>gridCounts[r][c]) {
                return false;
            }
        }
        return true;
    }


    static boolean hasCycle(boolean[][][][] adj, int srcRow, int srcColumn) {
        int n = adj.length - 1;
        int[][] vis = new int[n+1][n+1];
        return cycleDfs(adj, vis, srcRow, srcColumn);
    }
    static boolean cycleDfs(boolean[][][][] adj, int[][] vis, int row, int col) {
        // vis[r][c] = 0 if not visited at all.
        // vis[r][c] = 1 if currently processing.
        // vis[r][c] = 2 if finished processing.
        // Cycle is found if we encounter more than a single 1.
        vis[row][col] = 1;
        int[] dr = new int[]{-1,-1,1,1};
        int[] dc = new int[]{1,-1,-1,1};
        boolean cycleDetected = false;
        int adjacent1s = 0;
        for (int i=0; i<4; i++) {
            int nr = row + dr[i];
            int nc = col + dc[i];
            if (nr<0 || nr>=vis.length || nc<0 || nc>=vis.length) {
                continue;
            }
            if (adj[row][col][nr][nc]) {
                if (vis[nr][nc]==1) {
                    adjacent1s++;
                } else if (vis[nr][nc]==0) {
                    cycleDetected = cycleDetected || cycleDfs(adj, vis, nr, nc);
                }
            }
        }
        vis[row][col] = 2;
        if (adjacent1s>1) {
            cycleDetected = true;
        }
        return cycleDetected;
    }
}
