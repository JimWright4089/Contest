#include <iostream>
#include <iomanip>
using namespace std ;
const int MAX = 20 ;
int pm[2][MAX][MAX] ;
int cprob[MAX] ;
int n ;
double m ;
double cache[1+(1<<MAX)][MAX+1] ;
double p(int hist, int at, int kleft) {
   if (at == n)
      return hist & 1 ;
   if (kleft + at > n)
      kleft = n - at ;
   if (cache[hist][kleft] >= 0)
      return cache[hist][kleft] ;
   if (at > 0)
      for (int i=at; i<n; i++)
         cprob[i] += pm[hist & 1][at-1][i] ;
   double r = 0 ;
   if (cprob[at] <= -m)
      r = p(2*hist, at+1, kleft) ;
   else if (cprob[at] >= 0)
      r = p(2*hist+1, at+1, kleft) ;
   else {
      double rn0 = 0, rn1 = 0, ry0 = 0, ry1 = 0 ;
      rn0 = p(2*hist, at+1, kleft) ;
      if (kleft > 0)
         ry0 = p(2*hist, at+1, kleft-1) ;
      rn1 = p(2*hist+1, at+1, kleft) ;
      if (kleft > 0)
         ry1 = p(2*hist+1, at+1, kleft-1) ;
      double rn = rn0 + (rn1 - rn0) * (1.0 + cprob[at] / m) ;
      double ry = 0 ;
      if (ry0 < ry1)
         ry = ry0 + (ry1 - ry0) * (1.0 - cprob[at] * cprob[at] / (m*m)) ;
      else
         ry = ry0 + (ry1 - ry0) * ((m + cprob[at]) * (m + cprob[at]) / (m*m)) ;
      if (rn > ry)
         r = rn ;
      else
         r = ry ;
   }
   if (at > 0)
      for (int i=at; i<n; i++)
         cprob[i] -= pm[hist & 1][at-1][i] ;
   return cache[hist][kleft] = r ;
}
int main(int argc, char *argv[]) {
   int k ;
   cin >> n >> k >> m ;
   for (int i=0; i<n; i++) {
      cin >> cprob[i] ;
      for (int j=i+1; j<n; j++)
         cin >> pm[1][i][j] ;
      for (int j=i+1; j<n; j++)
         cin >> pm[0][i][j] ;
   }
   for (int i=0; i<=(1<<n); i++)
      for (int j=0; j<=k; j++)
         cache[i][j] = -1 ;
   double ans = p(1, 0, k) ;
   cout << fixed << setprecision(6) << ans << endl ;
   int residualDigit = int(10000000*ans) % 10;
   if (residualDigit == 4 || residualDigit == 5)
      cerr << "WARNING: " << residualDigit << " in the first unseen place" << endl;
}
