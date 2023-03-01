import java.io.*;
import java.util.*;
public class Triangle {
	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		PrintWriter pw = new PrintWriter(new BufferedWriter(new OutputStreamWriter(System.out)));
		int[] x = new int[3];
		int[] y = new int[3];
		StringTokenizer st = new StringTokenizer(br.readLine());
		x[0] = Integer.parseInt(st.nextToken());
		x[0] *= x[0];
		x[1] = Integer.parseInt(st.nextToken());
		x[1] *= x[1];
		x[2] = Integer.parseInt(st.nextToken());
		x[2] *= x[2];
		st = new StringTokenizer(br.readLine());
		y[0] = Integer.parseInt(st.nextToken());
		y[0] *= y[0];
		y[1] = Integer.parseInt(st.nextToken());
		y[1] *= y[1];
		y[2] = Integer.parseInt(st.nextToken());
		y[2] *= y[2];
		Arrays.sort(x);
		Arrays.sort(y);
		boolean valid = x[0] == y[0] && x[1] == y[1] && x[2] == y[2] && x[0] + x[1] == x[2];
		pw.println(valid ? "YES" : "NO");
		pw.close();
	}
}
