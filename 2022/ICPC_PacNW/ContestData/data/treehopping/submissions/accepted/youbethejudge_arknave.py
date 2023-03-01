#!/usr/bin/env python3


def main():
    t = int(input())
    for _ in range(t):
        n = int(input())
        tree = [[] for _ in range(n)]
        for _ in range(n - 1):
            u, v = map(int, input().split())
            u -= 1
            v -= 1
            tree[u].append(v)
            tree[v].append(u)

        perm = [int(input()) - 1 for _ in range(n)]

        par = [-1 for _ in range(n)]
        depths = [-1 for _ in range(n)]
        depths[0] = 0
        stk = [0]
        while stk:
            u = stk.pop()
            for v in tree[u]:
                if depths[v] == -1:
                    par[v] = u
                    depths[v] = depths[u] + 1
                    stk.append(v)

        def dist(u, v):
            moves = 0
            while moves <= 3 and u != v:
                assert 0 <= u < n and 0 <= v < n
                moves += 1
                if depths[u] > depths[v]:
                    u = par[u]
                else:
                    v = par[v]

            return moves

        is_valid = all(dist(u, v) <= 3 for u, v in zip(perm, perm[1:]))
        print(1 if is_valid else 0)


if __name__ == "__main__":
    main()
