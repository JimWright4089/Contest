n = int(raw_input())
a = []
for i in range(n):
   a.append(raw_input().split(" ")[::-1])
a.sort()
for i in a:
   print " ".join(i[::-1])
