using System;
using System.Collections.Generic;


public class blur {
	public static int w,h,b;
	public static int[,] g;

	public static void Main(string[] args) {
		h = nextInt();
		w = nextInt();
		b = nextInt();

		g = new int[w,h];
		for (int i = 0; i < w; i++) for (int j = 0; j < h; j++)
			g[i,j] = nextInt();

		for (int q = 0; q < b; q++) {
			int[,] next = new int[w,h];
			for (int i = 0; i < w; i++)
				for (int j = 0; j < h; j++)
					for (int k = -1; k <= 1; k++)
						for (int m = -1; m <= 1; m++)
							next[i,j] += g[(i+k+w)%w,(j+m+h)%h];
			g = next;
		}

		HashSet<int> hs = new HashSet<int>();
		for (int i = 0; i < w; i++) for (int j = 0; j < h; j++)
			hs.Add(g[i,j]);
		Console.WriteLine(hs.Count);
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