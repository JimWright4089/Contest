import java.io.PrintStream;
import java.util.Scanner;

/**
 * Solution to Square Bounce.
 *
 * @author vanb
 */
public class squarebounce_vanb
{
    /** Input. */
    private static Scanner sc;
    
    /** Output. */
    private static PrintStream ps;
    
    /**
     * The Class Rational, for rational numbers.
     *
     * @author vanb
     */
    private class Rational implements Comparable<Rational>
    {
        
        /** The numerator. */
        private long numerator;
        
        /** The denominator. */
        private long denominator;
        
        /**
         * Greatest Common Divisor
         *
         * @param a A value
         * @param b ANother value
         * @return the Greatest Common Divisor of a and b
         */
        private long gcd( long a, long b )
        {
            return b==0 ? a : gcd( b, a%b );
        }
        
        /**
         * Instantiates a new Rational.
         *
         * @param numerator the numerator
         * @param denominator the denominator
         */
        public Rational(long numerator, long denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
            reduce();
        }
        
        /**
         * Reduce a Rational.
         *
         * @return the rational
         */
        public Rational reduce()
        {
            // Handle 0s in numerator or denominator
            if( denominator==0 ) numerator=1;
            else if( numerator==0 ) denominator=1;
            else
            {
                // Reduce by dividing by GCD
                long g = gcd(Math.abs(numerator),Math.abs(denominator));
                numerator /= g;
                denominator /= g;
                
                // Can't have a negative denominator
                if( denominator<0 )
                {
                    numerator = -numerator;
                    denominator = -denominator;
                }
            }
            
            return this;
        }
        
        /**
         * Multiply this Rational by another, returning a new Rational.
         *
         * @param b the other Rational
         * @return the product
         */
        public Rational mult( Rational b )
        {
            return new Rational( numerator*b.numerator, denominator*b.denominator );
        }
        
        /**
         * Divide this Rational by another, returning a new Rational.
         *
         * @param b the other Rational
         * @return the dividend
         */
        public Rational div( Rational b )
        {
            return new Rational( numerator*b.denominator, denominator*b.numerator );
        }
        
        /**
         * Get this Rational's reciprocal
         *
         * @return the reciprocal
         */
        public Rational rec()
        {
            return new Rational( denominator, numerator );
        }
        
        /**
         * Negate this Rational.
         *
         * @return -this
         */
        public Rational neg()
        {
            return new Rational( -numerator, denominator );
        }
        
        /**
         * Add another rational to this one, returning a new Rational.
         *
         * @param b the other Rational
         * @return the sum
         */

        public Rational add( Rational b )
        {
            return new Rational( numerator*b.denominator+denominator*b.numerator, denominator*b.denominator );
        }
        
        /**
         * Subtract another rational from this one, returning a new Rational.
         *
         * @param b the other Rational
         * @return the difference
         */
        public Rational sub( Rational b )
        {
            return new Rational( numerator*b.denominator-denominator*b.numerator, denominator*b.denominator );
        }

        /**
         * Compare another Rational to this one.
         *
         * @param o the other rational
         * @return Standard for compareTo
         */
        @Override
        public int compareTo( Rational o )
        {
            return Long.compare( this.numerator*o.denominator, o.numerator*this.denominator );
        }
        
        /**
         * Determine if this Rational equals another.
         *
         * @param o the other
         * @return true, if the other is a Rational and they're equal
         */
        @Override
        public boolean equals( Object o )
        {
            boolean result = true;
            if( o==null || !(o instanceof Rational) ) result = false;
            else 
            {
                Rational r = (Rational)o;
                result = r.numerator==this.numerator && r.denominator==this.denominator;
            }
            
            return result;
        }
        
        /**
         * To string.
         *
         * @return the string
         */
        @Override
        public String toString()
        {
            return "" + numerator + " " + denominator;
        }
        
        /**
         * Determine if thie Rational is strictly bewtween two others.
         *
         * @param lo the lower bound
         * @param hi the upper bound
         * @return true, if between
         */
        public boolean between( Rational lo, Rational hi )
        {
            return compareTo( lo )>0 && compareTo( hi )<0;
        }        
        
    }
    
    /**
     * A Line, in y=mx+b form.
     *
     * @author vanb
     */
    private class Line
    {
        
        /** The m. */
        private Rational m;
        
        /** The b. */
        private Rational b;
        
        /**
         * Instantiates a new line.
         *
         * @param m the m (slope)
         * @param x the x of a point on the line
         * @param y the y of a point on the line
         */
        public Line( Rational m, Rational x, Rational y )
        {
            this.m = m;
            this.b = y.sub( m.mult( x ) );
        }
        
        /**
         * Gets the y for an x.
         *
         * @param theX the X
         * @return the y
         */
        public Rational getY( Rational theX )
        {
            return b.add( m.mult( theX ) );  
        }
        
        /**
         * Gets the x for a y.
         *
         * @param theY the Y
         * @return the x
         */
        public Rational getX( Rational theY )
        {
            return theY.sub( b ).div( m );
        }
        
    }
    
    /**
     * Do it!.
     */
    private void doit()
    {
        long a = sc.nextLong();
        long b = sc.nextLong();
        
        // We bounce n times, and then hit the wall one more time.
        int n = sc.nextInt()+1;
        
        // We start at (-1/1, 0/1)
        Rational x = new Rational( -1, 1 );
        Rational y = new Rational( 0, 1 );
        Rational m = new Rational( a, b );
        Line line = new Line( m, x, y );
        
        // These are constants we'll use a lot.
        Rational posone = new Rational( 1, 1 );
        Rational negone = new Rational( -1, 1 );
        
        while( n-->0 )
        {
            // Look on each of the edges of our square
            Rational yhi = line.getY( posone );
            Rational ylo = line.getY( negone );
            Rational xhi = line.getX( posone );
            Rational xlo = line.getX( negone );
            
            // If any of these values is strictle between -1 and 1, and not the point we came from, 
            // then it's the point we're going to.
            if( yhi.between( negone, posone ) && !yhi.equals( y ) && !x.equals( posone ) )
            {
                x = posone;
                y = yhi;
            }
            else if( ylo.between( negone, posone ) && !ylo.equals( y ) && !x.equals( negone ))
            {
                x = negone;
                y = ylo;
            }
            else if( xhi.between( negone, posone) && !xhi.equals( x ) && !y.equals( posone ) )
            {
                x = xhi;
                y = posone;
            }
            else if( xlo.between( negone, posone) && !xlo.equals( x ) && !y.equals( negone ) )
            {
                x = xlo;
                y = negone;
            }
                    
            // When we bounce, we negate the slope.
            m = m.neg();
            
            // And, we make a new line.
            line = new Line( m, x, y );
        }
        
        ps.println( x + " " + y );
    }
        
    /**
     * Main.
     *
     * @param args unused
     * @throws Exception the exception
     */
    public static void main( String[] args ) throws Exception
    {
        sc = new Scanner( System.in );
        ps = System.out;
        
        new squarebounce_vanb().doit();
    }

}
