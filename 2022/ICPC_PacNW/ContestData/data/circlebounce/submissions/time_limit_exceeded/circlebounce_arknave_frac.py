#!/usr/bin/env python3

from fractions import Fraction

MOD = int(1e9 + 7)


def next_intersection(x, y, a, b):
    """Consider a line intersecting (x, y) with slope a / b. Find the next point of intersection, and the new slope."""
    assert x * x + y * y == Fraction(1)

    # If the slope is a/b, then every time x increases by b, then y increases by a.
    # Line is all points of the form (x + bt, y + at)

    # Find a t > 0 such that
    # (x + bt)^2 + (y + at)^2 = 1
    # x^2 + 2xbt + b^2 t^2 + y^2 + 2yat + a^2 t^2 = 1

    # Because (x, y) is on the circle, we know x^2 + y^2 = 1.
    # 2xbt + b^2 t^2 + 2yat + a^2 t^2 = 0

    # Since t != 0, we can divide through by t to get
    # 2xb + b^2 t + 2ya + a^2 t = 0
    # t = 2(xb + ya) / (a^2 + b^2)

    t = -2 * (x * b + y * a) / (a * a + b * b)

    nx = x + t * b
    ny = y + t * a

    assert nx * nx + ny * ny == Fraction(1)

    # Now we need to compute the new slope. First, compute the tangent slope
    tangent = (-nx, ny)

    # Reflect the slope about that tangent line. Subtract out twice its projection onto the tangent
    dot = a * tangent[0] + b * tangent[1]
    na, nb = a - 2 * dot * tangent[0], b - 2 * dot * tangent[1]

    return nx, ny, na, nb


def main():
    a, b, n = map(int, input().split())
    n += 1

    x, y = Fraction(-1, 1), Fraction(0, 1)

    for _ in range(n):
        x, y, a, b = next_intersection(x, y, a, b)

    assert x * x + y * y == Fraction(1)

    ans = x.numerator * pow(x.denominator, MOD - 2, MOD) % MOD

    print(ans)


if __name__ == "__main__":
    main()
