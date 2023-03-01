import java.awt.geom.Point2D;
import java.util.*;

public class Coverage {
  static final double EPS = 1e-6;
  
  static ArrayList<Point2D.Double> CCI(Point2D.Double a, Point2D.Double b, double r, double R) {
    ArrayList<Point2D.Double> ret = new ArrayList<Point2D.Double>();
    double d = a.distance(b);
    if (d > r+R || d+Math.min(r, R) < Math.max(r, R)) return ret;
    double x = (d*d-R*R+r*r)/(2*d);
    double y = Math.sqrt(r*r-x*x);
    Point2D.Double v = new Point2D.Double((b.x - a.x)/d, (b.y - a.y)/d);
    ret.add(new Point2D.Double(a.x + x*v.x - y*v.y, a.y + x*v.y + y*v.x));
    if (y > 0) ret.add(new Point2D.Double(a.x + x*v.x + y*v.y, a.y + x*v.y - y*v.x));
    return ret;
  }
  
  public static void main(String[] args) {
    Scanner s = new Scanner(System.in);
    int n = s.nextInt();
    double mult = 1;
    if (args.length == 1) mult = Double.parseDouble(args[0]);
    Point2D.Double[] pts = new Point2D.Double[n];
    int[] label = new int[n];
    Arrays.fill(label, -1);
    for (int i = 0; i < n; i++) {
      pts[i] = new Point2D.Double(s.nextDouble() * mult, s.nextDouble() * mult);
    }
    
    int numComponents = 0;
    for (int i = 0; i < n; i++) {
      if (label[i] > -1) continue;
      Queue<Integer> queue = new LinkedList<Integer>();
      label[i] = numComponents;
      queue.add(i);
      while (!queue.isEmpty()) {
        int cur = queue.remove();
        for (int j = 0; j < n; j++) {
          if (label[j] == -1 && pts[cur].distanceSq(pts[j]) <= 4 + EPS) {
            label[j] = numComponents;
            queue.add(j);
          }
        }
      }
      numComponents++;
    }
    
    int[] sizes = new int[numComponents];
    boolean[] included = new boolean[numComponents];
    for (int i = 0; i < n; i++) sizes[label[i]]++;
    
    int bestSoFar = 1;
    for (int i = 0; i < numComponents; i++) bestSoFar = Math.max(bestSoFar, sizes[i] + 1);
    
    for (int i = 0; i < n; i++) {
      for (int j = i + 1; j < n; j++) {
        if (label[i] == label[j]) continue;
        
        for (Point2D.Double c : CCI(pts[i], pts[j], 2, 2)) {
          Arrays.fill(included, false);
          for (int k = 0; k < n; k++) {
            if(c.distanceSq(pts[k]) <= 4 + EPS) included[label[k]] = true;
          }
          int cur = 1;
          for (int k = 0; k < numComponents; k++) if (included[k]) cur += sizes[k];
          bestSoFar = Math.max(bestSoFar, cur);
        }
      }
    }
    
    System.out.println(bestSoFar);
  }
}
