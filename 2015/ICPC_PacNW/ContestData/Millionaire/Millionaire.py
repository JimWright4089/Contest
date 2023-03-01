import sys, math
lin = raw_input().split(" ")
N = int(lin[0])
W = float(lin[1])
maxval = 0
phere = 1.
lastsafe = 0.
valsofar = 0.
def mapit(v):
   return math.log(1+v/W)
def unmapit(v):
   return (math.exp(v)-1)*W
for i in range(N):
   lin = raw_input().split(" ")
   pi = float(lin[1])
   vi = mapit(float(lin[2]))
   valsofar += phere * (1 - pi) * lastsafe
   testval = valsofar + phere * pi * vi
   phere *= pi
   if testval > maxval:
      maxval = testval
   if lin[0] == 'safe':
      lastsafe = vi
print '$' + ("%.2f" % unmapit(maxval))
