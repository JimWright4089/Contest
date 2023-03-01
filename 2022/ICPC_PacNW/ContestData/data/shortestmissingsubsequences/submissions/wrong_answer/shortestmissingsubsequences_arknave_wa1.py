#!/usr/bin/env python3

def main():
    v = input()
    lookup = {c: i for i, c in enumerate(v)}
    s = input()
    n = len(s)

    nxt = [[n for _ in v] for _ in s]
    for i in range(n - 2, -1, -1):
        if i + 1 < n:
            nxt[i] = list(nxt[i + 1])
        nxt[i][lookup[s[i]]] = i

    sms_len = 0
    ptr = 0
    while ptr < n:
        sms_len += 1
        ptr = max(nxt[ptr]) + 1

    def is_sms(query):
        if len(query) != sms_len:
            return False

        ptr = 0
        for c in query:
            if nxt[ptr][lookup[c]] == n:
                return True
            else:
                ptr = nxt[ptr][lookup[c]] + 1

        return False

    q = int(input())
    for _ in range(q):
        query = input()
        if is_sms(query):
            print(1)
        else:
            print(0)


if __name__ == "__main__":
    main()
