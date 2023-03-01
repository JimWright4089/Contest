import java.io.*;
import java.util.*;

/**
 * First, identify how long the shortest missing subsequences will be.
 * Do this by iterating over the string, and waiting for all characters to
 * appear before resetting the appeared characters and incrementing a counter.
 *
 * Preprocess the input by storing lists of indices by character.
 * For each query string, test if it is a subsequence by finding the smallest index
 * of an occurence of the next character in the query string greater than the current
 * index in the given reference string. Use a binary search to do this.
 *
 * Time complexity: O(26n + Mlog(n)) where n is the length of the reference string, S,
 * and M is the sum of the lengths of the query strings.
 *
 * This problem can be solved in time O(26n + M), but it will likely be very difficult 
 * to separate from the O(26n + Mlog(n)) solution given here.
 *
 * @author Finn Lidbetter
 */

public class sms_finn {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        char[] chAlphabet = br.readLine().toCharArray();
        HashSet<Character> alphabetSet = new HashSet<>();
        for (char ch: chAlphabet) {
            alphabetSet.add(ch);
        }
        char[] seq = br.readLine().toCharArray();
        ArrayList[] table = new ArrayList[26];
        for (int i=0; i<26; i++) {
            table[i] = new ArrayList<Integer>();
        }
        for (int i=0; i<seq.length; i++) {
            table[seq[i]-'a'].add(i);
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
        int n = Integer.parseInt(br.readLine());
        for (int i=0; i<n; i++) {
            char[] curr = br.readLine().toCharArray();
            if (curr.length!=targetLength) {
                System.out.println("0");
                continue;
            }
            int currIndex = -1;
            boolean isSubsequence = true;
            for (char ch: curr) {
                if (table[ch-'a'].size()==0) {
                    isSubsequence = false;
                    break;
                }
                int lo = 0;
                int hi = table[ch-'a'].size()-1;
                int bestNextIndex = -1;
                while (lo<=hi) {
                    int mid = lo + (hi-lo)/2;
                    if ((int)((Integer)table[ch-'a'].get(mid))>currIndex) {
                        bestNextIndex = (int)((Integer) table[ch-'a'].get(mid));
                        hi = mid - 1;
                    } else {
                        lo = mid + 1;
                    }
                }
                if (bestNextIndex>currIndex) {
                    currIndex = bestNextIndex;
                } else {
                    isSubsequence = false;
                    break;
                }
            }
            if (isSubsequence) {
                System.out.println("0");
            } else {
                System.out.println("1");
            }
        }
    }
}
