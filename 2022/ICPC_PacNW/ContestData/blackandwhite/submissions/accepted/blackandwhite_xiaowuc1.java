import java.util.*;

public class blackandwhite_xiaowuc1 {
  public static void main(String[] args) {
    Scanner sc = new Scanner(System.in);
    int n = sc.nextInt();
    double[] p = new double[n];
    for(int i = 0; i < n; i++) p[i] = sc.nextDouble();
    sc.close();
    double[] pos = new double[1<<n];
    double[] neg = new double[1<<n];
    Arrays.fill(pos, 1);
    Arrays.fill(neg, 1);
    for(int mask = 0; mask < (1<<n); mask++) {
      for(int i = 0; i < n; i++) {
        if((mask&(1<<i)) != 0) {
          pos[mask] *= p[i];
          neg[mask] *= 1 - p[i];
        }
      }
    }
    double[] dp = new double[1<<n];
    double[] chance = new double[1<<n];
    chance[chance.length-1] = 1;
    double ret = 0;
    for(int mask = (1<<n)-1; mask >= 0; mask--) {
      if(Integer.bitCount(mask) == 2) ret += dp[mask];
      if(Integer.bitCount(mask) <= 2) continue;
      dp[mask] /= chance[mask];
      double term = 0;
      for(int i = 0; i < n; i++) {
        if((mask&(1<<i)) == 0) continue;
        double up = pos[mask] / p[i] * (1 - p[i]);
        double down = neg[mask] / (1 - p[i]) * p[i];
        term += up + down;
      }
      double gain = 1 / (term);
      for(int i = 0; i < n; i++) {
        if((mask&(1<<i)) == 0) continue;
        double up = pos[mask] / p[i] * (1 - p[i]);
        double down = neg[mask] / (1 - p[i]) * p[i];
        dp[mask ^ (1 << i)] += (dp[mask] + gain) * (up + down) / term * chance[mask];
        chance[mask ^ (1 << i)] += (up + down) / term * chance[mask];
      }
    }
    System.out.println(ret);
  }
}