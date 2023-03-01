#include <algorithm>
#include <iostream>
#include <vector>
#include <cassert>
#include <sys/time.h>
using namespace std ;
struct ordered {
   ordered(int k1_, int k2_, int fk_) : k1(k1_), k2(k2_), fk(fk_) {}
   ordered() : k1(0), k2(0), fk(0) {}
   bool operator<(const ordered &o) const {
      if (k1 != o.k1)
         return k1 < o.k1 ;
      if (k2 != o.k2)
         return k2 < o.k2 ;
      return fk < o.fk ;
   }
   int k1, k2, fk ;
} ;
vector<int> findneighbors(vector<ordered> &byk, int at, int R) {
   vector<int> neighbors ;
   for (int i=at-1; i>=0; i--) {
      if (byk[at].k1 != byk[i].k1 || byk[at].k2 - byk[i].k2 >= 2 * R + 1)
         break ;
      neighbors.push_back(byk[i].fk) ;
   }
   for (int i=at+1; i<byk.size(); i++) {
      if (byk[at].k1 != byk[i].k1 || byk[i].k2 - byk[at].k2 >= 2 * R + 1)
         break ;
      neighbors.push_back(byk[i].fk) ;
   }
   return neighbors ;
}
int main(int argc, char *[]) {
   int R, L ;
   cin >> R ;
   cin >> R >> L ;
   vector<int> xa, ya, b ;
   vector<ordered> byx, byy ;
   // b:  -1=undefined, 0=horizontal, 1=vertical
   for (int i=0; i<L; i++) {
      int x, y ;
      cin >> y >> x ;
      xa.push_back(x) ;
      ya.push_back(y) ;
      byx.push_back(ordered(x, y, i)) ;
      byy.push_back(ordered(y, x, i)) ;
      b.push_back(-1) ;
   }
   sort(byx.begin(), byx.end()) ;
   sort(byy.begin(), byy.end()) ;
   vector<int> tobyx(L), tobyy(L) ;
   for (int i=0; i<L; i++) {
      tobyx[byx[i].fk] = i ;
      tobyy[byy[i].fk] = i ;
   }
   int outerfail = 0 ;
   srand48(time(0)) ;
   vector<int> order(L) ;
   for (int i=0; i<L; i++)
      order[i] = i ;
   for (int ii=0; !outerfail && ii<L; ii++) {
      int off = ii + (int)((L-ii)*drand48()) ;
      swap(order[off], order[ii]) ;
      int i = order[ii] ;
      if (b[i] < 0) {
         int foundgood = 0 ;
         for (int tv=0; tv<2; tv++) {
            int fail = 0 ;
            vector<int> q ;
            b[i] = tv ;
            q.push_back(i) ;
            int qg = 0 ;
            while (!fail && qg < q.size()) {
               int j = q[qg++] ;
               vector<int> neighbors ;
               assert(b[j]>=0) ;
               assert(byx[tobyx[j]].fk == j) ;
               assert(byy[tobyy[j]].fk == j) ;
               if (b[j] == 0)
                  neighbors = findneighbors(byy, tobyy[j], R) ;
               else
                  neighbors = findneighbors(byx, tobyx[j], R) ;
// cout << "Set " << j << " to " << b[j] << ":" ;
// for (auto k : neighbors) cout << " " << k ;
// cout << endl ;
               for (auto k : neighbors) {
                  if (b[k] == -1) {
                     b[k] = 1 - b[j] ;
                     q.push_back(k) ;
                  } else if (b[k] == b[j]) {
                     fail = 1 ;
                  } else {
                     // okay, nothing to do here
                  }
               }
            }
// cout << (fail ? "Fail" : "Succeed") << endl ;
            if (fail) {
               for (int k=0; k<q.size(); k++)
                  b[q[k]] = -1 ;
            } else {
               foundgood = 1 ;
               break ;
            }
         }
         if (!foundgood) {
// cout << "Setting outerfail" << endl ;
            outerfail = 1 ;
            break ;
         }
      }
   }
   if (outerfail)
      cout << "NO" << endl ;
   else
      cout << "YES" << endl ;
   if (argc <= 1)
      exit(0) ;
// print the solution we found, if any, or where we died.
   // draw it
   for (int i=0; i<L; i++)
      cout << " " << b[i] ;
   cout << endl ;
   int hin = 0 ;
   for (int i=0; i<L; i++)
      hin = max(hin, max(xa[i], ya[i])) ;
   vector<string> board ;
   for (int i=0; i<=hin; i++) {
      board.push_back(string()) ;
      for (int j=0; j<=hin; j++)
         board[i].push_back('.') ;
   }
   for (int i=0; i<L; i++) {
      int x = xa[i] ;
      int y = ya[i] ;
      if (b[i] > -1) {
         for (int j=-R; j<=R; j++)
            if (b[i] == 0) {
               if (x + j >= 0 && x + j <= hin)
                  board[y][x+j] = '*' ;
            } else {
               if (y + j >= 0 && y + j <= hin)
                  board[y+j][x] = '*' ;
            }
      }
   }
   for (int i=0; i<L; i++)
      if (b[i] < 0)
         board[ya[i]][xa[i]] = '?' ;
      else if (b[i] == 0)
         board[ya[i]][xa[i]] = '-' ;
      else
         board[ya[i]][xa[i]] = '|' ;
   for (int i=0; i<=hin; i++)
      cout << board[i] << endl ;
}
