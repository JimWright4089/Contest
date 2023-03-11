import java.io.OutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.StringTokenizer;
import java.io.Writer;
import java.io.OutputStreamWriter;
import java.io.BufferedReader;
import java.io.InputStream;

/**
 * Built using CHelper plug-in
 * Actual solution is at the top
 *
 * @author lewin
 */
public class archery_accuracy_dynamic_lewin {
    public static void main(String[] args) {
        InputStream inputStream = System.in;
        OutputStream outputStream = System.out;
        InputReader2 in = new InputReader2(inputStream);
        OutputWriter out = new OutputWriter(outputStream);
        ArcheryAccuracyDynamic solver = new ArcheryAccuracyDynamic();
        solver.solve(1, in, out);
        out.close();
    }

    static class ArcheryAccuracyDynamic {
        public void solve(int testNumber, InputReader2 in, OutputWriter out) {
            int n = in.nextInt();
            double[] p = in.readDoubleArray(n);
            int[] s = in.readIntArray(n);

            double[][][] tprob = new double[n][n][2];
            for (int slot = 0; slot < n; slot++) {
                for (int shooter = 0; shooter < n; shooter++) {
                    int len = s[slot] * 2 + 1;
                    double[][] mat = new double[len][len + 1];
                    mat[0][len] = 0;
                    mat[0][0] = 1;
                    mat[len - 1][len] = 1;
                    mat[len - 1][len - 1] = 1;
                    for (int i = 1; i + 1 < len; i++) {
                        mat[i][i] = 1;
                        mat[i][i + 1] = -p[shooter];
                        mat[i][i - 1] = -(1 - p[shooter]);
                    }
                    RowReduce.rref(mat);
                    int offset = slot == 0 ? s[slot] : (s[slot] - s[slot - 1]);
                    tprob[slot][shooter][0] = mat[offset][len];
                    tprob[slot][shooter][1] = mat[len - offset - 1][len];
                }
            }

            double[][] dp = new double[1 << n][2];
            dp[0][1] = 1;

            for (int mask = 1; mask < 1 << n; mask++) {
                int slot = n - Integer.bitCount(mask);
                for (int cpos = 0; cpos <= 1; cpos++) {
                    for (int shooter = 0; shooter < n; shooter++) {
                        dp[mask][cpos] = Math.max(dp[mask][cpos],
                                dp[mask ^ (1 << shooter)][1] * tprob[slot][shooter][cpos] +
                                        dp[mask ^ (1 << shooter)][0] * (1 - tprob[slot][shooter][cpos]));
                    }
                }
            }

            out.printf("%.10f\n", dp[(1 << n) - 1][1]);
        }

    }

    static class InputReader2 {
        private BufferedReader in;
        private StringTokenizer st;

        public InputReader2(InputStream stream) {
            in = new BufferedReader(new InputStreamReader(stream));
            st = null;
        }

        public int[] readIntArray(int tokens) {
            int[] ret = new int[tokens];
            for (int i = 0; i < tokens; i++) {
                ret[i] = nextInt();
            }
            return ret;
        }

        public double[] readDoubleArray(int tokens) {
            double[] ret = new double[tokens];
            for (int i = 0; i < tokens; i++) {
                ret[i] = nextDouble();
            }
            return ret;
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

    static class RowReduce {
        public static void rref(double[][] M) {
            int row = M.length;
            if (row == 0)
                return;

            int col = M[0].length;

            int lead = 0;
            for (int r = 0; r < row; r++) {
                if (lead >= col - 1)
                    return;

                int k = r;
                while (M[k][lead] == 0) {
                    k++;
                    if (k == row) {
                        k = r;
                        lead++;
                        if (lead == col - 1)
                            return;
                    }
                }
                double[] temp = M[r];
                M[r] = M[k];
                M[k] = temp;

                double lv = M[r][lead];
                for (int j = 0; j < col; j++)
                    M[r][j] /= lv;

                for (int i = 0; i < row; i++) {
                    if (i != r) {
                        lv = M[i][lead];
                        for (int j = 0; j < col; j++)
                            M[i][j] -= lv * M[r][j];
                    }
                }
                lead++;
            }
        }

    }
}

