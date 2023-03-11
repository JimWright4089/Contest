#!/usr/bin/env python3


def main():
    a = input()
    b = input()
    ans = 1 << sum(x != y for x, y in zip(a, b))

    print(ans)


if __name__ == "__main__":
    main()
