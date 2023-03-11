#!/usr/bin/env python3

MOD = int(1e9 + 7)


def next_intersection(x, y, a, b):
    """See the frac version for comments / scratch math."""
    assert (x * x + y * y) % MOD == 1

    t = (MOD - 2) * (x * b + y * a) % MOD * pow(a * a + b * b, MOD - 2, MOD) % MOD

    nx = (x + t * b) % MOD
    ny = (y + t * a) % MOD

    assert (nx * nx + ny * ny) % MOD == 1

    tangent = (-nx, ny)

    dot = (a * tangent[0] + b * tangent[1]) % MOD
    na, nb = (a + (MOD - 2) * dot % MOD * tangent[0]) % MOD, (
        b + (MOD - 2) * dot % MOD * tangent[1]
    ) % MOD

    return nx, ny, na, nb


def main():
    a, b, n = map(int, input().split())
    n += 1

    x, y = MOD - 1, 0

    for _ in range(n):
        x, y, a, b = next_intersection(x, y, a, b)

    assert (x * x + y * y) % MOD == 1

    ans = x.numerator * pow(x.denominator, MOD - 2, MOD) % MOD

    print(ans)


if __name__ == "__main__":
    main()
