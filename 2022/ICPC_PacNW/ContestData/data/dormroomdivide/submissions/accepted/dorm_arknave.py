#!/usr/bin/env python3
import sys

def main():
    n = int(input())
    points = [tuple(map(int, input().split())) for _ in range(n)]
    origin = points[0]
    points = [(x - origin[0], y - origin[1]) for x, y in points]
    areas = [0]
    for i in range(1, n - 1):
        x0, y0 = points[i]
        x1, y1 = points[i + 1]
        areas.append(x0 * y1 - x1 * y0)

    total_area = sum(areas)
    swept_area = 0
    ptr = 1
    while 2 * (swept_area + areas[ptr]) <= total_area:
        swept_area += areas[ptr]
        ptr += 1

    rem_area = total_area - 2 * swept_area
    p = points[ptr]
    v = [points[ptr + 1][k] - points[ptr][k] for k in range(2)]

    t = rem_area / 2.0 / (p[0] * v[1] - p[1] * v[0])
    res = (origin[k] + p[k] + v[k] * t for k in range(2))
    print(*res)

if __name__ == '__main__':
    main()
