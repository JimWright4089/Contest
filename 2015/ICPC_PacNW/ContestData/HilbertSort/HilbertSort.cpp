#include <iostream>
#include <vector>
#include <cstring>

using namespace std ;
double sh(double v) {
   return 2 * v + (v < 0 ? 1 : -1) ;
}
struct pt {
   pt(double x_, double y_, const char *id_) : x(x_), y(y_), id(id_) {}
   double x, y ;
   const char *id ;
   int q() const { return (x >= 0) * 2 + (x*y <= 0) ; }
   pt up() const { return pt(sh(y), sh(x), id) ; }
   pt right() const { return pt(sh(x), sh(y), id) ; }
   pt down() const { return pt(sh(-y), sh(-x), id) ; }
} ;
void wander(const vector<pt> &pts) {
   size_t sz = pts.size() ;
   if (sz <= 1) {
      if (sz)
         cout << pts[0].id << endl ;
      return ;
   }
   vector<vector<pt> > q(4) ;
   for (size_t i=0; i<sz; i++) {
      const pt &p = pts[i] ;
      switch (p.q()) {
case 0: q[0].push_back(p.up()) ; break ;
case 1: q[1].push_back(p.right()) ; break ;
case 2: q[2].push_back(p.right()) ; break ;
case 3: q[3].push_back(p.down()) ; break ;
      }
   }
   for (int i=0; i<4; i++)
      wander(q[i]) ;
}
int main(int argc, char *argv[]) {
   int N, S ;
   cin >> N >> S ;
   vector<pt> pts ;
   for (int i=0; i<N; i++) {
      double x, y ;
      string id ;
      cin >> x >> y >> id ;
      pts.push_back(pt(sh(x/S), sh(y/S), strdup(id.c_str()))) ;
   }
   wander(pts) ;
}
