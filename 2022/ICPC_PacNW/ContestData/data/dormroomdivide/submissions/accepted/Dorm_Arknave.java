import java.io.*;
import java.util.*;

public class Dorm_Arknave {
    public static void main(String[] args) throws IOException {
        Scanner scan = new Scanner(System.in);
        int n = scan.nextInt();
        long[] x = new long[n];
        long[] y = new long[n];
        for (int i = 0; i < n; i++) {
            x[i] = scan.nextInt();
            y[i] = scan.nextInt();
        }

        long origX = x[0];
        long origY = y[0];
        for (int i = 0; i < n; i++) {
            x[i] -= origX;
            y[i] -= origY;
        }

        // double the signed area
        long[] areas = new long[n];
        long totalArea = 0;
        for (int i = 1; i + 1 < n; i++) {
            int j = i + 1;
            areas[i] = x[i] * y[j] - x[j] * y[i];
            totalArea += areas[i];
        }

        long halfArea = 0;
        int ptr = 1;
        while (2 * (halfArea + areas[ptr]) <= totalArea) {
            halfArea += areas[ptr++];
        }
        // still doubled
        long goalArea = totalArea - 2 * halfArea;
        assert areas[ptr] >= goalArea;
        long dx = x[ptr];
        long dy = y[ptr];

        long dvx = x[ptr + 1] - x[ptr];
        long dvy = y[ptr + 1] - y[ptr];

        // dx * (dy + t dvy) - dy * (dx + t dvx) = a
        // dx * dy + t dx dvy - dy * dx - t dy dvx = a
        // t dx dvy - t dy dvx = a
        // t (dx dvy - dy dvx) = a
        double t = goalArea / 2.0 / (dx * dvy - dy * dvx);
        double px = dx + dvx * t;
        double py = dy + dvy * t;

        System.out.printf("%.15f %.15f%n", origX + px, origY + py);
    }
}
