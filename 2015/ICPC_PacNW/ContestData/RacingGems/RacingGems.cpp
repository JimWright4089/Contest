#include <iostream>
#include <vector>
#include <algorithm>
using namespace std ;
typedef long long int64 ;
typedef pair<int64,int64> pt ;
int main(int argc, char *argv[]) {
   int N, W, H, R ;
   cin >> N >> R >> W >> H ;
   vector<pt> a ;
   for (int i=0; i<N; i++) {
      int64 x, y ;
      cin >> x >> y ;
      a.push_back(make_pair(x*R+y, y-x*R)) ;
   }
   sort(a.begin(), a.end()) ;
   vector<int64> start ;
   for (int i=0; i<a.size(); i++) {
      int64 y = a[i].second ;
      vector<int64>::iterator ind = upper_bound(start.begin(), start.end(), y) ;
      if (ind == start.end())
         start.push_back(y) ;
      else
         *ind = y ;
   }
   cout << start.size() << endl ;
}
