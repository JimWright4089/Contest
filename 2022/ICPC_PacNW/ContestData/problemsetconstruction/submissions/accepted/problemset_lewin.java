import java.io.OutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.util.Arrays;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
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
public class problemset_lewin {
    public static void main(String[] args) {
        InputStream inputStream = System.in;
        OutputStream outputStream = System.out;
        InputReader2 in = new InputReader2(inputStream);
        OutputWriter out = new OutputWriter(outputStream);
        ProblemSetConstruction solver = new ProblemSetConstruction();
        solver.solve(1, in, out);
        out.close();
    }

    static class ProblemSetConstruction {
        int n;
        int k;
        int t;
        ProblemSetConstruction.Problem[] ps;

        public void solve(int testNumber, InputReader2 in, OutputWriter out) {
            n = in.nextInt();
            k = in.nextInt();
            t = in.nextInt();
            ps = new ProblemSetConstruction.Problem[n];
            for (int i = 0; i < n; i++) {
                ps[i] = new ProblemSetConstruction.Problem(in.nextDouble(), in.nextInt(), i);
            }
            Arrays.sort(ps, Comparator.comparingInt(x -> x.time));
            double[][] dp = new double[t + 1][k + 1];
            // [i][j] = time elapsed, problems chosen -> prob
            dp[0][0] = 1;
            double[] score = new double[n];

            for (int i = 0; i < n; i++) {
                var p = ps[i];
                for (int time = t - 1; time >= 0; time--) {
                    for (int used = k - 1; used >= 0 && n - i >= k - used; used--) {
                        double base = dp[time][used];
                        double take = (k - used) / (double) (n - i);
                        // this problem was not chosen as part of our set
                        dp[time][used] = base * (1 - take);
                        // this problem was chosen in the set, but we can't solve it
                        dp[time][used + 1] += base * take * (1 - p.prob);
                        // this problem was chosen in the set, and we can solve it
                        if (time + p.time <= t) {
                            double add = base * take * p.prob;
                            dp[time + p.time][used + 1] += add;
                            score[p.index] += add;
                        }
                    }
                }
            }

            for (double x : score)
                out.printf("%.6f\n", x * n / k);
        }

        static class Problem {
            double prob;
            int time;
            int index;

            public Problem(double prob, int time, int index) {
                this.prob = prob;
                this.time = time;
                this.index = index;
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

        public void printf(String format, Object... objects) {
            writer.printf(format, objects);
        }

        public void close() {
            writer.close();
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

        public double nextDouble() {
            return Double.parseDouble(next());
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
}

