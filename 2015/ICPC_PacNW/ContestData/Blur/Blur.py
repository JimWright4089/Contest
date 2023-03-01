lin = raw_input().split(" ")
w = int(lin[0])
h = int(lin[1])
b = int(lin[2])
a = []
t = []
for i in range(h):
   a.append(map(int, raw_input().split(" ")))
   t.append([0] * w)
for bb in range(b):
   for i in range(h):
      for j in range(w):
         t[i][j] = a[(i+1)%h][j]+a[i][j]+a[(i+h-1)%h][j]
   for i in range(h):
      for j in range(w):
         a[i][j] = t[i][(j+1)%w]+t[i][j]+t[i][(j+w-1)%w]
u = {}
for i in range(h):
   for j in range(w):
      u[str(a[i][j])] = 1
print len(u)
