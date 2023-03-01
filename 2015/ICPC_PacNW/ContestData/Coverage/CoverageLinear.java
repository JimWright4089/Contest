import java.awt.Point;
import java.awt.geom.Point2D;
import java.util.*;

public class CoverageLinear {
  static final int SCALE = 2;
  
  static final double EPS = 1e-6;
  
  static Point2D.Double[] pts;
  static int[] label;
  static HashMap<Point2D.Double, Integer> reverseLookup = new HashMap<Point2D.Double, Integer>();
  
  static HashMap<Point, Point2D.Double> map = new HashMap<Point, Point2D.Double>();
  
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
  
  static void addPoint(Point2D.Double pt) {
    map.put(new Point((int) (SCALE * pt.x), (int) (SCALE * pt.y)), pt);
  }
  
  static ArrayList<Point2D.Double> getBall(Point2D.Double pt, int r) {
    int x = (int) (SCALE * pt.x);
    int y = (int) (SCALE * pt.y);
    ArrayList<Point2D.Double> ans = new ArrayList<Point2D.Double>();
    for (int dx = -r * SCALE; dx <= r * SCALE; dx++) {
      for (int dy = -r * SCALE; dy <= r * SCALE; dy++) {
        Point2D.Double o = map.get(new Point(x + dx, y + dy));
        if (o != null && o.distanceSq(pt) <= r * r + EPS) ans.add(o);
      }
    }
    return ans;
  }
  
  public static void main(String[] args) {
    Scanner s = new Scanner(System.in);
    int n = s.nextInt();
    pts = new Point2D.Double[n];
    label = new int[n];
    Arrays.fill(label, -1);
    for (int i = 0; i < n; i++) {
      pts[i] = new Point2D.Double(s.nextDouble(), s.nextDouble());
      addPoint(pts[i]);
      reverseLookup.put(pts[i], i);
    }
    
    int numComponents = 0;
    for (int i = 0; i < n; i++) {
      if (label[i] > -1) continue;
      Queue<Integer> queue = new LinkedList<Integer>();
      label[i] = numComponents;
      queue.add(i);
      while (!queue.isEmpty()) {
        int cur = queue.remove();
        ArrayList<Point2D.Double> ball = getBall(pts[cur], 2);
        for (Point2D.Double o : ball) {
          int idx = reverseLookup.get(o);
          if (label[idx] == -1) {
            label[idx] = numComponents;
            queue.add(idx);
          }
        }
      }
      numComponents++;
    }
    
    int[] sizes = new int[numComponents];
    for (int i = 0; i < n; i++) sizes[label[i]]++;
    
    int bestSoFar = 1;
    for (int i = 0; i < numComponents; i++) bestSoFar = Math.max(bestSoFar, sizes[i] + 1);
    
    for (int i = 0; i < n; i++) {
      ArrayList<Point2D.Double> ball = getBall(pts[i], 4);
      for (Point2D.Double o : ball) {
        int idx = reverseLookup.get(o);
        if (label[idx] == label[i]) continue;
        for (Point2D.Double c : CCI(pts[i], o, 2, 2)) {
          HashSet<Integer> labels = new HashSet<Integer>();
          for (Point2D.Double p : ball) if (p.distanceSq(c) <= 4 + EPS) labels.add(label[reverseLookup.get(p)]);
          int cur = 1;
          for (int l : labels) cur += sizes[l];
          bestSoFar = Math.max(bestSoFar, cur);
        }
      }
    }
    
    System.out.println(bestSoFar);
  }
}
