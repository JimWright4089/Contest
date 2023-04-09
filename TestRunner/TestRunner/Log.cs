using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestRunner
{
  class Log
  {
    static string mFilename = "";

    static Verbose mVerbose = Verbose.ERRORS;

    /// <summary>Verbose amount of text to the log file</summary>
    public enum Verbose
    {
      /// <summary>every message</summary>
      FULL,
      /// <summary>no headers but everything else</summary>
      NO_HEADERS,
      /// <summary>no headers, and array data but everything else</summary>
      NO_ARRAYS,
      /// <summary>just errors</summary>
      ERRORS
    }

    static public void OpenFile(string filename, Verbose verbose)
    {
      if (!Directory.Exists(".\\Logs\\"))
      {
        Directory.CreateDirectory(".\\Logs\\");
      }

      mFilename = ".\\Logs\\Log$" + filename + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("D2") + ".txt";
      StreamWriter logFile = new StreamWriter(mFilename, true);

      logFile.WriteLine("");
      logFile.WriteLine("--------------------------------------------------------------------------------");
      logFile.WriteLine("Starting at:" + DateTime.Now.ToString());
      logFile.Write("Verbose:");
      if (Verbose.ERRORS == verbose)
      {
        logFile.WriteLine("Errors");
      }
      else
      {
        if (Verbose.NO_HEADERS == verbose)
        {
          logFile.WriteLine("No Header");
        }
        else
        {
          logFile.WriteLine("Full");
        }
      }
      mVerbose = verbose;
      logFile.Close();
    }

    static public void WriteMessage(string message)
    {
      if (("" != mFilename) && (Verbose.ERRORS != mVerbose))
      {
        StreamWriter logFile = new StreamWriter(mFilename, true);
        logFile.WriteLine(message.Trim());
        logFile.Flush();
        logFile.Close();
      }
    }
  }
}
