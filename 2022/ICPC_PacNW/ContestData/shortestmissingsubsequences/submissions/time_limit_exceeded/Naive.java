import java.io.*;
import java.util.*;

/**
 * First, identify how long the shortest missing subsequences will be.
 * Do this by iterating over the string, and waiting for all characters to
 * appear before resetting the appeared characters and incrementing a counter.
 *
 *
 * @author Finn Lidbetter
 */

public class Naive {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        char[] chAlphabet = br.readLine().toCharArray();
        HashSet<Character> alphabetSet = new HashSet<>();
        for (char ch: chAlphabet) {
            alphabetSet.add(ch);
        }
        char[] seq = br.readLine().toCharArray();
        int targetLength = 1;
        HashSet<Character> seen = new HashSet<>();
        for (int i=0; i<seq.length; i++) {
            seen.add(seq[i]);
            if (seen.size()==alphabetSet.size()) {
                seen = new HashSet<>();
                targetLength++;
            }
        }
        int n = Integer.parseInt(br.readLine());
        for (int i=0; i<n; i++) {
            char[] curr = br.readLine().toCharArray();
            if (curr.length!=targetLength) {
                System.out.println("0");
                continue;
            }
            boolean isSubsequence = true;
            int seqIndex = 0;
            for (char ch: curr) {
                boolean foundChar = false;
                while (seqIndex<seq.length && seq[seqIndex]!=ch) {
                    seqIndex++;
                }
                if (seqIndex<seq.length && seq[seqIndex]==ch) {
                    foundChar = true;
                }
                if (!foundChar) {
                    isSubsequence = false;
                    break;
                }
                seqIndex++;
            }
            if (isSubsequence) {
                System.out.println("0");
            } else {
                System.out.println("1");
            }
        }
    }
}
