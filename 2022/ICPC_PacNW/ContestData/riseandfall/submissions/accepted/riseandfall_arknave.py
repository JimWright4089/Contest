#!/usr/bin/env python3


def main():
    t = int(input())
    for _ in range(t):
        s = input()
        # find the point of first increase after the point of first decrease
        # flatten the number after that point
        n = len(s)
        dec_point = n
        for i in range(1, n):
            if s[i - 1] > s[i]:
                dec_point = i
                break

        for i in range(dec_point, n):
            if s[i] > s[i - 1]:
                s = s[:i] + "".join(s[i - 1] for _ in s[i:])
                break

        print(s)


if __name__ == "__main__":
    main()
