import java.io.*;
import java.util.*;

/**
 * Simulate the bounces via some case handling.
 *
 * @author Finn Lidbetter
 */

public class squarebounce_finn {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        String[] s = br.readLine().split(" ");
        long a = Long.parseLong(s[0]);
        long b = Long.parseLong(s[1]);
        int n = Integer.parseInt(s[2]);
        Frac ZERO = new Frac(0, 1);
        Frac ONE = new Frac(1, 1);
        Frac MINUS_ONE = new Frac(-1, 1);
        Frac m = new Frac(a,b);
        Frac x = new Frac(-1, 1);
        Frac y = new Frac(0, 1);
        for (int i=0; i<=n; i++) {
            Frac topIntersectX = intersectHorizontal(m, x, y, ONE);
            Frac bottomIntersectX = intersectHorizontal(m, x, y, MINUS_ONE);
            Frac leftIntersectY = intersectVertical(m, x, y, MINUS_ONE);
            Frac rightIntersectY = intersectVertical(m, x, y, ONE);
            
            if (x.compareTo(ONE)==0) {
                if (MINUS_ONE.compareTo(topIntersectX)<0 && topIntersectX.compareTo(ONE)<0) {
                    x = topIntersectX;
                    y = ONE;
                } else if (MINUS_ONE.compareTo(bottomIntersectX)<0 && bottomIntersectX.compareTo(ONE)<0) {
                    x = bottomIntersectX;
                    y = MINUS_ONE;
                } else {
                    x = MINUS_ONE;
                    y = leftIntersectY;
                }
            } else if (x.compareTo(MINUS_ONE)==0) {
                if (MINUS_ONE.compareTo(topIntersectX)<0 && topIntersectX.compareTo(ONE)<0) {
                    x = topIntersectX;
                    y = ONE;
                } else if (MINUS_ONE.compareTo(bottomIntersectX)<0 && bottomIntersectX.compareTo(ONE)<0) {
                    x = bottomIntersectX;
                    y = MINUS_ONE;
                } else {
                    x = ONE;
                    y = rightIntersectY;
                }
            } else if (y.compareTo(ONE) == 0) {
                if (MINUS_ONE.compareTo(leftIntersectY)<0 && leftIntersectY.compareTo(ONE)<0) {
                    x = MINUS_ONE;
                    y = leftIntersectY;
                } else if (MINUS_ONE.compareTo(rightIntersectY)<0 && rightIntersectY.compareTo(ONE)<0) {
                    x = ONE;
                    y = rightIntersectY;
                } else {
                    x = bottomIntersectX;
                    y = MINUS_ONE;
                }
            } else {
                if (MINUS_ONE.compareTo(leftIntersectY)<0 && leftIntersectY.compareTo(ONE)<0) {
                    x = MINUS_ONE;
                    y = leftIntersectY;
                } else if (MINUS_ONE.compareTo(rightIntersectY)<0 && rightIntersectY.compareTo(ONE)<0) {
                    x = ONE;
                    y = rightIntersectY;
                } else {
                    x = topIntersectX;
                    y = ONE;
                }
            }

            m = new Frac(-m.n, m.d);
        }
        System.out.printf("%d %d %d %d\n", x.n, x.d, y.n, y.d);
    }
    static Frac intersectHorizontal(Frac m, Frac x0, Frac y0, Frac y1) {
        Frac b = y0.subtract(m.multiply(x0));
        return y1.subtract(b).divide(m);
    }
    static Frac intersectVertical(Frac m, Frac x0, Frac y0, Frac x1) {
        Frac b = y0.subtract(m.multiply(x0));
        return m.multiply(x1).add(b);
    }
}
class Frac implements Comparable<Frac> {

    long n,d;
    public Frac(long nn, long dd) {
        n = nn;
        d = dd;
        if (d<0) {
            n*=-1;
            d*=-1;
        }
        long gcf = gcd(n,d);
        n /= gcf;
        d /= gcf;
    }

    public Frac add(Frac f2) {
        return new Frac(n*f2.d + d*f2.n, d*f2.d);
    }
    public Frac subtract(Frac f2) {
        return new Frac(n*f2.d - d*f2.n, d*f2.d);
    }
    public Frac multiply(Frac f2) {
        return new Frac(n*f2.n, d*f2.d);
    }
    public Frac divide(Frac f2) {
        return new Frac(n*f2.d, d*f2.n);
    }

    static long gcd(long a, long b) {
        if (a<0) {
            return gcd(-a, b);
        }
        if (b<0) {
            return gcd(a, -b);
        }
        return b==0 ? a : gcd(b, a%b);
    }
    public int compareTo(Frac f2) {
        return (int) Math.signum(n*f2.d - d*f2.n);
    }
}

