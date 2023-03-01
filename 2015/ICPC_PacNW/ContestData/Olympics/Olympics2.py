lin = raw_input().split(" ")
e = int(lin[0])
s = int(lin[1])
f = int(lin[2])
if s < f:
   t = s
   s = f
   f = t
ns = (e-1)/s
nf = 0
k = 1.
ss = 0.
while ss < 1e9 and ns > 0:
   ss += k
   if ns * s + nf * f + f < e:
      k += k * ns / (nf + 1)
   else:
      ss += k
      k = k * ns / (nf + 1)
      ns -= 1
   nf += 1
ss += (e + f - 1) / f - nf
if ss>8:
   print("%.6f" % (200./ss))
else:
   print("%.6f" % (225./(ss+1)))
