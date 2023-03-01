import bisect
lin = raw_input().split(" ")
n = int(lin[0])
r = int(lin[1])
a = []
for i in range(n):
   lin = raw_input().split(" ")
   x = float(lin[0])
   y = float(lin[1])
   a.append([x*r+y, y-x*r])
a.sort()
b = []
for i in range(n):
   y = a[i][1]
   j = bisect.bisect_right(b, y)
   if j >= len(b):
      b.append(y)
   else:
      b[j] = y
print len(b)
