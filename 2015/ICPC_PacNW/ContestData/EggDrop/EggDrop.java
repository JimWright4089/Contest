import java.util.*;
public class EggDrop {
	public static void main(String[] args) throws Exception {
		Scanner scan = new Scanner(System.in);
		int n = scan.nextInt();
		int k = scan.nextInt();
		int highestSafe = k-1;
		int lowestBreak = 2;
		while(n-- > 0) {
			int f = scan.nextInt();
			String type = scan.next();
			if(type.equals("SAFE")) {
				lowestBreak = Math.max(lowestBreak, f+1);
			}
			else {
				highestSafe = Math.min(highestSafe, f-1);
			}
		}
		System.out.println(lowestBreak + " " + highestSafe);
	}
}
