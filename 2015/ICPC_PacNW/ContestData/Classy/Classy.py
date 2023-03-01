import sys
who = []
d = dict(upper='1',middle='2',lower='3')
raw_input()
while True:
   try:
      lin = raw_input()
      clas = lin.split(":")[1].split(" ")[::-1]
      key = ''.join(map(lambda s:d[s],clas[1:-1]))
      key = (key + ('2' * 255))[:255] ;
      who.append(key + ":" + lin)
   except (EOFError):
      break
who = sorted(who)
for lin in who:
   print lin.split(":")[1]
