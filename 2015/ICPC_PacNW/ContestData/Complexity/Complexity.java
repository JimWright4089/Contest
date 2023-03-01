import java.util.*;
public class Complexity {
	public static void main(String[] args) throws Exception {
		Scanner sc = new Scanner(System.in);
		String s = sc.next();
		int[] c = new int[256];
		for(int i = 0; i < s.length(); i++) {
			c[s.charAt(i)]++;
		}
		Arrays.sort(c);
		System.out.println(s.length()-c[255]-c[254]);
	}
}
