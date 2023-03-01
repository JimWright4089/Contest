import java.util.* ;

public class Blur {
   public static void main(String[] args) {
      Scanner s = new Scanner(System.in);
      int w = s.nextInt() ;
      int h = s.nextInt() ;
      int g = s.nextInt() ;
      long[][] b = new long[h][w] ;
      for (int i=0; i<h; i++)
         for (int j=0; j<w; j++)
            b[i][j] = s.nextInt() ;
      long[][] bb = new long[h][w] ;
      for (int gg=0; gg<g; gg++) {
         for (int i=0; i<h; i++)
            for (int j=0; j<w; j++)
               bb[i][j] = b[i][j] + b[i][(j+1)%w] + b[i][(j-1+w)%w] ;
         for (int i=0; i<h; i++)
            for (int j=0; j<w; j++)
               b[i][j] = bb[i][j] + bb[(i+1)%h][j] + bb[(i-1+h)%h][j] ;
      }
      HashSet hs = new HashSet() ;
      for (int i=0; i<h; i++)
         for (int j=0; j<w; j++)
            hs.add("K"+b[i][j]) ;
      System.out.println(hs.size()) ;
   }
}
