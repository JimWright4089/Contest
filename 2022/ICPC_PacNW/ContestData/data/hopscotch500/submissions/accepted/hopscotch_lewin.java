import java.io.OutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.TreeSet;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Stream;
import java.util.StringTokenizer;
import java.io.Writer;
import java.io.OutputStreamWriter;
import java.io.BufferedReader;
import java.util.Comparator;
import java.io.InputStream;

/**
 * Built using CHelper plug-in
 * Actual solution is at the top
 *
 * @author lewin
 */
public class hopscotch_lewin {
    public static void main(String[] args) {
        InputStream inputStream = System.in;
        OutputStream outputStream = System.out;
        InputReader2 in = new InputReader2(inputStream);
        OutputWriter out = new OutputWriter(outputStream);
        Hopscotch1000 solver = new Hopscotch1000();
        solver.solve(1, in, out);
        out.close();
    }

    static class Hopscotch1000 {
        public void solve(int testNumber, InputReader2 in, OutputWriter out) {
            int n = in.nextInt(), k = in.nextInt();
            List<Hopscotch1000.Position>[] ps = LUtils.genArrayList(k);
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    int num = in.nextInt() - 1;
                    ps[num].add(new Hopscotch1000.Position(i, j));
                }
            }
            CHT cx = new CHT(CHT.MIN), cy = new CHT(CHT.MIN);
            cx.add(0, 0);
            cy.add(0, 0);
            long best = 1L << 60;

            for (int i = 0; i < k; i++) {
                if (ps[i].size() == 0) {
                    out.println(-1);
                    return;
                }
                CHT nx = new CHT(CHT.MIN), ny = new CHT(CHT.MIN);
                for (Hopscotch1000.Position p : ps[i]) {
                    long bx = cx.query(p.x) + (i == 0 ? 0 : (long) p.x * p.x);
                    long by = cy.query(p.y) + (i == 0 ? 0 : (long) p.y * p.y);

                    long cost = Math.min(bx, by);
                    if (i == k - 1) best = Math.min(best, cost);
                    nx.add(-2 * p.x, (long) p.x * p.x + cost);
                    ny.add(-2 * p.y, (long) p.y * p.y + cost);
                }
                cx = nx;
                cy = ny;
            }
            out.println(best);
        }

        static class Position {
            public int x;
            public int y;

            public Position(int x, int y) {
                this.x = x;
                this.y = y;
            }

        }

    }

    static class OutputWriter {
        private final PrintWriter writer;

        public OutputWriter(OutputStream outputStream) {
            writer = new PrintWriter(new BufferedWriter(new OutputStreamWriter(outputStream)));
        }

        public OutputWriter(Writer writer) {
            this.writer = new PrintWriter(writer);
        }

        public void close() {
            writer.close();
        }

        public void println(long i) {
            writer.println(i);
        }

        public void println(int i) {
            writer.println(i);
        }

    }

    static class LUtils {
        public static <E> List<E>[] genArrayList(int size) {
            return Stream.generate(ArrayList::new).limit(size).toArray(List[]::new);
        }

    }

    static class InputReader2 {
        private BufferedReader in;
        private StringTokenizer st;

        public InputReader2(InputStream stream) {
            in = new BufferedReader(new InputStreamReader(stream));
            st = null;
        }

        public int nextInt() {
            return Integer.parseInt(next());
        }

        public String next() {
            while (st == null || !st.hasMoreTokens()) {
                try {
                    st = new StringTokenizer(in.readLine());
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
            return st.nextToken();
        }

    }

    static class CHT {
        public static final int MIN = -1;
        public final TreeSet<CHT.Line> hull;
        int type;
        boolean query = false;
        Comparator<CHT.Line> comp = new Comparator<CHT.Line>() {
            public int compare(CHT.Line a, CHT.Line b) {
                if (!query) return type * Long.compare(a.m, b.m);
                if (a.left == b.left)
                    return Long.compare(a.m, b.m);
                return Double.compare(a.left, b.left);
            }
        };

        public CHT(final int type) {
            this.type = type;
            this.hull = new TreeSet<>(comp);
        }

        public void add(long m, long b) {
            add(new CHT.Line(m, b));
        }

        public void add(CHT.Line a) {
            CHT.Line[] LR = {hull.lower(a), hull.ceiling(a)};
            for (int i = 0; i < 2; i++)
                if (LR[i] != null && LR[i].m == a.m) {
                    if (type == 1 && LR[i].b >= a.b)
                        return;
                    if (type == -1 && LR[i].b <= a.b)
                        return;
                    remove(LR[i]);
                }

            hull.add(a);
            CHT.Line L = hull.lower(a), R = hull.higher(a);
            if (L != null && R != null && a.inter(R) <= R.left) {
                hull.remove(a);
                return;
            }
            CHT.Line LL = (L != null) ? hull.lower(L) : null;
            CHT.Line RR = (R != null) ? hull.higher(R) : null;
            if (L != null) a.left = a.inter(L);
            if (R != null) R.left = a.inter(R);
            while (LL != null && L.left >= a.inter(LL)) {
                remove(L);
                a.left = a.inter(L = LL);
                LL = hull.lower(L);
            }
            while (RR != null && R.inter(RR) <= a.inter(RR)) {
                remove(R);
                RR.left = a.inter(R = RR);
                RR = hull.higher(R);
            }
        }

        public long query(long x) {
            CHT.Line temp = new CHT.Line(0, 0, 0);
            temp.left = x;
            query = true;
            long ans = hull.floor(temp).eval(x);
            query = false;
            return ans;
        }

        private void remove(CHT.Line x) {
            hull.remove(x);
        }

        public static class Line {
            long m;
            long b;
            double left = Long.MIN_VALUE;

            public Line(long m, long x, long y) {
                this.m = m;
                this.b = -m * x + y;
            }

            public Line(long m, long b) {
                this.m = m;
                this.b = b;
            }

            public long eval(long x) {
                return m * x + b;
            }

            public double inter(CHT.Line x) {
                return (double) (x.b - this.b) / (double) (this.m - x.m);
            }

            public String toString() {
                return "Line{" +
                        "m=" + m +
                        ", b=" + b +
                        ", left=" + left +
                        '}';
            }

        }

    }
}

