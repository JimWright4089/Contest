using System;
using System.Collections.Generic;

public class checkers {
	public static int n;
	public static string[][] names;

	public static void Main(string[] args) {
		n = nextInt();
		names = new string[n][];
		for (int i = 0; i < n; i++) {
			names[i] = new string[2];
			names[i][0] = next();
			names[i][1] = next();
		}

		Array.Sort(names, delegate(string[] a, string[] b) {
			return a[1].CompareTo(b[1]) == 0 ?
				   a[0].CompareTo(b[0]) :
				   a[1].CompareTo(b[1]);
		});

		for (int i = 0; i < n; i++) {
			Console.WriteLine("{0} {1}", names[i][0], names[i][1]);
		}
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