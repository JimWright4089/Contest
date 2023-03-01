import java.io.*;
import java.util.*;
public class Grid {
	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		PrintWriter pw = new PrintWriter(new BufferedWriter(new OutputStreamWriter(System.out)));
		StringTokenizer st = new StringTokenizer(br.readLine());
		int r = Integer.parseInt(st.nextToken());
		int c = Integer.parseInt(st.nextToken());
		int[][] grid = new int[r][c];
		int[][] dp = new int[r][c];
		for(int i = 0; i < r; i++) {
			String s = br.readLine();
			for(int j = 0; j < c; j++) {
				grid[i][j] = s.charAt(j) - '0';
				dp[i][j] = 1 << 25;
			}
		}
		dp[0][0] = 0;
		LinkedList<State> q = new LinkedList<State>();
		q.add(new State(0, 0));
		while(!q.isEmpty()) {
			State curr = q.removeFirst();
			for(int k = 0; k < dx.length; k++) {
				int nx = curr.x + dx[k] * grid[curr.x][curr.y];
				int ny = curr.y + dy[k] * grid[curr.x][curr.y];
				if(nx < 0 || nx >= r || ny < 0 || ny >= c) continue;
				if(dp[nx][ny] == 1 << 25) {
					dp[nx][ny] = 1 + dp[curr.x][curr.y];
					q.add(new State(nx, ny));
				}
			}
		}
		pw.println(dp[r-1][c-1] == 1 << 25 ? "IMPOSSIBLE" : dp[r-1][c-1]);
		pw.close();
	}

	static int[] dx = new int[]{1,-1,0,0};
	static int[] dy = new int[]{0,0,-1,1};

	static class State {
		public int x,y;
		public State(int a, int b) {
			x=a;
			y=b;
		}
	}
}
