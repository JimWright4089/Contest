import java.util.*;
public class Classy {
	public static void main(String[] args) throws Exception {
		Scanner sc = new Scanner(System.in);
                sc.nextLine() ;
		ArrayList<Person> ret = new ArrayList<Person>();
		while(sc.hasNextLine()) {
			String s = sc.nextLine();
			String name = s.substring(0, s.indexOf(":"));
			s = s.substring(name.length()+2, s.length()-6);
			StringTokenizer st = new StringTokenizer(s);
			ArrayList<String> l = new ArrayList<String>();
			while(st.hasMoreTokens()) {
				l.add(st.nextToken());
			}
			ret.add(new Person(name, l));
		}
		Collections.sort(ret);
		for(Person p: ret) {
			System.out.println(p);
		}
	}
	static class Person implements Comparable<Person> {
		public String n;
		public ArrayList<String> q;
		public String toString() {
			return n;
		}
		public Person(String a, ArrayList<String> b) {
			n = a;
			q = b;
		}
		public int compareTo(Person p) {
			int s = Math.max(q.size(), p.q.size());
			for(int i = 0; i < s; i++) {
				String me = q.size()-1-i >= 0 ? q.get(q.size()-1-i) : "middle";
				String they = p.q.size()-1-i >= 0 ? p.q.get(p.q.size()-1-i) : "middle";
				int ret = they.compareTo(me);
				if(ret != 0) return ret;
			}
			return n.compareTo(p.n);
		}
	}
}
