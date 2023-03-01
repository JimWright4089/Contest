import java.util.Arrays;
import java.util.Scanner;

public class RacingGemsAN {
  static class Point implements Comparable<Point> {
    public long x;
    public long y;
    public Point(long x, long y) {this.x = x; this.y = y;}
    
    public int compareTo(Point o) {
      if (this.x == o.x) return Long.compare(this.y, o.y);
      return Long.compare(this.x, o.x);
    }
  }
  
  static int insert(long[] arr, long num, int size) {
    int low = 0, high = size - 1;
    while (low <= high) {
      int mid = (low + high) / 2;
      if (arr[mid] > num) {
        high = mid - 1;
      } else {
        low = mid + 1;
      }
    }
    arr[low] = num;
    return low;
  }
  
  public static void main(String[] args) {
    Scanner s = new Scanner(System.in);
    int n = s.nextInt(), r = s.nextInt();
    s.nextInt(); s.nextInt(); // w and h don't matter
    
    Point[] pts = new Point[n];
    for (int i = 0; i < n; i++) {
      long y = s.nextLong(), x = s.nextLong();
      pts[i] = new Point(x - y * r, x + y * r);
    }
    Arrays.sort(pts);
    
    int ans = 1;
    long[] dp = new long[n];
    dp[0] = pts[0].y;
    for (int i = 1; i < n; i++) {
      ans = Math.max(ans, insert(dp, pts[i].y, ans) + 1);
    }
    
    System.out.println(ans);
  }
}
