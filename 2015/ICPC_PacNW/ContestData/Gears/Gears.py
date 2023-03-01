import sys
from fractions import gcd
N = int(raw_input())
gears = []
for i in range(N):
   gears.append([int(j) for j in raw_input().split()]) # x y r
dir = [0 for j in range(N)]
dir[0] = 1
q = []
q.append(0)
qget = 0
locked = False
def dist2(x, y):
   return x * x + y * y
while qget < len(q):
   i = q[qget]
   qget += 1
   for j in range(N):
      if i != j:
         d = dist2(gears[i][0]-gears[j][0], gears[i][1]-gears[j][1])
         rr = dist2(gears[i][2]+gears[j][2], 0)
         if d < rr:
            print "Dist", d
            print "rr", rr
            print gears[i][0], gears[i][1], gears[i][2] ;
            print gears[j][0], gears[j][1], gears[j][2] ;
            sys.exit("Gears not identical but too close")
         elif d == rr:
            if dir[j] == 0:
               dir[j] = - dir[i] ;
               q.append(j)
            elif dir[j] == dir[i]:
               locked = True
if locked:
   print "The input gear cannot move."
elif dir[N-1] == 0:
   print "The input gear is not connected to the output gear."
else:
   g = gcd(gears[0][2], gears[N-1][2])
   print str(dir[N-1]*gears[0][2]/g)+":"+str(gears[N-1][2]/g)
