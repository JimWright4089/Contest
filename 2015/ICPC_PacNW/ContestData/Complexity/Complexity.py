h = [0] * 128
s = raw_input()
for c in s:
   h[ord(c)] += 1
h.sort()
print len(s)-h[127]-h[126]
