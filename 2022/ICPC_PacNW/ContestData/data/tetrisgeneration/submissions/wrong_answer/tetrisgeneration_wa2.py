#!/usr/bin/env python3

def all_different(s):
    seen = set()
    for c in s:
        if c in seen:
            return False
        seen.add(c)

    return True


def _main():
    s = input()
    n = len(s)

    is_possible = False
    # Does not allow first block to be of length 6
    for k in range(6):
        valid = all_different(s[:k])
        for start in range(k, n, 7):
            valid &= all_different(s[start : start + 7])

        is_possible |= valid

    print(1 if is_possible else 0)

def main():
    t = int(input())
    for _ in range(t):
        _main()

if __name__ == "__main__":
    main()
