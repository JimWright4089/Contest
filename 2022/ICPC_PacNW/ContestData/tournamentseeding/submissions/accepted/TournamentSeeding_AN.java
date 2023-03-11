import java.util.*;

public class TournamentSeeding_AN {
  public static void main(String[] args) {
    Scanner s = new Scanner(System.in);
    int N = s.nextInt(), K = s.nextInt();
    int[] rating = new int[1 << N];
    for (int i = 0; i < rating.length; i++) rating[i] = 2000000000 - s.nextInt();
    Arrays.sort(rating);
    int close = 0;
    for (int round = 0; round < N; round++) {
      int bound = 1 << round;
      int lower = (2 << round) - 1;
      for (int upper = bound - 1; upper >= 0; upper--) {
        while (lower >= bound && rating[lower] - rating[upper] > K) lower--;
        if (lower >= bound && rating[lower] - rating[upper] <= K) {
          close++;
          lower--;
        }
      }
    }
    System.out.println(close);
  }
}
