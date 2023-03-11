import java.io.*;
import java.util.*;

/**
 * Try each offset of 7 as the possible starting point for the cycle of 7.
 *
 * @author Finn Lidbetter
 */

public class tetris_finn {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        int t = Integer.parseInt(br.readLine());
        while(t-- > 0) {
            char[] seq = br.readLine().toCharArray();
            boolean valid = false;
            for (int start=0; start<7; start++) {
                boolean validStart = true;
                if (start>=seq.length) {
                    break;
                }
                HashSet<Character> tetrominoSet = new HashSet<>();
                for (int j=0; j<start; j++) {
                    tetrominoSet.add(seq[j]);
                }
                if (tetrominoSet.size()!=start) {
                    validStart = false;
                    continue;
                }
                tetrominoSet.clear();
                int count = 0;
                for (int j=start; j<seq.length; j++) {
                    if (tetrominoSet.contains(seq[j])) {
                        validStart = false;
                        break;
                    }
                    tetrominoSet.add(seq[j]);
                    if (tetrominoSet.size()==7) {
                        tetrominoSet.clear();
                    }
                }
                if (validStart) {
                    valid = true;
                    break;
                }
            }
            if (valid) {
                System.out.println(1);
            } else {
                System.out.println(0);
            }
        }
    }
}
