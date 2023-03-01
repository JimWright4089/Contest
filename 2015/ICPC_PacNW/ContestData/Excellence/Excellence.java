import java.io.*;
import java.util.*;
public class Excellence {
	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		PrintWriter pw = new PrintWriter(new BufferedWriter(new OutputStreamWriter(System.out)));
		int n = Integer.parseInt(br.readLine());
		int[] list = new int[n];
		for(int i = 0; i < n; i++) {
			list[i] = Integer.parseInt(br.readLine());
		}
		Arrays.sort(list);
		int ret = Integer.MAX_VALUE;
		for(int i = 0; i < n-1-i; i++) {
			ret = Math.min(ret, list[i] + list[n-1-i]);
		}
		pw.println(ret);
		pw.close();
	}
}
