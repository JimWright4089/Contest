using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace PNW2013ProbA
{
    class Node
    {
        public string mName;
        public double mX,mY,mZ;
        public double mLength = double.MaxValue;
        public bool mVisit = false;

        public Node(string name,double x, double y, double z)
        {
            mName = name;
            mX = x;
            mY = y;
            mZ = z;
        }

        public Node()
        {
            mName = "";
            mX = 0;
            mY = 0;
            mZ = 0;
        }

        public void reset()
        {
            mLength = double.MaxValue;
            mVisit = false;
        }

        public double getDist(Node B)
        {
            double tempX = (( mX - B.mX ) * ( mX - B.mX ));
            double tempY = (( mY - B.mY ) * ( mY - B.mY ));
            double tempZ = ( ( mZ - B.mZ ) * ( mZ - B.mZ ) );

            double tempTotal = tempX+tempY+tempZ;

            double temp = Math.Sqrt(tempTotal);
            return temp;
        }
    }

    class Vertex
    {
        public Node mName1;
        public Node mName2;
        public double mDist;

        public Vertex(Node name1, Node name2)
        {
            mName1 = name1;
            mName2 = name2;
            mDist = mName1.getDist(mName2);
        }

        public void clearDist(double dist)
        {
            mDist = dist;
        }
    }

    class Graph
    {
        List<Node> mNodes = new List<Node>();
        List<Vertex> mVertexs = new List<Vertex>();

        public Graph()
        {
            mNodes = new List<Node>();
            mVertexs = new List<Vertex>();
        }

        public void addNewNode(string name, long x, long y, long z)
        {
            mNodes.Add(new Node(name,x,y,z));
        }

        Node getNode(string name)
        {
            for ( int i=0; i < mNodes.Count; i++ )
            {
                if ( name == mNodes[i].mName )
                {
                    return mNodes[i];
                }
            }
            return null;
        }

        void resetNodes()
        {
            for ( int i=0; i < mNodes.Count; i++ )
            {
                mNodes[i].reset();
            }
        }

        Vertex getVertex(string name1, string name2)
        {
            for ( int i=0; i < mVertexs.Count; i++ )
            {
                if ( ( mVertexs[i].mName1.mName == name1 ) && ( mVertexs[i].mName2.mName == name2 ) )
                {
                    return mVertexs[i];
                }
            }
            return null;
        }

        public void addNewVertex(string name1, string name2)
        {
            Node new1 = getNode(name1);
            Node new2 = getNode(name1);

            if((new1 != null)&&(new2 != null))
            {
                mVertexs.Add(new Vertex(new1,new2));
            }
        }

        public void setVertexDist(string name1, string name2, double dist)
        {
            Vertex newVertex = getVertex(name1,name2);

            if ( newVertex != null )
            {
                newVertex.clearDist(dist);
            }
        }

        public void buildVertexs()
        {
            for ( int i=0; i < mNodes.Count(); i++ )
            {
                for ( int j=0; j < mNodes.Count(); j++ )
                {
                    if ( i != j )
                    {
                        mVertexs.Add(new Vertex(mNodes[i], mNodes[j]));
                    }
                }
            }
        }

        public void printNodes()
        {
            for(int i=0;i<mNodes.Count();i++)
            {
                Console.WriteLine(  mNodes[i].mName + " " + 
                                    mNodes[i].mX + " " + 
                                    mNodes[i].mY + " " +
                                    mNodes[i].mZ + " " +
                                    mNodes[i].mLength +" "+
                                    mNodes[i].mVisit);
            }
        }

        public void printVertexs()
        {
            for ( int i=0; i < mVertexs.Count(); i++ )
            {
                Console.WriteLine(  mVertexs[i].mName1.mName + " " + 
                                    mVertexs[i].mName2.mName + " " + 
                                    mVertexs[i].mDist);
            }
        }

        public double getDistance(string name1, string name2)
        {
            return dijkstras(name1,name2);
        }

        private double dijkstras(string name1, string name2)
        {
            double dist = 0;

            resetNodes();

            Node curNode = getNode(name1);
            if(null != curNode)
            {
                curNode.mLength = 0.0;

                for ( int i=0; i < mNodes.Count; i++ )
                {
                    curNode.mVisit = true;

                    for ( int j=0; j < mNodes.Count; j++ )
                    {
                        if ( ( mNodes[j].mName != curNode.mName ) && ( false == mNodes[j].mVisit ) )
                        {
                            Vertex theVertex = getVertex(curNode.mName, mNodes[j].mName);
                            if(null != theVertex)
                            {
                                dist = curNode.mLength + theVertex.mDist;

                                if ( mNodes[j].mLength > dist )
                                {
                                    mNodes[j].mLength = dist;
                                }
                            }
                        }
                    }

                    dist = double.MaxValue;
                    for ( int j=0; j < mNodes.Count; j++ )
                    {
                        if ( false == mNodes[j].mVisit )
                        {
                            if ( mNodes[j].mLength < dist )
                            {
                                curNode = mNodes[j];
                                dist = mNodes[j].mLength;
                            }
                        }
                    }
                }

                for ( int i=0; i < mNodes.Count; i++ )
                {
                    if ( name2 == mNodes[i].mName )
                    {
                        dist = mNodes[i].mLength;
                    }
                }
            }
            return dist;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string numberOfString = Console.ReadLine();
            int numberOfTest;
            int.TryParse(numberOfString,out numberOfTest);

            for(int index=0;index<numberOfTest;index++)
            {
                RunTest(index+1);
            }
        }

        static void RunTest(int theCase)
        {
            string text;
            string name;
            string name1;
            string name2;
            long x;
            long y;
            long z;
            int count=0;
            Graph theGraph = new Graph();

            Console.WriteLine("Case "+theCase.ToString()+":");

            text = Console.ReadLine();
            int.TryParse(text, out count);

            for ( int index=0; index < count; index++ )
            {
                text = Console.ReadLine();
                string[] textTokens = text.Split(new char[1] { ' ' });
                name = textTokens[0];
                long.TryParse(textTokens[1], out x);
                long.TryParse(textTokens[2], out y);
                long.TryParse(textTokens[3], out z);

                theGraph.addNewNode(name,x,y,z);
            }

            theGraph.buildVertexs();

            text = Console.ReadLine();
            int.TryParse(text, out count);

            for ( int index=0; index < count; index++ )
            {
                text = Console.ReadLine();
                string[] textTokens = text.Split(new char[1] { ' ' });
                name1 = textTokens[0];
                name2 = textTokens[1];

                theGraph.setVertexDist(name1,name2,0.0);
            }

/*
            theGraph.printNodes();
            theGraph.printVertexs();
*/

            text = Console.ReadLine();
            int.TryParse(text, out count);
            for ( int index=0; index < count; index++ )
            {
                text = Console.ReadLine();
                string[] textTokens = text.Split(new char[1] { ' ' });
                name1 = textTokens[0];
                name2 = textTokens[1];
                double dist=theGraph.getDistance(name1,name2);
                Console.WriteLine("The distance from "+name1+" to "+name2+" is "+dist.ToString("f0")+" parsecs.");
            }
        }
    }
}
