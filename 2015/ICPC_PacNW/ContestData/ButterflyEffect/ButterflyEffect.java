import java.util.Scanner;
import java.text.DecimalFormat;

public class ButterflyEffect {
  enum Outcome {POS, NEG};
  enum Intervention {HI, LOW, NONE};
  
  static double m;
  
  static double eventProb(int base, Outcome o, Intervention i) {
    if (base >= 0) return o == Outcome.POS ? 1 : 0;
    if (base <= -m) return o == Outcome.NEG ? 1 : 0;
    double ans = 0;
    switch (i) {
    case HI:
      ans = 1 - (base * base) / (m * m);
      break;
    case LOW:
      ans = (m + base) * (m + base) / (m * m);
      break;
    case NONE:
      ans = (m + base) / m;
      break;
    }
    return o == Outcome.POS ? ans : 1 - ans;
  }
  
  static int[] b = new int[20];
  static int[][] p = new int[20][20];
  static int[][] q = new int[20][20];
  
  static int getBase(int idx, int prefix) {
    int ans = b[idx];
    for (int i = 1; i <= idx; i++) {
      ans += (prefix & 1) == 1 ? p[idx - i][idx] : q[idx - i][idx];
      prefix >>= 1;
    }
    return ans;
  }
  
  public static void main(String[] args) {
    Scanner s = new Scanner(System.in);
    int n = s.nextInt(), k = s.nextInt();
    m = s.nextInt();
    
    for (int i = 0; i < n; i++) {
      b[i] = s.nextInt();
      for (int j = i + 1; j < n; j++) p[i][j] = s.nextInt();
      for (int j = i + 1; j < n; j++) q[i][j] = s.nextInt();
    }
    
    double[][] dp = new double[k+1][1 << n];
    
    for (int i = 0; i < (1 << (n - 1)); i++) {
      dp[0][i + (1 << (n - 1))] = eventProb(getBase(n - 1, i), Outcome.POS, Intervention.NONE);
      for (int j = 1; j <= k; j++) {
        dp[j][i + (1 << (n - 1))] = eventProb(getBase(n - 1, i), Outcome.POS, Intervention.HI);
      }
    }
    
    for (int len = n - 2; len >= 0; len--) {
      for (int i = 0; i < (1 << len); i++) {
        dp[0][i + (1 << len)] = eventProb(getBase(len, i), Outcome.POS, Intervention.NONE) * dp[0][(i << 1) + 1 + (2 << len)]
            + eventProb(getBase(len, i), Outcome.NEG, Intervention.NONE) * dp[0][(i << 1) + 0 + (2 << len)];
        for (int j = 1; j <= k; j++) {
          dp[j][i + (1 << len)] = eventProb(getBase(len, i), Outcome.POS, Intervention.NONE) * dp[j][(i << 1) + 1 + (2 << len)]
              + eventProb(getBase(len, i), Outcome.NEG, Intervention.NONE) * dp[j][(i << 1) + 0 + (2 << len)];
          dp[j][i + (1 << len)] = Math.max(dp[j][i + (1 << len)],
              eventProb(getBase(len, i), Outcome.POS, Intervention.HI) * dp[j-1][(i << 1) + 1 + (2 << len)]
              + eventProb(getBase(len, i), Outcome.NEG, Intervention.HI) * dp[j-1][(i << 1) + 0 + (2 << len)]);
          dp[j][i + (1 << len)] = Math.max(dp[j][i + (1 << len)],
              eventProb(getBase(len, i), Outcome.POS, Intervention.LOW) * dp[j-1][(i << 1) + 1 + (2 << len)]
              + eventProb(getBase(len, i), Outcome.NEG, Intervention.LOW) * dp[j-1][(i << 1) + 0 + (2 << len)]);
        }
      }
    }
    
    DecimalFormat fmt = new DecimalFormat("0.000000");
    System.out.println(fmt.format(dp[k][1]));
  }
}
