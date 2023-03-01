import java.io.*;
import java.util.*;

/**
 * Root the tree and check for distance 3 using a small number of constant time
 * checks using ancestry information.
 * Time complexity: O(n)
 *
 * @author Finn Lidbetter
 */

public class youbethejudge_finn1 {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        int t = Integer.parseInt(br.readLine());
        while(t-- > 0) {
            int n = Integer.parseInt(br.readLine());
            int[] permutation = new int[n];
            int[] parent = new int[n];
            Node[] nodes = new Node[n];
            for (int i=0; i<n; i++) {
                nodes[i] = new Node(i);
            }
            for (int i=0; i<n-1; i++) {
                String[] s = br.readLine().split(" ");
                int u = Integer.parseInt(s[0])-1;
                int v = Integer.parseInt(s[1])-1;
                nodes[u].adj.add(v);
                nodes[v].adj.add(u);
            }
            assignParents(nodes, 0, -1);
            for (int i=0; i<n; i++) {
                permutation[i] = Integer.parseInt(br.readLine())-1;
            }
            boolean valid = true;
            for (int i=0; i<n-1; i++) {
                if (!distAtMost3(nodes, permutation[i], permutation[i+1])) {
                    valid = false;
                    break;
                }
            }
            if (valid) {
                System.out.println("1");
            } else {
                System.out.println("0");
            }
        }
    }
    static boolean distAtMost3(Node[] nodes, int u, int v) {
        if (nodes[u].p==v || nodes[v].p==u) {
            // One is the other's parent (distance 1).
            return true;
        }
        if (false && nodes[u].p==nodes[v].p) {
            // They have a common parent (distance 2).
            return true;
        }
        if ((nodes[u].p!=-1 && nodes[nodes[u].p].p==v) || (nodes[v].p!=-1 && nodes[nodes[v].p].p==u)) {
            // One is the other's grandparent (distance 2).
            return true;
        }
        if ((nodes[u].p!=-1 && nodes[nodes[u].p].p!=-1 && nodes[nodes[nodes[u].p].p].p==v) || 
                (nodes[v].p!=-1 && nodes[nodes[v].p].p!=-1 && nodes[nodes[nodes[v].p].p].p==u)) {
            // One is the other's great grandparent (distance 3).
            return true;
        }
        if ((nodes[u].p!=-1 && nodes[nodes[u].p].p==nodes[v].p) || (nodes[v].p!=-1 && nodes[nodes[v].p].p==nodes[u].p)) {
            // One's parent is the other's grandparent (distance 3).
            return true;
        }
        return false;
    }

    static void assignParents(Node[] nodes, int curr, int prev) {
        nodes[curr].p = prev;
        for (int nbr: nodes[curr].adj) {
            if (nbr!=prev) {
                assignParents(nodes, nbr, curr);
            }
        }
    }
}
class Node {
    int id;
    int p;
    ArrayList<Integer> adj;
    public Node(int idd) {
        id = idd;
        adj = new ArrayList<>();
    }
}
