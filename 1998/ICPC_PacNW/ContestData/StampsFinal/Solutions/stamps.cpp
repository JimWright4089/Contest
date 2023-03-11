

/*

  Input:

    Alternating, until EOF,
      - a list of integers, ending with a 0, representing the values of
        postage stamps.

      - a list of integers, ending with 0, representing desired postage
        values

  Output:

    On a separate line for each postage value, print the "best"
    combination of up to 4 stamps for that value, where "best" is:
      - maximum number of different stamps
      - in case of tie, minimum number of total stamps
      - in case of tie, set with highest single-value stamp
      - in case of tie, print "tie".
    if no combination is possible, print "none"

*/

#include <iostream>
#include <vector>
#include <algorithm>
#include <fstream>

using namespace std;

class C {
public:
  int s[4];
  int nd;

  C(int diff, int a, int b = 0, int c = 0, int d = 0) {
    s[0] = a;
    s[1] = b;
    s[2] = c;
    s[3] = d;
    nd = diff;
  }
  
  int ns() {
    for (int i = 0; i < 4; i++) {
      if (s[i] == 0) return i;
    }
    return 4;
  }

  int high() {
    for (int i = 1; i < 4; i++) {
      if (s[i] == 0) return s[i-1];
    }
    return s[3];
  }
};


bool compare(C *a, C *b) {
  if (a->nd > b->nd) return true;
  if (a->nd < b->nd) return false;
  if (a->ns() < b->ns()) return true;
  if (a->ns() > b->ns()) return false;
  if (a->high() > b->high()) return true;
  if (a->high() < b->high()) return false;
  else return true;
}

bool tie(C *a, C *b) {
  return (compare(a, b) && compare(b, a));
}

int nd(int a, int b) {
  if (a == b) return 1;
  return 2;
}

int nd(int a, int b, int c) {
  if (a == b) return nd(b, c);
  return 1 + nd(b, c);
}

int nd(int a, int b, int c, int d) {
  if (a == b) return nd(b, c, d);
  return 1 + nd(b, c, d);
}

bool comp(C* &best, C* &curr, bool tied) {
  if (best == 0) {
    best = curr;
    return false;
  }
  if (compare(best, curr)) {
    if (compare(curr, best)) {
      delete curr;
      return true;
    }
    delete curr;
    return tied;
  }
  delete best;
  best = curr;
  return false;
}

void evaluate(vector<int> stamps, int value) {
  C* best = 0;
  C* curr = 0;
  bool tied = false;

  for (int ia = 0; ia < stamps.size(); ia++) {
    
    if (stamps[ia] == value) {
      curr = new C(1, stamps[ia]);
      tied = comp(best, curr, tied);
      }
    
    for (int ib = ia; ib < stamps.size(); ib++) {
      
      if ((stamps[ia] + stamps[ib]) == value) {
	curr = new C(nd(ia, ib), stamps[ia], stamps[ib]);
	tied = comp(best, curr, tied);
      }
      
      for (int ic = ib; ic < stamps.size(); ic++) {
	
	if ((stamps[ia] + stamps[ib] + stamps[ic]) == value) {
	  curr = new C(nd(ia, ib, ic), stamps[ia], stamps[ib], stamps[ic]);
	  tied = comp(best, curr, tied);
	}
	
	for (int id = ic; id < stamps.size(); id++) {
	  
	  if ((stamps[ia] + stamps[ib] + stamps[ic] + stamps[id]) == value) {
	    curr = new C(nd(ia, ib, ic, id), stamps[ia], stamps[ib],
			       stamps[ic], stamps[id]);
	    tied = comp(best, curr, tied);
	  }
	}
      }
    }
  }

  if (best == 0) {
    cout << " ---- none" << endl;
    return;
  }
      
  if (tied) {
    cout << " (" << best->nd << "): tie" << endl;
    return;
  }

  cout << " (" << best->nd << "): ";
  for (int j = 0; j < 4; j++) {
    if (best->s[j] == 0) break;
    cout << best->s[j] << " ";
  }
  cout << endl;
}


int main() {
  vector<int> *stamps;
  int i;
  fstream infile;
  infile.open("stamps.dat",ios::in);

  while (! infile.eof()) {
    stamps = new vector<int>;

    while(infile >> i) {
      if (infile.eof()) exit(0);
      if (i == 0) break;
      stamps->push_back(i);
    }

    sort(stamps->begin(), stamps->end());

    while(infile >> i) {
      if (infile.eof()) exit(1);
      if (i == 0) break;
      cout << i;
      evaluate(*stamps, i);
    }

    delete stamps;
  }
}

