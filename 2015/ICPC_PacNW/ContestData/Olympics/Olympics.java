import java.util.* ;
public class Olympics {
   public static void main(String[] args) {
      Scanner sc = new Scanner(System.in) ;
      int E = sc.nextInt(), E1 = sc.nextInt(), E2 = sc.nextInt() ;
      double a[] = new double[E+1] ;
      for (int i=1; i<=E; i++)
         a[i] = 1 + a[Math.max(0, i-E1)] + a[Math.max(0, i-E2)] ;
      System.out.printf("%.6f\n", Math.min(225/(a[E]+1),200/a[E])) ;
   }
}
