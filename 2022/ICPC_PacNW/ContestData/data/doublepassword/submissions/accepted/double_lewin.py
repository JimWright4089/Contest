#!/bin/python3
import operator
print(2 ** (sum(map(lambda x: x[0] != x[1], zip(input(), input())))))
