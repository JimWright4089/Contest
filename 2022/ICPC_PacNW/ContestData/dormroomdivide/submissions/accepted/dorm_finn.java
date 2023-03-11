import java.io.*;
import java.util.*;

/**
 * Binary search on how far around the polygon to split in two. 
 * Use cross products to get areas of triangles that make up the polygon.
 *
 * Time complexity: O(n log (1/EPS))
 *
 * @author Finn Lidbetter
 */

public class dorm_finn {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        int n = Integer.parseInt(br.readLine());
        long[] x = new long[n];
        long[] y = new long[n];
        for (int i=0; i<n; i++) {
            String[] s = br.readLine().split(" ");
            x[i] = Long.parseLong(s[0]);
            y[i] = Long.parseLong(s[1]);
        }
        double length = 0;
        double[] prefix = new double[n+1];
        for (int i=1; i<n; i++) {
            long dx = x[i]-x[i-1];
            long dy = y[i]-y[i-1];
            length += Math.sqrt(dx*dx + dy*dy);
            prefix[i] = length;
        }
        long lastDx = x[0]-x[n-1];
        long lastDy = y[0]-y[n-1];
        length += Math.sqrt(lastDx*lastDx + lastDy*lastDy);
        prefix[n] = length;
        double area = area(x, y);
        double target = area/2.0;
        double lo = prefix[1];
        double hi = prefix[n-1];
        double EPS = 0.000000001;
        int count = 0;
        double bestx = 0;
        double besty = 0;
        while (hi-lo>EPS && count<200) {
            double mid = (lo+hi)/2.0;
            int limit = 0;
            while (mid > prefix[limit]) {
                limit++;
            }
            double vx = x[limit]-x[limit-1];
            double vy = y[limit]-y[limit-1];
            double frac = (mid-prefix[limit-1])/(prefix[limit]-prefix[limit-1]);
            double px = x[limit-1] + vx*frac;
            double py = y[limit-1] + vy*frac;
            double midArea = partialArea(x,y, px, py, limit);
            if (midArea>target) {
                hi = mid;
            } else {
                lo = mid;
            }


            bestx = px;
            besty = py;
            if (Math.abs(midArea-target)<EPS) {
                break;
            }
            count++;
        }
        System.out.println(bestx + " " + besty);
    }

    static double partialArea(long[] x, long[] y, double px, double py, int limit) {
        double twiceArea = 0;
        for (int i=1; i<limit-1; i++) {
            long v1x = x[i]-x[0];
            long v1y = y[i]-y[0];
            long v2x = x[i+1]-x[0];
            long v2y = y[i+1]-y[0];
            long cross = v1x*v2y - v2x*v1y;
            if (cross<0) {
                cross *= -1;
            }
            twiceArea += cross;
        }
        double v1px = x[limit-1] - x[0];
        double v1py = y[limit-1] - y[0];
        double v2px = px - x[0];
        double v2py = py - y[0];
        double pCross = v1px*v2py - v2px*v1py;
        if (pCross<0) {
            pCross*=-1;
        }
        twiceArea += pCross;
        return (double)twiceArea/2.0;

    }
    static double area(long[] x, long[] y) {
        int n = x.length;
        long twiceArea = 0;
        for (int i=1; i<n-1; i++) {
            long v1x = x[i]-x[0];
            long v1y = y[i]-y[0];
            long v2x = x[i+1]-x[0];
            long v2y = y[i+1]-y[0];
            long cross = v1x*v2y - v2x*v1y;
            if (cross<0)
                cross *= -1;
            twiceArea += cross;
        }
        return (double)twiceArea/2.0;
    }
}
