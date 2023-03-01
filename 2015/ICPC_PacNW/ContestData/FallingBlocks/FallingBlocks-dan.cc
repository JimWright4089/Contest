#include <assert.h>
#include <stdio.h>
#include <stdint.h>
#include <stdlib.h>
#include <string.h>

#include <algorithm>
#include <unordered_map>

#define NPIECES  8
#define MAXN    20
#define NROWS   10
#define NCOLS    3

typedef uint8_t byte;
typedef uint64_t uint64;

typedef struct piece piece;
struct piece {
  char id;            // T,X,R,S,Z,V,7,W
  byte data[4][3][3]; // 4 orientation x 3 rows x 3 bits
};

piece pieces[NPIECES];
int piecemap[256];

char rawdata[5][NPIECES*4] = {
  "XXX  X   XX  XX XX  X   XX  X  ",
  " X  XXX XX   X   X  X    XX XX ",
  " X   X   X  XX   XX XXX  X   XX",
  "                               ",
  " T   X   R   S   Z   V   7   W ",
};

// batting order
int order[MAXN+1];
int ordersize;

typedef struct state state;
struct state {
  byte ondeck;               // next piece to drop
  byte board[NROWS][NCOLS];
};

// DFS state
std::unordered_map<uint64, int> visited; // 0 = not visited, 1 = visiting, 2 = done
std::unordered_map<uint64, uint64> parent;
std::unordered_map<uint64, int> distance;

void init() {
  int i, j, r, c;
  piece *p;

  for (i = 0; i < NPIECES; i++) {
    p = &pieces[i];
    p->id = rawdata[4][4*i + 1];
    piecemap[(int)p->id] = i;

    for (r = 0; r < 3; r++)
      for (c = 0; c < 3; c++)
	p->data[0][r][c] = (rawdata[r][4*i + c] == 'X') ? 1 : 0;

    // rotate
    for (j = 1; j < 4; j++)
      for (r = 0; r < 3; r++)
	for (c = 0; c < 3; c++)
	  p->data[j][r][c] = p->data[j-1][c][2-r];
  }
}

void getinput() {
  char s[MAXN+1], *p;;

  assert(scanf(" %s", s));
  ordersize = strlen(s);
  for (p = s; *p; p++)
    order[p-s] = piecemap[(int)*p];
}

// pack packs the state into a 64-bit id.
uint64 pack(state s) {
  uint64 id = 0;
  int r, c;

  for (r = 0; r < NROWS; r++)
    for (c = 0; c < NCOLS; c++)
      id |= ((uint64)s.board[r][c]) << (r*3 + c);

  id |= (uint64)s.ondeck << (NROWS*3);

  return id;
}

// unpack is the inverse of pack.
state unpack(uint64 id) {
  state s;
  int r, c;

  memset(&s, 0, sizeof(s));
  s.ondeck = (byte)((id >> (NROWS*3)) & 0xff);
  for (r = 0; r < NROWS; r++)
    for (c = 0; c < NCOLS; c++)
      s.board[r][c] = (id & (1ULL << (r*3 + c))) ? 1 : 0;
  return s;
}

// canplace returns true if we can drop piece (p, rot) at row r.
int canplace(state *s, int row, piece *p, int rot) {
  int r, c;

  assert(row+3 <= NROWS);

  for (r = 0; r < 3; r++)
    for (c = 0; c < 3; c++)
      if (p->data[rot][r][c] && s->board[row+r][c])
	return 0;
  return 1;
}

void place(state *s, int row, piece *p, int rot) {
  int r, c;

  assert(row+3 <= NROWS);

  for (r = 0; r < 3; r++)
    for (c = 0; c < 3; c++)
      if (p->data[rot][r][c])
	s->board[row+r][c] = 1;
}

// isfullrow returns true if row r is full.
int isfullrow(state *s, int r) {
  int c;

  for (c = 0; c < NCOLS; c++)
    if (!s->board[r][c])
      return 0;
  return 1;
}

// nextstate returns the next board state after applying rotation to the ondeck piece.
state nextstate(state s, int rot) {
  piece *p;
  int r, rr, c;

  // Drop the piece until it locks.
  p = &pieces[order[s.ondeck]];
  for (r = 0; r+3 <= NROWS; r++)
    if (!canplace(&s, r, p, rot))
      break;
  r--;
  assert(r >= 0);

  // Lock the piece in place.
  place(&s, r, p, rot);

  // Find rows to clear.
  for (r = rr = NROWS-1; r >= 0; r--) {
    if (isfullrow(&s, r))
      continue;
    for (c = 0; c < NCOLS; c++)
      s.board[rr][c] = s.board[r][c];
    rr--;
  }
  for (; rr >= 0; rr--)
    for (c = 0; c < NCOLS; c++)
      s.board[rr][c] = 0;

  s.ondeck = (s.ondeck + 1) % ordersize;
  return s;
}

// gameover returns true if the gameover condition is met.
int gameover(state s) {
  int r, c;

  for (r = 0; r < 3; r++)
    for (c = 0; c < NCOLS; c++)
      if (s.board[r][c])
	return 1;
  return 0;
}

void visit(uint64 u) {
  state s, t;
  int i;
  uint64 v;

  visited[u] = 1;
  s = unpack(u);
  if (gameover(s)) {
      visited[u] = 2;
      return;
  }

  // There are at most four possibilities, one for each rotation of the next piece.
  for (i = 0; i < 4; i++) {
    t = nextstate(s, i);
    v = pack(t);
    if (!visited[v]) {
      parent[v] = u;
      visit(v);
    } else if (visited[v] == 1) {
      // cycle!
      printf("forever\n");
      exit(0);
    }
    distance[u] = std::max(distance[u], 1 + distance[v]);
  }
  visited[u] = 2;
}

int main() {
  init();
  getinput();
  visit(0);  // The zero state happens to be the starting state of the game.
  printf("%d\n", distance[0]);
}
