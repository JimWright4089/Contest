using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNW2015ProbA
{
#if notX
    class Program
    {
        public static int mNumberOfAirports, mNumberOfFlight, vidx;
        public static int[] s, f, t, p, mAirportServices, vis;
        public static int[,] o, d;

        public static void Main(string[] args)
        {
            mNumberOfAirports = nextInt();
            mNumberOfFlight = nextInt();

            mAirportServices = new int[mNumberOfAirports];
            for (int i = 0; i < mNumberOfAirports; i++) mAirportServices[i] = nextInt();
            o = new int[mNumberOfAirports, mNumberOfAirports];
            d = new int[mNumberOfAirports, mNumberOfAirports];
            for (int i = 0; i < mNumberOfAirports; i++) for (int j = 0; j < mNumberOfAirports; j++)
                {
                    o[i, j] = nextInt();
                    d[i, j] = o[i, j] + (i == j ? 0 : mAirportServices[j]);
                }

            for (int k = 0; k < mNumberOfAirports; k++) for (int i = 0; i < mNumberOfAirports; i++) for (int j = 0; j < mNumberOfAirports; j++)
                        d[i, j] = Math.Min(d[i, j], d[i, k] + d[k, j]);

            s = new int[mNumberOfFlight];
            f = new int[mNumberOfFlight];
            t = new int[mNumberOfFlight];
            vis = new int[mNumberOfFlight];
            p = new int[mNumberOfFlight];
            for (int i = 0; i < mNumberOfFlight; i++)
            {
                s[i] = nextInt() - 1;
                f[i] = nextInt() - 1;
                t[i] = nextInt();
                vis[i] = 0;
                p[i] = -1;
            }

            vidx = 0;
            int c = 0;
            for (int i = 0; i < mNumberOfFlight; i++)
            {
                ++vidx;
                if (dfs(i))
                {
                    ++c;
                }
            }

            Console.WriteLine(mNumberOfFlight - c);
        }

        public static bool dfs(int u)
        {
            vis[u] = vidx;
            for (int v = 0; v < mNumberOfFlight; v++)
            {
                if (o[s[u], f[u]] + mAirportServices[f[u]] + d[f[u], s[v]] <= t[v] - t[u] &&
                    (p[v] == -1 || (vis[p[v]] != vidx && dfs(p[v]))))
                {
                    p[v] = u;
                    return true;
                }
            }
            return false;
        }

        private static string[] __tokens;
        private static int __tidx;
        public static int nextInt()
        {
            return int.Parse(next());
        }

        public static string next()
        {
            if (__tokens == null || __tidx == __tokens.Length)
            {
                __tidx = 0;
                __tokens = Console.ReadLine().Split(' ');
            }
            return __tokens[__tidx++];
        }
    }
#endif

    class Program
    {
        static void Main(string[] args)
        {
            char[] delims = { ' ', ':' };
            List<Airport> mAirports = new List<Airport>();
            List<Airplane> mPlanes = new List<Airplane>();

            string line = Console.ReadLine();
            string[] items = line.Split(delims);
            int numberOfAirports;
            int numberOfFlights;
            int.TryParse(items[0], out numberOfAirports);
            int.TryParse(items[1], out numberOfFlights);

            line = Console.ReadLine();
            items = line.Split(delims);

            for(int i=0;i<numberOfAirports;i++)
            {
                Airport newAirport = new Airport();
                int.TryParse(items[i], out newAirport.mService);
                newAirport.mTravelTo = new int[numberOfAirports];
                mAirports.Add(newAirport);

                line = Console.ReadLine();
                string[] items2 = line.Split(delims);

                for(int j=0;j<numberOfAirports;j++)
                {
                    int.TryParse(items2[j], out newAirport.mTravelTo[j]);
                }
            }

            for(int i=0;i<numberOfFlights;i++)
            {
                Flight newFlight = new Flight();
                line = Console.ReadLine();
                items = line.Split(delims);

                int endTime = 0;

                int.TryParse(items[0], out newFlight.mStartAirport);
                int.TryParse(items[1], out newFlight.mStopAirport);
                int.TryParse(items[2], out newFlight.mStart);
                newFlight.mStartAirport--;
                newFlight.mStopAirport--;

                endTime = mAirports[newFlight.mStartAirport].mService + 
                    mAirports[newFlight.mStartAirport].mTravelTo[newFlight.mStopAirport];
                newFlight.mStop = endTime + newFlight.mStart;

                bool foundPlane = false;
                for(int j=0;j<mPlanes.Count;j++)
                {
                    bool found = false;
                    for (int k = 0; k < mPlanes[j].mFlights.Count; k++)
                    {
                        if(true == FlightColide(mPlanes[j].mFlights[k],newFlight))
                        {
                            found = true;
                            break;
                        }
                    }
                    if(false == found)
                    {
                        // find the flight prev to this.
                        int maxEnd = 0;
                        int prevFlight = -1;
                        int maxStart = 0;
                        int nextFlight = -1;
                        for (int k = 0; k < mPlanes[j].mFlights.Count; k++)
                        {
                            if (mPlanes[j].mFlights[k].mStop < newFlight.mStart)
                            {
                                if (maxEnd < mPlanes[j].mFlights[k].mStop)
                                {
                                    maxEnd = mPlanes[j].mFlights[k].mStop;
                                    prevFlight = k;
                                }
                            }
                            if (mPlanes[j].mFlights[k].mStart > newFlight.mStop)
                            {
                                if (maxStart < mPlanes[j].mFlights[k].mStart)
                                {
                                    maxStart = mPlanes[j].mFlights[k].mStart;
                                    nextFlight = k;
                                }
                            }
                        }

                        // If the flight is the first of the day
                        if(-1 == prevFlight)
                        {
                            if(newFlight.mStopAirport != mPlanes[j].mFlights[nextFlight].mStartAirport)
                            {
                                Flight midFlight = new Flight();
                                midFlight.mStartAirport = newFlight.mStopAirport;
                                midFlight.mStopAirport = mPlanes[j].mFlights[nextFlight].mStartAirport;

                                endTime = mAirports[midFlight.mStartAirport].mService +
                                    mAirports[midFlight.mStartAirport].mTravelTo[midFlight.mStopAirport];
                                midFlight.mStop = endTime + midFlight.mStart;

                                if(false == FlightColide(midFlight,mPlanes[j].mFlights[nextFlight]))
                                {
                                    mPlanes[j].mFlights.Add(newFlight);
                                    mPlanes[j].mFlights.Add(midFlight);
                                    found = true;
                                    foundPlane = true;
                                    break;
                                }
                            }
                            else
                            {
                                mPlanes[j].mFlights.Add(newFlight);
                                found = true;
                                foundPlane = true;
                                break;
                            }
                        }
                        else
                        {
                            if (newFlight.mStartAirport != mPlanes[j].mFlights[prevFlight].mStopAirport)
                            {
                                Flight midFlight = new Flight();
                                midFlight.mStartAirport = newFlight.mStartAirport;
                                midFlight.mStopAirport = mPlanes[j].mFlights[prevFlight].mStopAirport;
                                midFlight.mStart = maxEnd;

                                endTime = mAirports[midFlight.mStartAirport].mService +
                                    mAirports[midFlight.mStartAirport].mTravelTo[midFlight.mStopAirport];
                                midFlight.mStop = endTime + midFlight.mStart;

                                if (false == FlightColide(midFlight, newFlight))
                                {
                                    mPlanes[j].mFlights.Add(newFlight);
                                    mPlanes[j].mFlights.Add(midFlight);
                                    found = true;
                                    foundPlane = true;
                                    break;
                                }
                            }
                            else
                            {
                                mPlanes[j].mFlights.Add(newFlight);
                                found = true;
                                foundPlane = true;
                                break;
                            }
                        }
                    }
                }

                if (false == foundPlane)
                {
                    Airplane newPlane = new Airplane();
                    newPlane.mFlights.Add(newFlight);
                    mPlanes.Add(newPlane);
                }
            }
            Console.WriteLine(mPlanes.Count);
        }

        public static bool FlightColide(Flight flightA, Flight flightB)
        {
            bool returnValue = false;

            // flight begins in the other flight
            if ((flightA.mStart >= flightB.mStart) &&
                (flightA.mStart <= flightB.mStop))
            {
                return true;
            }

            // flight ends in the other flight
            if ((flightA.mStop <= flightB.mStart) &&
                (flightA.mStop >= flightB.mStop))
            {
                return true;
            }

            // flight begins in the other flight
            if ((flightB.mStart >= flightA.mStart) &&
                (flightB.mStart <= flightA.mStop))
            {
                return true;
            }

            // flight ends in the other flight
            if ((flightB.mStop <= flightA.mStart) &&
                (flightB.mStop >= flightA.mStop))
            {
                return true;
            }

            // flight A is in B
            if((flightA.mStart>=flightB.mStart)&&
                (flightA.mStop<=flightB.mStop))
            {
                return true;
            }

            // flight B is in A
            if((flightB.mStart>=flightA.mStart)&&
                (flightB.mStop<=flightA.mStop))
            {
                return true;
            }

            return returnValue;
        }
    }

    public class Airport
    {
        public int mService;
        public int[] mTravelTo;
    }

    public class Flight
    {
        public int mStart = 0;
        public int mStop = 0;
        public int mStartAirport = 0;
        public int mStopAirport = 0;
    }

    public class Airplane
    {
        public List<Flight> mFlights = new List<Flight>();
    }
}
