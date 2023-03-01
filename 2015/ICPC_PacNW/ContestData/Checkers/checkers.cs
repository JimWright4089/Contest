using System;
using System.Collections.Generic;

public class checkers {
	public static int n, vidx, wc;
	public static char[][] board;
	public static int[,] vis;
	public static int[] dx = {-1, -1, 1, 1},
						dy = {1, -1, 1, -1};

	public static void Main(string[] args) {
		n = nextInt();
		board = new char[n][];
		for (int i = 0; i < n; i++) board[i] = next().ToCharArray();

		wc = 0;
		for (int i = 0; i < n; i++) for (int j = 0; j < n; j++)
			if (board[i][j] == 'W') wc++;

		vidx = 0;
		vis = new int[n,n];
		int bx = -1, by = -1;
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n; j++) {
				if (board[i][j] == 'B') {
					if (ok(i,j)) {
						if (bx != -1) {
							Console.WriteLine("Multiple");
							Environment.Exit(0);
						}
						bx = i;
						by = j;
					}
				}
			}
		}

		if (bx == -1) {
			Console.WriteLine("None");
		} else {
			Console.WriteLine("{0}{1}", ((char)(by+'a')), (n-bx));
		}
	}

	public static bool ok(int x, int y) {
		try {
			board[x][y] = '_';
			int numodd = 0;
			bool isodd = false;
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					if ((i-x) % 2 == 0 && (j-y) % 2 == 0 && ((((i-x)/2) ^ ((j-y)/2)) & 1) == 0) {
						int neighbors = 0;
						for (int k = 0; k < 4; k++) {
							int nx = i+dx[k], ny = j+dy[k];
							int gx = nx+dx[k], gy = ny+dy[k];
							if (nx >= 0 && nx < n && ny >= 0 && ny < n && board[nx][ny] == 'W') {
								if (gx < 0 || gy >= n || gy < 0 || gy >= n || board[gx][gy] != '_')
									return false;
								neighbors++;
							}
						}
						if (neighbors % 2 == 1) numodd++;
						if (i == x && j == y && neighbors % 2 == 1) isodd = true;
					}
				}
			}
			if (numodd > 2 || (numodd == 2 && !isodd)) return false;

			++vidx;
			return count(x,y) == wc;
		} finally {
			board[x][y] = 'B';
		}
	}

	public static int count(int x, int y) {
		int res = 0;
		for (int i = 0; i < 4; i++) {
			int nx = x+dx[i], ny = y+dy[i];
			int gx = nx+dx[i], gy = ny+dy[i];
			if (gx >= 0 && gx < n && gy >= 0 && gy < n 
				&& board[nx][ny] == 'W' && board[gx][gy] == '_' && vis[nx,ny] != vidx) {
				vis[nx,ny] = vidx;
				res += 1 + count(nx+dx[i], ny+dy[i]);
			}
		}
		return res;
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