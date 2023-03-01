import java.io.*;
import java.util.*;

/**
 * Greedy solution. Keep track of whether or not we have finished nondecreasing and 
 * when we should switch from mimicking to repeating a digit.
 *
 * @author Finn Lidbetter
 */
public class riseandfall_finn {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));

        int t = Integer.parseInt(br.readLine());
        while(t-- > 0) {
            char[] seq = br.readLine().toCharArray();
            boolean nondecreasing = true;
            boolean repeat = false;
            char repeater = '0';
            StringBuilder sb = new StringBuilder();
            sb.append(seq[0]);
            for (int i=1; i<seq.length; i++) {
                if (nondecreasing) {
                    if (seq[i]>=seq[i-1]) {
                        sb.append(seq[i]);
                    } else {
                        sb.append(seq[i]);
                        nondecreasing = false;
                        repeater = seq[i];
                    }
                } else {
                    if (repeat) {
                        sb.append(repeater);
                    } else {
                        if (seq[i]>seq[i-1]) {
                            repeat = true;
                            repeater = seq[i-1];
                            sb.append(repeater);
                        } else {
                            sb.append(seq[i]);
                        }
                    } 
                }
            }
            System.out.println(sb);
        }
    }
}
