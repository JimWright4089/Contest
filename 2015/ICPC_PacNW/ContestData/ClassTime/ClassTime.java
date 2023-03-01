import java.util.*;
public class ClassTime {
	public static void main(String[] args) throws Exception {
		Scanner sc = new Scanner(System.in);
		int n = sc.nextInt();
		Person[] l = new Person[n];
		for(int i = 0; i < n; i++) {
			l[i] = new Person(sc.next(), sc.next());
		}
		Arrays.sort(l);
		for(Person p: l) {
			System.out.println(p);
		}
	}
	static class Person implements Comparable<Person> {
		public String f, l;
		public Person(String a, String b) {
			f=a;
			l=b;
		}
		public int compareTo(Person p) {
			int ret = l.compareTo(p.l);
			if(ret == 0) ret = f.compareTo(p.f);
			return ret;
		}
		public String toString() {
			return f + " " + l;
		}
	}
}
