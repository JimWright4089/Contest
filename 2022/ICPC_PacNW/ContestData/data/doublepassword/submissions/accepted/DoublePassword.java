import java.util.*;

public class DoublePassword {
    public static void main(String[] args) {
        Scanner s = new Scanner(System.in);
        String password1 = s.nextLine();
        String password2 = s.nextLine();
        int num_passwords = 1;
        for (int i = 0; i < 4; ++i) {
            if (password1.charAt(i) != password2.charAt(i)) {
                num_passwords *= 2;
            }
        }
        System.out.println(num_passwords);
    }
}