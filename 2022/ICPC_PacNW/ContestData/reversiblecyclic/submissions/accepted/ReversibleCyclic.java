import java.lang.*;
import java.util.*;

public class ReversibleCyclic {
    public static void main(String[] args) {
        String s = (new Scanner(System.in)).next();
        String reversed = (new StringBuilder(s)).reverse().toString();
        String sub1 = reversed.substring(0, s.length() - 1);
        String sub2 = reversed.substring(1);
        String ss = s + s;
        System.out.println((ss.contains(sub1) && ss.contains(sub2)) ? 1 : 0);
    }
}
