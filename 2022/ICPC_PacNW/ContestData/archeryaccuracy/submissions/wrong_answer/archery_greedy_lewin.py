#!/usr/bin/python3
n = int(input())
ps = [float(input()) for i in range(n)]
ss = [0] + [int(input()) for i in range(n)]

ps = sorted(ps)


EPS = 1e-12

def solve_walk(p, k, n):
    # https://digitalcommons.wku.edu/cgi/viewcontent.cgi?article=2504&context=theses
    q = 1.0 - p
    if q > p:
        return 1 - solve_walk(1 - p, n - k, n)
    if abs(q - p) < EPS:
        return k / n

    f = q / p

    return (1.0 - pow(f, k)) / (1.0 - pow(f, n))

def eval(tp):
    prob = 1
    for i in range(n):
        tr0 = solve_walk(tp[i], ss[i+1] + ss[i], ss[i+1] * 2)
        tr1 = solve_walk(tp[i], ss[i+1] - ss[i], ss[i+1] * 2)
        prob = tr0 * prob + tr1 * (1 - prob)
    return prob

best = eval(ps)
for itt in range(1000):
    changed = False
    for i in range(n-1):
        ps[i], ps[i+1] = ps[i+1], ps[i]
        tmp = eval(ps)
        if tmp > best:
            best = tmp
            changed = True
        else:
            ps[i], ps[i+1] = ps[i+1], ps[i]
    if not changed:
        break

print(best)
