import java.util.*;
public class Gears {
	static int[] x, y, r;
	public static void main(String[] args) throws Exception {
		Scanner sc = new Scanner(System.in);
		int n = sc.nextInt();
		x = new int[n];
		y = new int[n];
		r = new int[n];
		for(int i = 0; i < n; i++) {
			x[i] = sc.nextInt();
			y[i] = sc.nextInt();
			r[i] = sc.nextInt();
		}
		LinkedList<Integer> q = new LinkedList<Integer>();
		int[] dp = new int[n];
		dp[0] = 1;
		boolean locked = false;
		q.add(0);
		while(!q.isEmpty()) {
			int curr = q.removeFirst();
			for(int i = 0; i < n; i++) {
				if(i == curr) continue;
				if(connected(curr, i)) {
					if(dp[i] == dp[curr]) {
						locked = true;
					}
					else if(dp[i] == 0) {
						dp[i] = -dp[curr];
						q.add(i);
					}
				}
			}
		}
		if(locked) {
			System.out.println("The input gear cannot move.");
		}
		else if(dp[n-1] == 0) {
			System.out.println("The input gear is not connected to the output gear.");
		}
		else {
			int gcd = gcd(r[n-1], r[0]);
			System.out.println(dp[n-1]*r[0]/gcd + ":" + r[n-1]/gcd);
		}
	}
	public static boolean connected(int i, int j) {
		int xx = x[i] - x[j];
		int yy = y[i] - y[j];
		int rr = xx*xx+yy*yy;
		int other = r[i] + r[j];
		other *= other;
		return rr==other;
	}
	public static int gcd(int a, int b) {
		return a%b==0 ? b : gcd(b, a%b);
	}
}
