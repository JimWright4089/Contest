import java.util.*;
public class MagicTrick {
	public static void main(String[] args) throws Exception {
		Scanner sc = new Scanner(System.in);
		int n = sc.nextInt();
		String[] op = new String[n];
		int[] val = new int[n];
		for(int i = 0; i < n; i++) {
			op[i] = sc.next();
			val[i] = sc.nextInt();
		}
		int ret = 0;
		for(int a = 1; a <= 100; a++) {
			int curr = a;
			for(int i = 0; i < n; i++) {
				if(op[i].equals("ADD")) {
					curr += val[i];
				}
				else if(op[i].equals("SUBTRACT")) {
					curr -= val[i];
					if(curr < 0) {
						ret++;
						break;
					}
				}
				else if(op[i].equals("MULTIPLY")) {
					curr *= val[i];
				}
				else if(op[i].equals("DIVIDE")) {
					if(curr % val[i] != 0) {
						ret++;
						break;
					}
					curr /= val[i];
				}
			}
		}
		System.out.println(ret);
	}	
}
