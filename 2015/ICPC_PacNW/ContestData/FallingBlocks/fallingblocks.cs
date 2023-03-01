using System;
using System.Collections.Generic;

public class fallingblocks {
	public static string seq;
	public static string[] s = {
		"XXX X  XX XXXX X  XX X  ",
		" X XXXXX  X  X X   XXXX ",
		" X  X  X XX  XXXXX X  XX"
	};
	public static string labels = "TXRSZV7W";
	public static int[,,] x;
	public static int[,] dp;
	public static int INF = 1 << 29;

	public static void parsePieces() {
		x = new int[8,2,5];
		for (int i = 0; i < 8; i++) {
			int cx = 1, cy = 1+3*i;
			int idx = 0;
			for (int j = -1; j <= 1; j++) {
				for (int k = -1; k <= 1; k++) {
					if (s[j+cx][k+cy] == 'X') {
						x[i,0,idx] = j;
						x[i,1,idx] = k;
						++idx;
					}
				}
			}
		}
	}

	public static void rotate(int which) {
		for (int i = 0; i < 5; i++) {
			int t = x[which,1,i];
			x[which,1,i] = x[which,0,i];
			x[which,0,i] = -t;
		}
	}

	public static int[,] rot;
	public static void Main(string[] args) {
		parsePieces();

		seq = next();
		dp = new int[1<<21,seq.Length];
		for (int i = 0; i < 1 << 21; i++)
			for (int j = 0; j < seq.Length; j++)
				dp[i,j] = -1;

		int res = solve(0, 0);
		if (res >= INF) Console.WriteLine("forever");
		else Console.WriteLine(res);

	}

	public static int solve(int mask, int where) {
		if (dp[mask,where] != -1) return dp[mask,where];
		dp[mask,where] = INF;
		int res = 1;
		int which = labels.IndexOf(seq[where]);
		for (int i = 0; i < 4; i++) {
			int nmask = getNext(which, mask);
			if (nmask >= 0) {;
				int m = 1+solve(nmask, (where+1) % seq.Length);
				if (m > res) {
					res = m;
				}
			}
			rotate(which);
		}
		return dp[mask,where] = res;
	}


	public static int getNext(int which, int mask) {
		int i;
		for (i = 7; i >= 1; i--) {
			bool ok = true;
			for (int j = 0; j < 5; j++) {
				int cx = i+x[which,0,j], cy = 1+x[which,1,j];
				int c = cx*3 + cy;
				if (((mask >> c) & 1) == 1) {
					ok = false;
					break;
				}
			}
			if (!ok) break;
		}
		i++;
		long nmask = mask;
		for (int j = 0; j < 5; j++) {
			int cx = i+x[which,0,j], cy = 1+x[which,1,j];
			int c = cx*3 + cy;
			nmask |= 1L << c;
		}
		for (int j = 0; j <= 9; j++) {
			if (((nmask >> (3 * j)) & 7) == 7) {
				long top = nmask >> (3 * (j+1));
				long bottom = nmask & ((1L << (3 * j)) - 1);
				nmask = bottom | (top << (3 * j));
				j--;
			}
		}
		if (nmask >= 1L << 21) return -1;
		return (int)nmask;
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
