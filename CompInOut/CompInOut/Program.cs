using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace CompInOut
{
    class Program
    {
        static void Main(string[] args)
        {
            string program = args[0];
            string rootDirectory = args[1];
            char[] delims = { '.' };
            char[] delims2 = { '\\' };
            string inDirectory = rootDirectory + "\\input";
            string outDirectory = rootDirectory + "\\output";

            foreach (string inFiles in Directory.GetFiles(inDirectory,"*.in"))
            {
                string[] parts = inFiles.Split(delims);
                string outFilePart = parts[0] + ".out";
                parts = outFilePart.Split(delims2);
                string outFile = outDirectory + "\\"+parts[parts.Length - 1];

                string toRun = program + " <" + inFiles + " >text.out";
                StreamWriter myBatFile = new StreamWriter("Run.bat");
                myBatFile.WriteLine("echo off");
                myBatFile.WriteLine(toRun);
                myBatFile.Close();

                // Use ProcessStartInfo class.
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = "Run.bat";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                DateTime start = DateTime.Now;
                List<string> errors = new List<string>();
                try
                {
                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using-statement will close.
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }
                }
                catch
                {
                    // Log error.
                }
                Console.WriteLine(outFile + ", Run Time:" + DateTime.Now.Subtract(start).TotalMilliseconds.ToString("f2") + "ms");

                StreamReader inFile = new StreamReader(outFile);
                StreamReader textFile = new StreamReader("text.out");

                while(!inFile.EndOfStream)
                {
                    string inLine = inFile.ReadLine();
                    string textLine = textFile.ReadLine();

                    if(inLine != textLine)
                    {
                        Console.WriteLine("   Should be:" + inLine+ " Got:"+textLine);
                    }
                }
                inFile.Close();
                textFile.Close();
            }
        }
    }
}
