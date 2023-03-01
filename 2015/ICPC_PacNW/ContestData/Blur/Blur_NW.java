import java.io.*;
import java.util.*;
public class Blur_NW {
	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		PrintWriter pw = new PrintWriter(new BufferedWriter(new OutputStreamWriter(System.out)));
		StringTokenizer st = new StringTokenizer(br.readLine());
		int c = Integer.parseInt(st.nextToken());
		int r = Integer.parseInt(st.nextToken());
		int qq = Integer.parseInt(st.nextToken());
		double[][] grid = new double[r][c];
		for(int i = 0; i < r; i++) {
			st = new StringTokenizer(br.readLine());
			for(int j = 0; j < c; j++) {
				grid[i][j] = Double.parseDouble(st.nextToken());
			}
		}
		while(qq-- > 0) {
			double[][] next = new double[r][c];
			for(int a = 0; a < r; a++) {
				for(int b = 0; b < c; b++) {
					for(int dx = -1; dx <= 1; dx++) {
						for(int dy = -1; dy <= 1; dy++) {
							next[(a+dx+r)%r][(b+dy+c)%c] += grid[a][b] / 9; 
						}
					}
				}
			}
			grid = next;
		}
		double[] all = new double[r*c];
		int index = 0;
		for(int i = 0; i < r; i++) {
			for(int j = 0; j < c; j++) {
				all[index++] = grid[i][j];
			}
		}
		Arrays.sort(all);
		int ret = 1;
		for(int i = 1; i < all.length; i++) {
			if(all[i] - all[i-1] > 1e-9) {
				ret++;
			}
		}
		pw.println(ret);
		pw.close();
	}
}

