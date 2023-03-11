import java.io.*;
import java.util.*;

/**
 * Get the first intersection point to determine values for cos(theta) and sin(theta),
 * where theta is the central angle between the initial point and the first intersection 
 * point.
 * These will be (a^2-b^2)/(a^2+b^2) and (2ab)/(a^2+b^2) respectively.
 * 
 * Use these values in the rotation matrix and use fast matrix exponentiation to get
 * the result after n ricochets.
 *
 * @author Finn Lidbetter
 */

public class ricochet_finn {
    static long MOD = 1_000_000_007L;
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));

        String[] s = br.readLine().split(" ");
        long a = Long.parseLong(s[0]);
        long b = Long.parseLong(s[1]);
        long n = Long.parseLong(s[2]);

        long aapbb = (a*a + b*b)%MOD;
        long aambb = (((a*a)%MOD - (b*b)%MOD) + MOD)%MOD;
        long nab2 = (MOD - ((2*a*b)%MOD))%MOD;
        long aapbbInv = modInv(aapbb, MOD);
        long cosTheta = (aambb * aapbbInv)%MOD;
        long sinTheta = (nab2 * aapbbInv)%MOD;
        long nSinTheta = (MOD-sinTheta)%MOD;

        long[][] rot = new long[][] {{cosTheta, nSinTheta}, {sinTheta, cosTheta}};
        long[][] pow = matrixPower(rot, n+1);
        long result = (MOD - pow[0][0])%MOD;
        System.out.println(result);
    }
    
    static long[][] matrixPower(long[][] matrix, long n) {
        int len = matrix.length;
        long[][] result = null;
        if (n == 0) {
            result = new long[len][len];
            for (int i = 0; i < len; i++) {
                result[i][i] = 1L;
            }
        } else {
            long[][] arr = deepCopy(matrix);
            while (n > 0) {
                if ((n & 1L) == 1L) {
                    result = (result == null ? deepCopy(arr) : mult(result, arr));
                }
                arr = mult(arr, arr);
                n >>= 1L;
            }
        }
        return result; 
    }
    static long[][] mult(long[][] a, long[][] b) {
        int len = a.length;
        long[][] c = new long[len][len];
        for (int i = 0; i < len; i++) {
            for (int j = 0; j < len; j++) {
                for (int k = 0; k < len; k++) {
                    c[i][j] = (c[i][j] + a[i][k] * b[k][j]) % MOD;
                }
            }
        }
        return c; 
    }
    static long[][] deepCopy(long[][] arr) {
        int len = arr.length;
        long[][] copy = new long[len][len];
        for (int i = 0; i < len; i++) {
            copy[i] = arr[i].clone();
        }
        return copy;
    } 

    static long[] egcd(long a, long b) {
        if (b == 0) 
            return new long[] { a, 1, 0 };
        else {
            long[] ret = egcd(b, a % b);
            long tmp = ret[1] - ret[2] * (a / b);
            ret[1] = ret[2]; 
            ret[2] = tmp; 
            return ret; 
        } 
    }
    static long modInv(long x, long m) { 
        return (egcd(x, m)[1] + m) % m; 
    }
}
