import java.io.* ;
import java.util.* ;
public class FallingBlocks {
   static final int MAXHEIGHT = 10 ;
   static final String shapes[] = new String[] {
      "T", "XXX X  X ",
      "X", " X XXX X ",
      "R", " XXXX  X ",
      "S", " XX X XX ",
      "Z", "XX  X  XX",
      "V", "X  X  XXX",
      "7", "XX  XX X ",
      "W", "X  XX  XX"
   } ;
   static int startpos[], shapeindex[], bitmaps[], numshapes, strmod ;
   static HashMap<Long, Integer> distance ;
   static String inp ;
   static long mask ;
   static int explor(long bits, int strpos) {
      long state = bits * strmod + strpos ;
      distance.put(state, -1) ;
      int deep = 0 ;
      int nextblock = shapeindex[inp.charAt(strpos)] ;
      for (int i=startpos[nextblock]; i<startpos[nextblock+1]; i++) {
         long block = bitmaps[i] ;
         long bm = (bits << 9) ;
         while ((bm & ~0777) != 0 && (bm & (block << 3)) == 0)
            bm >>= 3 ;
         bm = bm | block ;
         for (int row=2; row>=0; row--) {
            long bmrow = 07 << (3 * row) ;
            if ((bm & bmrow) == bmrow) {
               bm = (bm & ((1L << (3 * row)) - 1)) |
                    ((bm >> 3) & ~((1L << (3 * row)) - 1)) ;
            }
         }
         if ((bm & ~mask) != 0)
            continue ;
         long nstate = bm * strmod + ((strpos + 1) % strmod) ;
         if (distance.get(nstate) != null) {
            if (distance.get(nstate) < 0) {
               System.out.println("forever") ;
               System.exit(0) ;
            }
            deep = Math.max(distance.get(nstate), deep) ;
         } else {
            int t = explor(bm, (strpos + 1) % strmod) ;
            deep = Math.max(deep, t) ;
         }
      }
      distance.put(state, deep + 1) ;
      return deep + 1 ;
   }
   public static void main(String[] args) {
      /* turn the shapes above into 9-bit bitmaps. */
      boolean seen[] = new boolean[512] ;
      numshapes = shapes.length / 2 ;
      startpos = new int[numshapes+1] ;
      shapeindex = new int[128] ;
      bitmaps = new int[numshapes*4] ;
      int at = 0 ;
      for (int i=0; i<shapes.length; i += 2) {
         shapeindex[shapes[i].charAt(0)] = i/2 ;
         startpos[i/2] = at ;
         int bm = 0 ;
         for (int j=0; j<9; j++)
            if (shapes[i+1].charAt(j) == 'X')
               bm |= (1<<j) ;
         for (int d=0; d<4; d++) {
            if (!seen[bm]) {
               seen[bm] = true ;
               bitmaps[at++] = bm ;
            }
            bm = ((bm & 7) << 6) + (bm & 070) + ((bm & 0700) >> 6) ;
            bm = ((bm & 1) << 8) + ((bm & 012) << 4) + (bm & 0124) +
                 ((bm & 0240) >> 4) + ((bm & 0400) >> 8) ;
         }
      }
      startpos[numshapes] = at ;
      if (args.length > 0)
         inp = args[0] ;
      else {
         Scanner sc = new Scanner(System.in);
         inp = sc.next() ;
      }
      strmod = inp.length() ;
      distance = new HashMap<Long, Integer>() ;
      mask = (1L << (3 * (MAXHEIGHT-3))) - 1 ;
      int r = explor(0L, 0) ;
//    System.out.println("Number of states is " + distance.size()) ;
      System.out.println(r) ;
   }
}
