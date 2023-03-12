#include <iostream>
#include <vector>
#include <algorithm>
#include <cmath>

using namespace std;

double INF = 1e100;
double EPS = 1e-12;

struct PT { 
  double x, y; 
  PT() {}
  PT(double x, double y) : x(x), y(y) {}
  PT(const PT &p) : x(p.x), y(p.y)    {}
  PT operator-(const PT &p)  const { return PT(x-p.x,y-p.y); }
  PT operator+(const PT &p)  const { return PT(x+p.x,y+p.y); }
  PT operator*(double c)     const { return PT(x*c,y*c); }
  PT operator/(double c)     const { return PT(x/c,y/c); }
};

struct EVT {
  double angle;
  bool start;
  EVT(double angle = 0, bool start = false) : angle(angle), start(start) {}
  bool operator<(const EVT &o) const {return angle < o.angle;}
};

double dot(PT p, PT q)     { return p.x*q.x+p.y*q.y; }
double dist2(PT p, PT q)   { return dot(p-q,p-q); }
double cross(PT p, PT q)   { return p.x*q.y-p.y*q.x; }
ostream &operator<<(ostream &os, const PT &p) { 
  os << "(" << p.x << "," << p.y << ")"; 
}

PT RotateCCW90(PT p)   { return PT(-p.y,p.x); }
PT RotateCW90(PT p)    { return PT(p.y,-p.x); }

// compute intersection of circle centered at a with radius r
// with circle centered at b with radius R

vector<PT> CircleCircleIntersection(PT a, PT b, double r, double R) {
  vector<PT> ret;
  double d = sqrt(dist2(a,b));
  if (d > r+R || d+min(r,R) < max(r,R)) return ret;
  double x = (d*d-R*R+r*r)/(2*d);
  double y = sqrt(r*r-x*x);
  PT v = (b-a)/d;
  ret.push_back(a+v*x + RotateCCW90(v)*y);
  if (y > 0) 
    ret.push_back(a+v*x - RotateCCW90(v)*y);
  return ret;
}

// start, end, start, end, going CCW
vector<PT> Intervals(PT a, PT b, double r, double R) {
  vector<PT> ret;
  if (abs(r - R) < 1e-3) {
    vector<PT> internal = CircleCircleIntersection((a+b)/2, b, sqrt(dist2(a,b))/2, R + r);
    ret.push_back(b - internal[1]);
    ret.push_back(RotateCCW90(b-a));
    ret.push_back(RotateCW90(b-a));
    ret.push_back(b - internal[0]);
  } else if (r < R) {
    vector<PT> external = CircleCircleIntersection((a+b)/2, b, sqrt(dist2(a,b))/2, R - r);
    vector<PT> internal = CircleCircleIntersection((a+b)/2, b, sqrt(dist2(a,b))/2, R + r);
    ret.push_back(b - internal[1]);
    ret.push_back(external[0] - b);
    ret.push_back(external[1] - b);
    ret.push_back(b - internal[0]);
  } else {
    vector<PT> external = CircleCircleIntersection((a+b)/2, a, sqrt(dist2(a,b))/2, r - R);
    vector<PT> internal = CircleCircleIntersection((a+b)/2, a, sqrt(dist2(a,b))/2, r + R);
    ret.push_back(internal[1] - a);
    ret.push_back(external[1] - a);
    ret.push_back(external[0] - a);
    ret.push_back(internal[0] - a);
  }
  
  return ret;
}

PT center[4096];
double radius[4096];

int main() {
  int t;
  cin >> t;
  for (int tc = 0; tc < t; tc++) {
    int n;
    cin >> n;
    
    for (int i = 0; i < n; i++) cin >> center[i].x >> center[i].y >> radius[i];
//    for (int i = 0; i < n; i++) radius[i] += 1e-6;
    
    if (n <= 2) {
      cout << n << endl;
      continue;
    }
    
    int best = 0;
    int badcount = 0;
    for (int i = 0; i < n; i++) {
      int cur = 1;
      vector<EVT> events;
      for (int j = 0; j < n; j++) {
        if (i == j) continue;
        vector<PT> intervals = Intervals(center[i], center[j], radius[i], radius[j]);
        events.push_back(EVT(atan2(intervals[0].y, intervals[0].x), true));
        events.push_back(EVT(atan2(intervals[1].y, intervals[1].x), false));
        if (events[events.size() - 2].angle > 0 && events[events.size() - 1].angle < 0) cur++;
        events.push_back(EVT(atan2(intervals[2].y, intervals[2].x), true));
        events.push_back(EVT(atan2(intervals[3].y, intervals[3].x), false));
        if (events[events.size() - 2].angle > 0 && events[events.size() - 1].angle < 0) cur++;
      }
      
      sort(events.begin(), events.end());
      
      for (int j = 0; j < events.size(); j++) {
        if (events[j].start) cur++; else cur--;
        if (cur > best) best = cur;
      }
    }
    
    cout << best << endl;
  }

  return 0;
}

