import sys
n = int(raw_input())
q = []
for i in range(n):
   q.append(raw_input().split(" "))
complain = 0
for i in range(1,101):
   s = float(i)
   broken = False
   for j in q:
      if j[0] == 'ADD':
         s += int(j[1])
      if j[0] == 'MULTIPLY':
         s *= int(j[1])
      if j[0] == 'SUBTRACT':
         s -= int(j[1])
      if j[0] == 'DIVIDE':
         s /= int(j[1])
      if s < 0 or int(s) != s:
         broken = True
   if broken:
      complain += 1
print complain
