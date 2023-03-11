import java.io.*;
import java.util.*;

/**
 * Use 2-sat. Build the implication graph by a creating a clause for each pair 
 * of non-X answers for each student. Then use Tarjan's algorithm for Strongly
 * Connected Components to determine if there is a satisfying assignment. Then
 * assign all of the non-free variables. Then assign any unbound variables in
 * lexicgraphic order, propagating any assignments that are made.
 *  Complexity: O(n*k^2+k^3), with a bit more care this could probably be 
 *  reduced to O(n*k^2 + k^2)=O(n*k^2), but since k has the same upper bound 
 *  as n, this is not necessary.
 *
 * @author Finn Lidbetter
 */

public class exam_finn {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        String[] s = br.readLine().split(" ");

        int n = Integer.parseInt(s[0]);
        int k = Integer.parseInt(s[1]);
        boolean[][] adj = new boolean[2*k][2*k];
        for (int i=0; i<n; i++) {
            char[] seq = br.readLine().toCharArray();
            for (int q1=0; q1<k; q1++) {
                if (seq[q1] == 'X') {
                    continue;
                }
                for (int q2=0; q2<k; q2++) {
                    if (q1==q2 || seq[q2] == 'X') {
                        continue;
                    }
                    // Either the answer to q1 is wrong or the answer to q2 is wrong.
                    // ~seq[q1] v ~seq[q2]  <==>  seq[q1] => ~seq[q2]  <==>  seq[q2] => ~seq[q1].
                    if (seq[q1] == 'T') {
                        if (seq[q2] == 'T') {
                            adj[q1][q2+k] = true;
                            adj[q2][q1+k] = true;
                        } else {
                            adj[q1][q2] = true;
                            adj[q2+k][q1+k] = true;
                        }
                    } else {
                        if (seq[q2] == 'T') {
                            adj[q1+k][q2+k] = true;
                            adj[q2][q1] = true;
                        } else {
                            adj[q1+k][q2] = true;
                            adj[q2+k][q1] = true;
                        }
                    }
                }
            }
        }
        Tarjan sccs = new Tarjan(adj);
        boolean valid = true;
        for (int i=0; i<k; i++) {
            if (sccs.id[i] == sccs.id[i+k]) {
                valid = false;
                break;
            }
        }
        if (!valid) {
            System.out.println("-1");
            return;
        }
        boolean[][] adj2 = new boolean[sccs.count][sccs.count];
        for (int i=0; i<2*k; i++) {
            for (int j=0; j<2*k; j++) {
                if (adj[i][j] && sccs.id[i]!=sccs.id[j]) {
                    adj2[sccs.id[i]][sccs.id[j]] = true;
                }
            }
        }
        Boolean[] assignment = new Boolean[k];
        // First, assign the non-free variables. That is, if there is a path from i to i+k,
        // then we must assign i as False. Similarly, if there is a path from i+k to i,
        // then we must assign i as True.
        for (int i=0; i<k; i++) {
            if (hasPath(sccs.id[i], sccs.id[i+k], adj2)) {
                assignment[i] = false;
                for (int j=0; j<k; j++) {
                    if (sccs.id[j]==sccs.id[i]) {
                        assignment[j] = false;
                    }
                }
                for (int j=k; j<2*k; j++) {
                    if (sccs.id[j]==sccs.id[i]) {
                        assignment[j-k] = true;
                    }
                }
            }
            if (hasPath(sccs.id[i+k], sccs.id[i], adj2)) {
                assignment[i] = true;
                for (int j=0; j<k; j++) {
                    if (sccs.id[j]==sccs.id[i]) {
                        assignment[j] = true;
                    }
                }
                for (int j=k; j<2*k; j++) {
                    if (sccs.id[j]==sccs.id[i]) {
                        assignment[j-k] = false;
                    }
                }
            }
        }
        // Next, assign the free variables in lexicographically smallest order (preferring false to true).
        for (int i=0; i<k; i++) {
            if (assignment[i]==null) {
                assignment[i] = false;
                for (int j=0; j<k; j++) {
                    if (sccs.id[j]==sccs.id[i]) {
                        if (assignment[j]!=null && assignment[j]) {
                            // Debugging exception. This should not be possible.
                            throw new NullPointerException();
                        }
                        assignment[j] = false;
                    }
                }
                for (int j=k; j<2*k; j++) {
                    if (sccs.id[j]==sccs.id[i]) {
                        if (assignment[j-k]!=null && !assignment[j-k]) {
                            // Debugging exception. This should not be possible.
                            throw new NullPointerException();
                        }
                        assignment[j-k] = true;
                    }
                }
                // Propagate from the component with ~i.
                LinkedList<Integer> q = new LinkedList<>();
                HashSet<Integer> vis = new HashSet<>();
                q.offer(sccs.id[i+k]);
                vis.add(sccs.id[i+k]);
                while (q.size()>0) {
                    int curr = q.poll();
                    for (int j=0; j<adj2.length; j++) {
                        if (adj2[curr][j] && !vis.contains(j)) {
                            vis.add(j);
                            q.offer(j);
                        }
                    }
                }
                for (int j=0; j<k; j++) {
                    if (vis.contains(sccs.id[j])) {
                        if (assignment[j]!=null && !assignment[j]) {
                            // Debugging exception. This should not be possible.
                            throw new NullPointerException();
                        }
                        assignment[j] = true;
                    }
                }
                for (int j=k; j<2*k; j++) {
                    if (vis.contains(sccs.id[j])) {
                        if (assignment[j-k]!=null && assignment[j-k]) {
                            // Debugging exception. This should not be possible.
                            throw new NullPointerException();
                        }
                        assignment[j-k] = false;
                    }
                }
            }
        }
        StringBuilder sb = new StringBuilder();
        for (int i=0; i<k; i++) {
            if (assignment[i]) {
                sb.append("T");
            } else {
                sb.append("F");
            }
        }
        System.out.println(sb.toString());
    }
    static boolean hasPath(int src, int dest, boolean[][] adj) {
        LinkedList<Integer> q = new LinkedList<>();
        q.offer(src);
        boolean[] vis = new boolean[adj.length];
        vis[src] = true;
        while (q.size()>0) {
            int curr = q.poll();
            if (curr == dest) {
                return true;
            }
            for (int i=0; i<adj.length; i++) {
                if (adj[curr][i] && !vis[i]) {
                    q.offer(i);
                    vis[i] = true;
                }
            }
        }
        return false;
    }
}
class Tarjan {
  boolean[][] adj;
  int n, pre, count = 0;
  boolean[] marked;
  int[] id, low;
  Stack<Integer> stack = new Stack<>();
  public Tarjan(boolean[][] adj) {
    n = adj.length; this.adj = adj;
    marked = new boolean[n]; id = new int[n]; low = new int[n];
    for (int u = 0; u < n; u++) {
        if (!marked[u]) {
            dfs(u);
        }
    }
  }
  public void dfs(int u) {
    marked[u] = true; 
    low[u] = pre++; 
    int min = low[u]; 
    stack.push(u);
    for (int v = 0; v < n; v++) {
      if (adj[u][v]) {
        if (!marked[v]) {
            dfs(v);
        }
        if (low[v] < min) {
            min = low[v]; 
        } 
      }
    }
    if (min < low[u]) { 
        low[u] = min; 
        return; 
    }
    int v;
    do { 
        v = stack.pop(); 
        id[v] = count; 
        low[v] = n; 
    } while (v != u);
    count++; 
  } 
}

