import sys
dx = [-1, 1, 0, 0]
dy = [0, 0, -1, 1]
lin = raw_input().split(" ")
m = int(lin[0])
n = int(lin[1])
board = []
dist = []
for i in range(m):
   lin = raw_input()
   board.append(lin[:n])
   dist.append([-1] * n)
q = [0, 0]
qg = 0
dist[0][0] = 0
while qg < len(q):
   i = q[qg]
   j = q[qg + 1]
   qg += 2
   odist = dist[i][j]
   mult = int(board[i][j])
   for d in range(4):
      ii = i + dx[d] * mult
      jj = j + dy[d] * mult
      if ii >= 0 and ii < m and jj >= 0 and jj < n and dist[ii][jj] == -1:
         dist[ii][jj] = odist + 1
         q.append(ii)
         q.append(jj)
if dist[m-1][n-1] == -1:
   print "IMPOSSIBLE"
else:
   print dist[m-1][n-1]
