import sys
N = int(raw_input())
strength = []
for i in range(N):
   strength.append(int(raw_input()))
strength = sorted(strength)
r = 1000000000
for i in range(N/2):
   r = min(r, strength[i]+strength[N-1-i])
print r
