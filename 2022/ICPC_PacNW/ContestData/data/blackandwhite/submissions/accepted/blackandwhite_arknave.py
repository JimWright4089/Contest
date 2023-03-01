#!/usr/bin/env python3

EPS = 1e-10


def main():
    n = int(input())
    p = [float(input()) for _ in range(n)]
    exp_val = [0.0 for _ in range(1 << n)]
    popcount = [0 for _ in range(1 << n)]

    for mask in range(1, len(exp_val)):
        popcount[mask] = popcount[mask ^ (mask & -mask)] + 1
        if popcount[mask] <= 2:
            continue

        # print("Computing mask", mask)
        all_up = 1.0
        all_down = 1.0

        for i in range(n):
            if (mask & (1 << i)) > 0:
                all_up *= p[i]
                all_down *= 1.0 - p[i]

        # print(all_up, all_down)

        # E[7] = 3/4 (1 + E[0]) + 1/4 (E[7] + 1)
        # E[7] = 3/4  + 1/4 E[7] + 1/4

        p_shift = 0.0
        for i in range(n):
            if (mask & (1 << i)) == 0:
                continue

            prob = all_up * (1.0 - p[i]) / p[i] + all_down * p[i] / (1.0 - p[i])
            # print(f'Prob {i} leaves is {prob}')
            p_shift += prob
            exp_val[mask] += prob * (exp_val[mask ^ (1 << i)] + 1.0)

        # print(exp_val[mask], p_shift)
        assert p_shift > EPS
        exp_val[mask] += (1.0 - p_shift)
        exp_val[mask] /= p_shift

    # print(popcount)
    # print(exp_val)
    print(exp_val[-1])


if __name__ == "__main__":
    main()
