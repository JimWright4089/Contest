#!/usr/bin/python3
s = input()
ss = (s + s)[::-1]
print(int(s[:-1] in ss and s[1:] in ss))
