t1 = sorted(map(int, raw_input().split(" ")))
t2 = sorted(map(int, raw_input().split(" ")))
print "YES" if t1 == t2 and t1[0]*t1[0]+t1[1]*t1[1]==t1[2]*t1[2] else "NO"
