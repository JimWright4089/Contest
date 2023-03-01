import sys
dx = [-1, -1, 1, 1]
dy = [-1, 1, -1, 1]
def solveone():
   N = int(raw_input())
   board = []
   for i in range(N):
      lin = raw_input()
      board.append(lin[:N])
#
#   Let's find the white checker offset.  All the white checkers
#   need to be at the same x % 2 and y % 2 location or we can't
#   capture them all anyway.
#
   wcx = -1
   wcy = -1
   fail = 0
   whitecheckercount = 0
   for y in range(N):
      for x in range(N):
         if board[y][x] == 'W':
            whitecheckercount += 1
            if wcx < 0:
               wcy = y % 2
               wcx = x % 2
            else:
               if wcy != y % 2 or wcx != x % 2:
                  fail = 1
   if whitecheckercount == 0:
      sys.exit("No white checkers?")
#
#   If all the white checkers are on matching squares, then we
#   can see if there's a solution.  The only black checkers
#   that matter are the ones that are on the opposite parity
#   locations in both x and y.  From these, there are two
#   possibilities:  we use the positions such that (x/2+y/2)
#   are even, and we use the positions such that (x/2+y/2)
#   are odd.  (Every jump maintains this parity.)  We try
#   each in turn.
#
   sols = []
   if not fail:
      for xparity in range(2):
         aritycounts = [0 for i in range(2)]
         blackcheckers = [0 for i in range(2)]
         blackcheckerx = -1
         blackcheckery = -1
         for y in range(1-wcy, N, 2):
            for x in range(1-wcx, N, 2):
               if 1 & (x/2 + y/2 + xparity):
                  arity = 0
                  for d in range(4):
                     x2 = x + 2 * dx[d]
                     y2 = y + 2 * dy[d]
                     if (x2 >= 0 and x2 < N and y2 >= 0 and y2 < N and
                         board[y+dy[d]][x+dx[d]] == 'W'):
                        arity += 1
                  if arity > 0:
                     aritycounts[arity % 2] += 1
                     if board[y][x] == 'B':
                        blackcheckers[arity % 2] += 1
                        blackcheckerx = x
                        blackcheckery = y
#
#   We may have a solution at this point only if:
#      The number of odd parity nodes is 2 or less
#      There is exactly one black checker node
#      If there are any odd parity nodes, the black checker is odd
#
         if (aritycounts[1] <= 2 and
             blackcheckers[0] + blackcheckers[1] == 1 and
             (aritycounts[1] == 0 or blackcheckers[0] == 0)):
#
#   The only remaining concern is connectivity.  We bfs from the
#   black checker and make sure we pick up all white checkers
#   (edges).
#
            seencount = 0
            q = [blackcheckerx, blackcheckery]
            qget = 0
            seen = [[0 for x in range(N)] for y in range(N)]
            seen[blackcheckery][blackcheckerx] = 1
            while qget < len(q):
               x = q[qget]
               y = q[qget+1]
               qget += 2
               for d in range(4):
                  x2 = x + 2 * dx[d]
                  y2 = y + 2 * dy[d]
                  if (x2 >= 0 and x2 < N and y2 >= 0 and y2 < N and
                      board[y+dy[d]][x+dx[d]] == 'W'):
                     seencount += 1
                     if seen[y2][x2] == 0:
                        seen[y2][x2] = 1
                        q.append(x2)
                        q.append(y2)
            if seencount == 2 * whitecheckercount:
               sols.append(blackcheckerx)
               sols.append(blackcheckery)
   if len(sols) == 0:
      print "None"
   elif len(sols) > 2:
      print "Multiple"
   else:
      print chr(ord('a')+sols[0])+str(N-sols[1])

T = 1 ;
for t in range(T):
   solveone()
