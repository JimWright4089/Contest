using System;
using System.Collections.Generic;


public class airports {
	public static int n,m,vidx;
	public static int[] s,f,t,p,x,vis;
	public static int[,] o,d;

	public static void Main(string[] args) {
		n = nextInt();
		m = nextInt();

		x = new int[n];
		for (int i = 0; i < n; i++) x[i] = nextInt();
		o = new int[n,n];
		d = new int[n,n];
		for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) {
			o[i,j] = nextInt();
			d[i,j] = o[i,j] + (i == j ? 0 : x[j]);
		}

		for (int k = 0; k < n; k++) for (int i = 0; i < n; i++) for (int j = 0; j < n; j++)
			d[i,j] = Math.Min(d[i,j], d[i,k] + d[k,j]);

		s = new int[m];
		f = new int[m];
		t = new int[m];
		vis = new int[m];
		p = new int[m];
		for (int i = 0; i < m; i++) {
			s[i] = nextInt()-1;
			f[i] = nextInt()-1;
			t[i] = nextInt();
			vis[i] = 0;
			p[i] = -1;
		}

		vidx = 0;
		int c = 0;
		for (int i = 0; i < m; i++) {
			++vidx;
			if (dfs(i)) {
				++c;
			}
		}

		Console.WriteLine(m-c);
	}

	public static bool dfs(int u) {
		vis[u] = vidx;
		for (int v = 0; v < m; v++) {
			if (o[s[u],f[u]] + x[f[u]] + d[f[u],s[v]] <= t[v] - t[u] &&
				(p[v] == -1 || (vis[p[v]] != vidx && dfs(p[v])))) {
					p[v] = u;
					return true;
				}
		}
		return false;
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