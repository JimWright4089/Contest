import java.io.*;
import java.util.*;
public class treehopping_xiaowuc1 {
  public static void main(String[] args) throws IOException {
    BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
    int t = Integer.parseInt(br.readLine());
    while(t-- > 0) { 
      int n = Integer.parseInt(br.readLine());
      int[] parent = new int[n];
      int[] depth = new int[n];
      Arrays.fill(depth, n+1);
      ArrayList<Integer>[] edges = new ArrayList[n];
      for(int i = 0; i < n; i++) edges[i] = new ArrayList<>();
      for(int i = 1; i < n; i++) {
        StringTokenizer st = new StringTokenizer(br.readLine());
        int a = Integer.parseInt(st.nextToken())-1;
        int b = Integer.parseInt(st.nextToken())-1;
        edges[a].add(b);
        edges[b].add(a);
      }
      LinkedList<Integer> q = new LinkedList<>();
      q.add(0);
      depth[0] = 0;
      while(!q.isEmpty()) {
        int curr = q.removeFirst();
        for(int out: edges[curr]) {
          if(depth[out] > n) {
            depth[out] = depth[curr] + 1;
            parent[out] = curr;
            q.add(out);
          }
        }
      }
      int[] l = new int[n];
      for(int i = 0; i < n; i++) l[i] = Integer.parseInt(br.readLine());
      int ret = 1;
      for(int i = 0; i + 1 < n; i++) {
        int a = l[i]-1;
        int b = l[i+1]-1;
        int tot = 0;
        while(tot <= 3 && a != b) {
          tot++;
          if(depth[a] >= depth[b]) a = parent[a];
          else b = parent[b];
        }
        if(tot > 3) {
          ret = 0;
          break;
        }
      }
      System.out.println(ret);
    }
  } 
}