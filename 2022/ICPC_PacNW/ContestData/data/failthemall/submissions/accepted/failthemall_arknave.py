#!/usr/bin/env python3

import itertools

"""
For each submission, we get some information. Namely, given two answers from a
student, if one is right then another is wrong. This is basically a series of
implications a la 2-SAT. THe weird part is finding the lex-min 2sat.

Implementation Strategy:
Create a graph with 2k nodes. Question i corresponds to nodes i (false) and i +
k (true). If after adding all the edges, i and i + k are in the same SCC for
any i, the answer is impossible. Otherwise, set the values greedily, and roll
back as needed. Because of the small bounds, we do not have to be too
efficient, O(n^2) should be fine.
"""


def main():
    n, k = map(int, input().split())

    graph = [[] for _ in range(k + k)]
    for _ in range(n):
        sub = input()
        for i, c in enumerate(sub):
            if c == "X":
                continue
            u = i + (k if c == "T" else 0)
            for j, d in enumerate(sub):
                if i == j or d == "X":
                    continue

                v = j + (0 if d == "T" else k)
                graph[u].append(v)

    ans = [None for _ in range(k)]

    def get_closure(u):
        stk = [u]
        closure = set([u])
        while stk:
            cur = stk.pop()
            for v in graph[cur]:
                if v not in closure:
                    closure.add(v)
                    stk.append(v)

        if u + k in closure or u - k in closure:
            return None

        return list(closure)

    def can_enable(u):
        to_enable = get_closure(u)
        if not to_enable:
            return False

        for v in to_enable:
            if v < k and ans[v] == True:
                return False
            if v >= k and ans[v - k] == False:
                return False

        for v in to_enable:
            if v < k:
                ans[v] = False
            else:
                ans[v - k] = True

        return True

    for i in range(k):
        if can_enable(i):
            assert ans[i] == False
        else:
            if can_enable(i + k):
                assert ans[i] == True
            else:
                break

    if any(x is None for x in ans):
        print(-1)
    else:
        print("".join("T" if x else "F" for x in ans))


if __name__ == "__main__":
    main()
