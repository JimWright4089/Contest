import java.io.*;
import java.util.*;
public class Airports {
	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		PrintWriter pw = new PrintWriter(new BufferedWriter(new OutputStreamWriter(System.out)));
		int qq = 1 ; // Integer.parseInt(br.readLine());
		while(qq-- > 0) {
			solve(br, pw);
		}
		pw.close();
	}
	public static void solve(BufferedReader br, PrintWriter pw) throws IOException {
		StringTokenizer st = new StringTokenizer(br.readLine());
		int n = Integer.parseInt(st.nextToken());
		int m = Integer.parseInt(st.nextToken());
		int[] recharge = new int[n];
		st = new StringTokenizer(br.readLine());
		for(int i = 0; i < n; i++) {
			recharge[i] = Integer.parseInt(st.nextToken());
		}
		int[][] grid = new int[n][n];
		int[][] orig = new int[n][n];
		for(int i = 0; i < n; i++) {
			st = new StringTokenizer(br.readLine());
			for(int j = 0; j < n; j++) {
				orig[i][j] = grid[i][j] = Integer.parseInt(st.nextToken());
				if(i != j) {
					grid[i][j] += recharge[j];
				}
			}
		}
		for(int k = 0; k < n; k++) {
			for(int i = 0; i < n; i++) {
				for(int j = 0; j < n; j++) {
					grid[i][j] = Math.min(grid[i][j], grid[i][k] + grid[k][j]);
				}
			}
		}
		int[] s = new int[m];
		int[] f = new int[m];
		int[] t = new int[m];
		for(int i = 0; i < m; i++) {
			st = new StringTokenizer(br.readLine());
			s[i] = Integer.parseInt(st.nextToken())-1;
			f[i] = Integer.parseInt(st.nextToken())-1;
			t[i] = Integer.parseInt(st.nextToken());
		}
		int[][] cost = new int[m][m];
		for(int i = 0; i < m; i++) {
			for(int j = 0; j < m; j++) {
				if(t[i] + orig[s[i]][f[i]] + recharge[f[i]] + grid[f[i]][s[j]] <= t[j]) {
					cost[i][j] = -1;
				}
			}
		}
		pw.println(m + minCostMatching(cost, new int[m], new int[m]));
	}

	public static int minCostMatching(int[][] cost, int[] left, int[] right) {
		int[] u = new int[cost.length];
		int[] v = new int[cost.length];
		for(int i = 0; i < cost.length; i++) {
			u[i] = cost[i][0];
			for(int j = 1; j < cost.length; j++) 
				u[i] = Math.min(u[i], cost[i][j]);
		}
		for(int j = 0; j < cost.length; j++) {
			v[j] = cost[0][j] - u[0];
			for(int i = 1; i < cost.length; i++)
				v[j] = Math.min(v[j], cost[i][j] - u[i]);
		}
		Arrays.fill(left, -1);
		Arrays.fill(right, -1);
		int mated = 0;
		for(int i = 0; i < cost.length; i++) {
			for(int j = 0; j < cost.length; j++) {
				if(right[j] != -1) 
					continue;
				if(Math.abs(cost[i][j] - u[i] - v[j]) < 1e-10) {
					left[i] = j;
					right[j] = i;
					mated++;
					break;
				}
			}
		}
		int[] dist = new int[cost.length];
		int[] dad = new int[cost.length];
		boolean[] seen = new boolean[cost.length];
		while(mated < cost.length) {
			int s = 0;
			while(left[s] != -1) 
				s++;
			Arrays.fill(dad, -1);
			Arrays.fill(seen, false);
			for(int k = 0; k < cost.length; k++) 
				dist[k] = cost[s][k] - u[s] - v[k];
			int j = 0;
			while(true) {
				j = -1;
				for(int k = 0; k < cost.length; k++) {
					if(seen[k]) 
						continue;
					if(j == -1 || dist[k] < dist[j])
						j = k;
				}
				seen[j] = true;
				if(right[j] == -1)
					break;
				int i = right[j];
				for(int k = 0; k < cost.length; k++) {
					if(seen[k])
						continue;
					int newDist = dist[j] + cost[i][k] - u[i] - v[k];
					if(dist[k] > newDist) {
						dist[k] = newDist;
						dad[k] = j;
					}
				}
			}
			for(int k = 0; k < cost.length; k++) {
				if(k == j || !seen[k])
					continue;
				int i = right[k];
				v[k] += dist[k] - dist[j];
				u[i] -= dist[k] - dist[j];
			}
			u[s] += dist[j];
			while(dad[j] >= 0) {
				int d = dad[j];
				right[j] = right[d];
				left[right[j]] = j;
				j = d;
			}
			right[j] = s;
			left[s] = j;
			mated++;
		}
		int value = 0;
		for(int i = 0; i < cost.length; i++)
			value += cost[i][left[i]];
		return value;
	}

}

