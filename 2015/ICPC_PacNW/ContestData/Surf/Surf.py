import sys
n = int(raw_input())
q = []
for i in range(n):
   lin = raw_input().split(" ")
   m = int(lin[0])
   f = int(lin[1])
   w = int(lin[2])
   q.append([m, 0, i])
   q.append([m+w, -f, i])
q.sort()
val = 0
st = [0] * n
for i in q:
   if i[1] == 0:
      st[i[2]] = val
   else:
      t = st[i[2]]-i[1]
      if t > val:
         val = t
print val
