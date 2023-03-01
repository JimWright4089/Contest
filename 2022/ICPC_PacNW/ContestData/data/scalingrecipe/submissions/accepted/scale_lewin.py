#!/usr/bin/env python3
n,x,y = map(int, input().split())
for i in range(n):
    a = int(input())
    print(a * y // x)
