import java.io.*;
import java.util.*;

public class doublepassword_finn {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        char[][] lines = new char[2][4];
        lines[0] = br.readLine().toCharArray();
        lines[1] = br.readLine().toCharArray();
        int count = 0;
        for (int i=0; i<10000; i++) {
            char[] seq = new char[4];
            seq[0] = (char)('0'+(i/1000));
            seq[1] = (char)('0'+((i/100)%10));
            seq[2] = (char)('0'+((i/10)%10));
            seq[3] = (char)('0'+(i%10));
            boolean good = true;
            for (int j=0; j<4; j++) {
                if (seq[j]!=lines[0][j] && seq[j]!=lines[1][j]) {
                    good = false;
                    break;
                }
            }
            if (good) {
                count++;
            }
        }
        System.out.println(count);

    }
}
