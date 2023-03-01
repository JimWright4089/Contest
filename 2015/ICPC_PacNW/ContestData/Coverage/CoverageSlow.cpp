#include <iostream>
#include <vector>
#include <math.h>
using namespace std ;
int verbose = 0 ;
const int OFFSET = 10 ;
struct circ ;
struct circ {
   circ(double x_, double y_) : x(x_), y(y_), ccid(-1) { }
   double x, y ;
   int ccid ;
} ;
double d2(const circ &a, const circ &b) {
   return (a.x-b.x)*(a.x-b.x)+(a.y-b.y)*(a.y-b.y) ;
}
double d2(const circ &a, double x, double y) {
   return (a.x-x)*(a.x-x)+(a.y-y)*(a.y-y) ;
}
int findtangentc(const circ &a, const circ &b, double r, int dir,
                 double &x, double &y) {
   double d = d2(a, b) ;
   if (d > 4 * r * r)
      return 0 ;
   double dx = a.x - b.x ;
   double dy = a.y - b.y ;
   double x0 = 0.5 * (a.x + b.x) ;
   double y0 = 0.5 * (a.y + b.y) ;
   double dp = sqrt(r * r - 0.25 * d) ;
   d = sqrt(d) ;
   x = x0 + dy * dir * dp / d ;
   y = y0 - dx * dir * dp / d ;
   return 1 ;
}
int main(int argc, char *argv[]) {
   int N ;
   cin >> N ;
   vector<circ> cir ;
   for (int i=0; i<N; i++) {
      double x, y ;
      cin >> x >> y ;
      cir.push_back(circ(x, y)) ;
   }
   int seen = 0 ;
   int rover = 0 ;
   int ccid ;
   vector<int> pop ;
   vector<long long> thisset ;
   long long key = 0 ;
   int bestpop = 1 ;
   for (ccid=0; seen<N; ccid++) {
      pop.push_back(0) ;
      thisset.push_back(0) ;
      vector<int> q ;
      while (cir[rover].ccid >= 0)
         rover++ ;
      q.push_back(rover) ;
      cir[rover].ccid = ccid ;
      pop[ccid]++ ;
      int qget = 0 ;
      while (qget < q.size()) {
         int at = q[qget++] ;
         seen++ ;
         const circ &c = cir[at] ;
         // within 2km means each coordinate is +/- 2
         for (int ii=0; ii<cir.size(); ii++) {
            circ &p = cir[ii] ;
            if (p.ccid < 0 && d2(p, c) <= 4.0) {
               p.ccid = ccid ;
               pop[ccid]++ ;
               if (pop[ccid] > bestpop)
                  bestpop = pop[ccid] ;
               q.push_back(ii) ;
            }
         }
      }
   }
   for (int ii=0; ii<cir.size(); ii++)
      for (int jj=ii+1; jj<cir.size(); jj++)
         if (d2(cir[ii], cir[jj]) <= 16.0) {
            for (int dir=-1; dir<=1; dir+=2) {
               double x, y ;
               if (findtangentc(cir[ii], cir[jj], 2.0, dir, x, y)) {
                  key++ ;
                  thisset[cir[ii].ccid] = key ;
                  thisset[cir[jj].ccid] = key ;
                  int thispop = pop[cir[ii].ccid] ;
                  if (cir[ii].ccid != cir[jj].ccid)
                     thispop += pop[cir[jj].ccid] ;
                  for (int kk=0; kk<cir.size(); kk++)
                       if (d2(cir[kk], x, y) <= 4.0) {
                          if (thisset[cir[kk].ccid] != key) {
                             thisset[cir[kk].ccid] = key ;
                             thispop += pop[cir[kk].ccid] ;
                          }
                       }
                  if (thispop > bestpop)
                     bestpop = thispop ;
               }
            }
         }
   cout << (bestpop + 1) << endl ;
}
