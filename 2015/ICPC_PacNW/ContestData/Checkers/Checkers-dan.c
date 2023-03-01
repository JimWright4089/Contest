#include <stdio.h>
#include <string.h>
#include <assert.h>

#define MAXN 26

char board[MAXN][MAXN+1];
int size;

// moves
int dr[] = {1,1,-1,-1};
int dc[] = {1,-1,1,-1};

void getinput() {
  int i;

  assert(scanf(" %d", &size) == 1);
  for (i = 0; i < size; i++) {
    assert(scanf(" %s", board[i]) == 1);
    assert(strlen(board[i]) == size);
    assert(strspn(board[i], "._BW") == size);
  }
}

int inbounds(int r, int c) {
  return r >= 0 && r < size && c >= 0 && c < size;
}

int has_white_neighbor(int r, int c) {
  int i, rr, cc;

  for (i = 0; i < 4; i++) {
    rr = r + dr[i]; cc = c + dc[i];
    if (inbounds(rr, cc) && board[rr][cc] == 'W')
      return 1;
  }
  return 0;
}

int parity(int r, int c) { return ((r/2) + (c/2)) % 2; }

// state for euler/union-find.
int degree[MAXN][MAXN];
int parent[MAXN*MAXN];
int rank[MAXN*MAXN];

int V(int r, int c) { return r*size + c; }

int find(int x) {
  if (parent[x] == -1)
    parent[x] = x;
  else if (parent[x] != x)
    parent[x] = find(parent[x]);
  return parent[x];
}

void link(int x, int y) {
  x = find(x); y = find(y);
  if (x == y) return;
  if (rank[x] < rank[y]) {
    parent[x] = y;
  } else if (rank[y] < rank[x]) {
    parent[y] = x;
  } else {
    parent[y] = x;
    rank[x]++;
  }
}

// Check if graph consists of a single connected component.
int is_connected() {
  int r, c, v, component;

  component = -1;
  for (r = 0; r < size; r++)
    for (c = 0; c < size; c++) {
      v = V(r,c);
      if (parent[v] < 0) continue;
      v = find(v);
      if (component < 0)
	component = v;
      else if (v != component)
	return 0;
    }
  return 1;
}

// Check for euler_path starting at (sr,sc).
int euler_path(int sr, int sc) {
  int r, c, i, rr, cc;
  int nodd = 0;

  memset(degree, 0, sizeof(degree));
  memset(rank, 0, sizeof(rank));
  memset(parent, -1, sizeof(parent));

  for (r = 0; r < size; r++)
    for (c = 0; c < size; c++) {
      if (parity(r,c) != parity(sr,sc)) continue;
      // Find edges in the graph.
      for (i = 0; i < 4; i++) {
	rr = r + dr[i]; cc = c + dc[i];
	if (!inbounds(rr, cc)) continue;
	if (board[rr][cc] != 'W') continue;
	rr = rr + dr[i]; cc = cc + dc[i];

	// A white piece without two open spaces on either side is unjumpable.
	if (board[r][c] == 'B' || !inbounds(rr, cc) || board[rr][cc] == 'B')
	  return 0;

	if (inbounds(rr, cc) && board[rr][cc] == '_') {
	  degree[r][c]++;
	  link(V(r,c), V(rr,cc));
	}
      }
      if (degree[r][c] % 2 == 1)
	nodd++;
    }

  // Check for connectivity first.
  if (!is_connected())
    return 0;

  // Euler path iff all even degree, or two odd, including the start.
  return nodd == 0 || (nodd == 2 && degree[sr][sc] % 2 == 1);
}

int main() {
  int whites[2] = {0};
  int p, r, c, sr, sc, nsols;

  getinput();

  // Are the whites on even or odd rows?
  for (r = 0; r < size; r++)
    for (c = 0; c < size; c++)
      if (board[r][c] == 'W')
	whites[r%2]++;

  // If they're on both, you can't jump 'em all.
  if (whites[0] > 0 && whites[1] > 0) {
    printf("None\n");
    return 0;
  }

  // There are two possibilities for our jumper, parity 0 or 1.
  // Jumping doesn't change the parity. Therefore, we only need to
  // consider the first black king of each parity.
  sr = sc = -1;
  nsols = 0;
  for (p = 0; p < 2; p++) {
    for (r = 0; r < size; r++)
      for (c = 0; c < size; c++)
	if (board[r][c] == 'B' && parity(r, c) == p && has_white_neighbor(r,c)) {
	  // Remove the black king from the board now that we have our
	  // starting point. Any remaining black kings remain as obstacles.
	  board[r][c] = '_';
	  if (euler_path(r, c)) {
	    nsols++;
	    sr = r; sc = c;
	  }
	  // break out of loop
	  r = c = size-1;
	}
  }

  switch (nsols) {
  case 0: printf("None\n"); break;
  case 1: printf("%c%d\n", 'a'+sc, size-sr); break;
  case 2: printf("Multiple\n"); break;
  default: assert(0);
  }
  return 0;
}
