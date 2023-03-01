#!/usr/bin/env python3

from fractions import Fraction

"""
Shift the problem so the square goes from (0, 0) to (2, 2)
and the ray begins at (0, 1) and has slope a / b.

Consider shooting the ray outside the boundaries of the box. The number of
reflections at that point is the number of lines the ray crosses of the form x
= 2k or y = 2k for some non-negative integer k. Because we know the bullet has
to end on the boundary of the square, we can compute how many bounces it takes
to reach a specific line.
"""


def fix_coord(x):
    """Move back into range [0, 2]"""
    x %= 4
    if x >= 2:
        x = 4 - x

    return x


def print_ans(x, y):
    x = Fraction(x)
    y = Fraction(y)

    x = fix_coord(x)
    y = fix_coord(y)

    x -= 1
    y -= 1

    print(x.numerator, x.denominator, y.numerator, y.denominator)


def main():
    a, b, n = map(int, input().split())
    n += 1

    # slope of the line is y = ax/b + 1
    for x_line in range(2, 2 * n + 1, 2):
        y = Fraction(a * x_line, b) + 1
        crossed = x_line // 2 + int(y) // 2
        # print(x_line, y, crossed)
        if crossed == n:
            print_ans(x_line, y)
        elif crossed > n:
            break

    # y = ax/b + 1
    # y - 1 = ax / b
    # x = (b y - b) / a
    for y_line in range(2, 2 * n + 1, 2):
        x = Fraction(b * y_line - b, a)
        crossed = int(x) // 2 + y_line // 2
        # print(x, y_line, crossed)
        if crossed == n:
            print_ans(x, y_line)
        elif crossed > n:
            break


if __name__ == "__main__":
    main()
