import java.util.*;

public class ScalingRecipe {
    public static void main(String[] args) {
        Scanner s = new Scanner(System.in);
        int n = s.nextInt();
        int x = s.nextInt();
        int y = s.nextInt();
        int a;
        for (int i = 0; i < n; ++i) {
            a = s.nextInt();
            System.out.println(a * y/x);
        }
    }
}