using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace TestRunner
{
  public partial class TestRunner : Form
  {
    string[] YEARS = { "1986", "1987", "1988", "1989", "1990", "1991", "1992", "1993",
                       "1994", "1995", "1996", "1997", "1998", "1999", "2000", "2001",
                       "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009",
                       "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", 
                       "2018", "2019", "2020", "2021", "2022", "2023" };
    string[] COMPS = {  "ICPC_ECUSA", "ICPC_GNYUSA", "ICPC_MAUSA", "ICPC_MCUSA",
                        "ICPC_NCUSA", "ICPC_NEUSA", "ICPC_PacNW", "ICPC_RMUSA",
                        "ICPC_SCUSA", "ICPC_SEUSA", "ICPC_SouthCal" };
    string[] DIRS = { "ContestData", "Docs", "Solutions"};

    const string ROOT_DIR = "..\\";
    ConfigFile mConfig = ConfigFile.GetInstance();
    Tests mTests = Tests.GetInstance();
    string[] mYears = { "19", "20"};

    public TestRunner()
    {
      InitializeComponent();
    }

    bool IsDirectory(string dir, string cent)
    {
      int loc = dir.IndexOf(cent);

      if(3 == loc)
      {
        return true;
      }
      return false;
    }

    private void TestRunner_Load(object sender, EventArgs e)
    {
      string[] subdirectoryEntries = Directory.GetDirectories(ROOT_DIR);

      foreach(string dir in subdirectoryEntries)
      {
        foreach (string cent in mYears)
        {
          if (true == IsDirectory(dir, cent))
          {
            cbYear.Items.Add(dir.Substring(3));
          }
        }
      }

      cbYear.Text = mConfig.Year;
    }

    private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
    {
      mConfig.Year = cbYear.Text;
      mConfig.saveConfigFile();
    }

    void ProcessProblems(string dir, string year, string comp)
    {
      try
      {
        string[] subdirectoryEntries = Directory.GetDirectories(dir + "\\ContestData");

        foreach (string test in subdirectoryEntries)
        {
          string problem = test.Substring(test.LastIndexOf("\\") + 1);
          Debug.WriteLine("     " + test+" "+problem);
          mTests.AddIfNew(year, comp, problem, test);
        }
      }
      catch(System.IO.DirectoryNotFoundException e)
      {
        Debug.WriteLine("Badly Formated");
      }
      catch (Exception e)
      {
        Debug.WriteLine(e.Message);
      }
    }

    void ProcessComps(string dir, string year)
    {
      string[] subdirectoryEntries = Directory.GetDirectories(dir);

      foreach (string comp in subdirectoryEntries)
      {
        string competition = comp.Substring(comp.LastIndexOf("\\") + 1);
        Debug.WriteLine("  "+comp+" "+competition);
        ProcessProblems(comp,year,competition);
      }
    }

    private void bScan_Click(object sender, EventArgs e)
    {
      string[] subdirectoryEntries = Directory.GetDirectories(ROOT_DIR);

      foreach (string dir in subdirectoryEntries)
      {
        foreach (string cent in mYears)
        {
          if (true == IsDirectory(dir, cent))
          {
            string year = dir.Substring(dir.LastIndexOf("\\")+1); 
            Debug.WriteLine(dir + " " + year);
            ProcessComps(dir,year);  
          }
        }
      }
    }

    private void FixComp(string compDir)
    {
      foreach (string dirComp in DIRS)
      {
        string dir = compDir + "\\" + dirComp;
        if (true != Directory.Exists(dir))
        {
          Debug.Write(dir + " ");
          Debug.WriteLine("needs");
          Directory.CreateDirectory(dir);
        }
      }

      string[] subdirectoryEntries = Directory.GetDirectories(compDir);
      foreach (string comp in subdirectoryEntries)
      {
        string thecomp = comp.Substring(comp.LastIndexOf("\\") + 1);

        bool found = false;
        foreach (string checkComp in DIRS)
        {
          if (checkComp == thecomp)
          {
            found = true;
            break;
          }
        }

        if (false == found)
        {
          Debug.WriteLine("Bad Comp Dir:" +compDir + "\\" + thecomp);
        }
      }
    }

    private void FixYear(string yearDir)
    {
      foreach (string comp in COMPS)
      {
        string dir = yearDir + "\\" + comp;
        if (true != Directory.Exists(dir))
        {
          Debug.Write(dir + " ");
          Debug.WriteLine("needs");
          Directory.CreateDirectory(dir);
        }
      }

      string[] subdirectoryEntries = Directory.GetDirectories(yearDir);
      foreach (string comp in subdirectoryEntries)
      {
        string thecomp = comp.Substring(comp.LastIndexOf("\\") + 1);

        bool found = false;
        foreach (string checkComp in COMPS)
        {
          if (checkComp == thecomp)
          {
            found = true;
            break;
          }
        }

        if(false == found)
        {
          Debug.WriteLine("Bad Comp:" + thecomp);
        }

        FixComp(comp);      

      }
    }

    private void bFix_Click(object sender, EventArgs e)
    {
      foreach (string year in YEARS)
      {
        string dir = ROOT_DIR + "\\" + year;
        if (true != Directory.Exists(dir))
        {
          Debug.Write(dir + " ");
          Debug.WriteLine("needs");
          Directory.CreateDirectory(dir);
        }
      }

      foreach (string year in YEARS)
      {
        string dir = ROOT_DIR + "\\" + year;
        FixYear(dir);
      }
    }
  }
}
