#!/usr/bin/env python3


"""
First, we need to find the length of all shortest missing substrings (SMS). The
first character of our SMS should be a character that doesn't exist in the
string. If this is not possible, then it should be the character whose first
occurrence is last in the string, to eliminate as much of the string as
possible. Then we greedily repeat this process unitl we have an SMS.

Overall runtime O(|S| |V| + \sum |q|)
"""


def main():
    v = input()
    lookup = {c: i for i, c in enumerate(v)}
    s = input()
    n = len(s)

    nxt = [[n for _ in v] for _ in range(n + 1)]
    for i in range(n - 1, -1, -1):
        nxt[i] = list(nxt[i + 1])
        nxt[i][lookup[s[i]]] = i

    sms_len = 0
    ptr = -1
    while ptr < n:
        sms_len += 1
        assert max(nxt[ptr + 1]) > ptr
        ptr = max(nxt[ptr + 1])

    def is_sms(query):
        if len(query) != sms_len:
            return False

        ptr = 0
        for c in query:
            if nxt[ptr][lookup[c]] == n:
                return True
            ptr = nxt[ptr][lookup[c]] + 1

        return False

    q = int(input())
    for _ in range(q):
        query = input()
        if is_sms(query):
            print("1")
        else:
            print("0")


if __name__ == "__main__":
    main()
