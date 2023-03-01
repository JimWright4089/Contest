import java.io.*;
import java.util.*;

/**
 * First, identify how long the shortest missing subsequences will be.
 * Do this by iterating over the string, and waiting for all characters to
 * appear before resetting the appeared characters and incrementing a counter.
 *
 * Preprocess the input by the index of the next (or current) occurence of each character.
 * For each query string, test if it is a subsequence by finding the smallest index
 * of an occurence of the next character in the query string greater than the current
 * index in the given reference string.
 * @author Finn Lidbetter
 */

public class sms_scanner_io {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        char[] chAlphabet = sc.nextLine().toCharArray();
        HashSet<Character> alphabetSet = new HashSet<>();
        for (char ch: chAlphabet) {
            alphabetSet.add(ch);
        }
        char[] seq = sc.nextLine().toCharArray();
        int[][] table = new int[26][seq.length];
        for (int c=0; c<26; c++) {
            int prev = -2;
            for (int i=seq.length-1; i>=0; i--) {
                if (seq[i]==(char)((int)'a'+c)) {
                    prev = i;
                }
                table[c][i] = prev;
            }
        }
        int targetLength = 1;
        HashSet<Character> seen = new HashSet<>();
        for (int i=0; i<seq.length; i++) {
            seen.add(seq[i]);
            if (seen.size()==alphabetSet.size()) {
                seen = new HashSet<>();
                targetLength++;
            }
        }
        int n = Integer.parseInt(sc.nextLine());
        for (int i=0; i<n; i++) {
            char[] curr = sc.nextLine().toCharArray();
            if (curr.length!=targetLength) {
                System.out.println("0");
                continue;
            }
            boolean isSubsequence = true;
            int currIndex = 0;
            for (char ch: curr) {
                if (currIndex < 0 || currIndex >= seq.length) {
                   isSubsequence = false;
                   break;
                }
                currIndex = table[ch-'a'][currIndex] + 1;
            }
            if (currIndex==-1) {
                isSubsequence = false;
            }
            if (isSubsequence) {
                System.out.println("0");
            } else {
                System.out.println("1");
            }
        }
    }
}
