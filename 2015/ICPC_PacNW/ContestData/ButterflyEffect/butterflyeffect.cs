using System;
using System.Collections.Generic;

public class butterflyeffect {
	public static int k,n,m;
	public static int[] b;
	public static int[,] p,q;
	public static double[,] dp;

	public static void Main(string[] args) {
		n = nextInt();
		k = nextInt();
		m = nextInt();

		double mm = m*m;

		b = new int[n];
		p = new int[n,n];
		q = new int[n,n];
		for (int i = 0; i < n; i++) {
			b[i] = nextInt();
			for (int j = i+1; j < n; j++) p[i,j] = nextInt();
			for (int j = i+1; j < n; j++) q[i,j] = nextInt();
		}
		dp = new double[1<<n,k+1];
		for (int mask = 0; mask < 1 << n; mask++)
			for (int i = 0; i <= k; i++)
				if (((mask >> (n-1)) & 1) == 1)
					dp[mask,i] = 1;

		for (int which = n-1; which >= 0; which--) {
			double[,] next = new double[1<<which, k+1];
			for (int mask = 0; mask < 1 << which; mask++) {
				for (int left = 0; left <= k; left++) {
					int total = getCurVal(mask, which);

					// normal
					{
						double ret = 0;
						if (total+1 > 0) ret = dp[mask | (1 << which), left];
						else if (total+m <= 0) ret = dp[mask,left];
						else {
							int need = -total;
							double a1 = dp[mask | (1 << which), left];
							double a2 = dp[mask, left];
							ret = (a1 * (m-need) + a2 * need) / (double)m;
						}
						next[mask,left] = Math.Max(next[mask,left], ret);
					}
					// intervene
					if (left > 0) {
						double ret = 0;
						if (total+1 > 0) ret = dp[mask | (1 << which), left-1];
						else if (total+m <= 0) ret = dp[mask,left-1];
						else {
							int need = -total;
							double a1 = dp[mask | (1 << which), left-1];
							double a2 = dp[mask, left-1];

							if (a1 == a2) ret = a1;
							else if (a1 > a2) ret = (a1 * (mm - need * need) + a2 * need * need) / mm;
							else {
								need = m-need;
								ret = (a2 * (mm - need * need) + a1 * need * need) / mm;
							}
						}
						next[mask,left] = Math.Max(next[mask,left], ret);
					}
				}
			}
			dp = next;
		}
		Console.WriteLine(String.Format("{0:F6}", dp[0,k]));
	}

	public static int getCurVal(int mask, int which) {
		int total = b[which];
		for (int k = 0; k < which; k++) total += (((mask >> k) & 1) > 0) ? p[k,which] : q[k,which];
		return total;
	}

	private static string[] __tokens;
	private static int __tidx;
	public static int nextInt() {
		return int.Parse(next());
	}

	public static string next() {
		if (__tokens == null || __tidx == __tokens.Length) {
			__tidx = 0;
			__tokens = Console.ReadLine().Split(' ');
		}
		return __tokens[__tidx++];
	}
}