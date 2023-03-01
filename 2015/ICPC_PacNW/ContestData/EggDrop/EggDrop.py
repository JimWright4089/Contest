lin = raw_input().split(" ")
n = int(lin[0])
k = int(lin[1])
lo = 2
hi = k-1
for i in range(n):
   lin = raw_input().split(" ")
   if lin[1] == 'SAFE':
      lo = max(lo, int(lin[0])+1)
   if lin[1] == 'BROKEN':
      hi = min(hi, int(lin[0])-1)
print lo, hi
