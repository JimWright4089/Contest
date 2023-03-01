lin = raw_input().split(" ")
n = int(lin[0])
S = int(lin[1])
a = []
for i in range(n):
   lin = raw_input().split(" ")
   x = int(lin[0])
   y = int(lin[1])
   id = lin[2]
   key = ''
   for j in range(32):
      x *= 2
      y *= 2
      if y > S:
         y -= S
         if x > S:
            key = key + '2' ;
            x -= S
         else:
            key = key + '1' ;
      else:
         t = x 
         x = y
         y = t
         if y > S:
            key = key + '3' ;
            x = S - x
            y = 2 * S - y
         else:
            key = key + '0' ;
   a.append(key + " " + id)
a.sort()
for i in range(n):
   print a[i].split(" ")[1]
