#!/usr/bin/env python3


def max_match(a, b, k):
    ans = 0
    for x in a:
        if x + k >= b[ans]:
            ans += 1

    return ans


def main():
    n, k = map(int, input().split())
    a = [int(input()) for _ in range(1 << n)]
    a.sort()

    ans = 0
    while len(a) > 1:
        h = len(a) // 2
        losers, winners = a[:h], a[h:]

        ans += max_match(losers, winners, k)

        a = winners

    print(ans)

if __name__ == "__main__":
    main()
