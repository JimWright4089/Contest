#!/usr/bin/env python3

EPS = 1e-12


def solve_walk(p, k, n):
    # https://digitalcommons.wku.edu/cgi/viewcontent.cgi?article=2504&context=theses
    q = 1.0 - p
    if abs(q - p) < EPS:
        return k / n

    f = q / p

    return (1.0 - pow(f, k)) / (1.0 - pow(f, n))


def main():
    n = int(input())
    ps = [float(input()) for _ in range(n)]
    ss = [int(input()) for _ in range(n)]

    dp = [0.0 for _ in range(1 << n)]
    dp[0] = 1.0

    for mask in range((1 << n) - 1):
        pc = sum(mask & (1 << i) > 0 for i in range(n))
        cur = ss[pc - 1] if pc > 0 else 0
        goal = ss[pc]

        for i, p in enumerate(ps):
            if mask & (1 << i):
                continue

            # f(0) = 0
            # f(total) = 1
            # f(x) = p f(x - 1) + (1 - p) f(x + 1)

            new_mask = mask | (1 << i)

            prob = 0.0
            prob += solve_walk(p, goal + cur, goal + goal) * dp[mask]
            prob += solve_walk(p, goal - cur, goal + goal) * (1.0 - dp[mask])

            dp[new_mask] = max(dp[new_mask], prob)

    print(dp[-1])


if __name__ == "__main__":
    main()
