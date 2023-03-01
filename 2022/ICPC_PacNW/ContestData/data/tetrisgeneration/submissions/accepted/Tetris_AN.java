import java.util.*;

public class Tetris_AN {
  public static void main(String[] args) {
    Scanner s = new Scanner(System.in);
    int t = s.nextInt();
    for (int tc = 0; tc < t; tc++) {
      String sequence = s.next();
      boolean possible = false;
      for (int shift = 0; shift < 7; shift++) {
        boolean valid = true;
        Set<Character> bag = new HashSet<>();
        for (int i = 0; i < shift; i++) {
          if (i >= sequence.length()) break;
          if (!bag.add(sequence.charAt(i))) {
            valid = false;
            break;
          }
        }
        if (!valid) continue;
        bag.clear();
        for (int i = shift; i < sequence.length(); i++) {
          if (!bag.add(sequence.charAt(i))) {
            valid = false;
            break;
          }
          if (bag.size() == 7) bag.clear();
        }
        if (valid) {
          possible = true;
          break;
        }
      }
  
      if (possible) {
        System.out.println(1);
      } else {
        System.out.println(0);
      }
    }
  }
}
