//----------------------------------------------------------------------------
//
//  $Workfile: ConfigFile.cs$
//
//  $Revision: X$
//
//  Project:    ICPC Test Runner
//
//                            Copyright (c) 2023
//                                Jim Wright
//                            All Rights Reserved
//
//  Modification History:
//  $Log:
//  $
//
//----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Using
//----------------------------------------------------------------------------
using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;

namespace TestRunner
{
  //----------------------------------------------------------------------------
  //  Class Declarations
  //----------------------------------------------------------------------------
  //
  // Class Name: TestRunner
  // 
  // Purpose:
  //      Handle the Test Runner form
  //
  //----------------------------------------------------------------------------
  public partial class TestRunner : Form
  {
    string[] YEARS = { "1986", "1987", "1988", "1989", "1990", "1991", "1992", "1993",
                       "1994", "1995", "1996", "1997", "1998", "1999", "2000", "2001",
                       "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009",
                       "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017",
                       "2018", "2019", "2020", "2021", "2022", "2023" };
    string[] COMPS = {  "ICPC_ECUSA", "ICPC_GNYUSA", "ICPC_MAUSA", "ICPC_MCUSA",
                        "ICPC_NCUSA", "ICPC_NEUSA", "ICPC_PacNW", "ICPC_RMUSA",
                        "ICPC_SCUSA", "ICPC_SEUSA", "ICPC_SouthCal", "ICPC_Worlds" };
    string[] DIRS = { "ContestData", "Docs", "Solutions" };
    string[] OLD_ANSWER_DIRS = { "output", "input" };
    string[] CUR_ANSWER_DIRS = { "sample", "secret" };
    string[] OLD_ANSWER_Files = { "*.in", "*.out", "*.ans" };

    const string ROOT_DIR = "..\\";
    ConfigFile mConfig = ConfigFile.GetInstance();
    Tests mTests = Tests.GetInstance();
    string[] mYears = { "19", "20" };

    public TestRunner()
    {
      InitializeComponent();
      Log.OpenFile("TestRunner", Log.Verbose.FULL);
    }

    bool IsDirectory(string dir, string cent)
    {
      int loc = dir.IndexOf(cent);

      if (3 == loc)
      {
        return true;
      }
      return false;
    }

    private void TestRunner_Load(object sender, EventArgs e)
    {
      mConfig.loadConfigFile();
      Scan();
      cbYear.Items.Clear();
      List<string> years = mTests.GetYears();

      foreach (string year in years)
      {
        cbYear.Items.Add(year);
      }
      cbYear.Text = mConfig.Year;
    }

    private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
    {
      mConfig.Year = cbYear.Text;
      mConfig.saveConfigFile();

      List<string> comps = mTests.GetComp(cbYear.Text);
      cbCompetition.Items.Clear();
      foreach (string comp in comps)
      {
        cbCompetition.Items.Add(comp);
      }
      cbCompetition.Text = mConfig.Comp;

    }

    void ProcessProblems(string dir, string year, string comp)
    {
      try
      {
        string[] subdirectoryEntries = Directory.GetDirectories(dir + "\\ContestData");

        foreach (string test in subdirectoryEntries)
        {
          string problem = test.Substring(test.LastIndexOf("\\") + 1);
          Debug.WriteLine("     " + test + " " + problem);
          mTests.AddIfNew(year, comp, problem, test);
        }
      }
      catch (System.IO.DirectoryNotFoundException)
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
        Debug.WriteLine("  " + comp + " " + competition);
        ProcessProblems(comp, year, competition);
      }
    }

    private void Scan()
    {
      string[] subdirectoryEntries = Directory.GetDirectories(ROOT_DIR);

      foreach (string dir in subdirectoryEntries)
      {
        foreach (string cent in mYears)
        {
          if (true == IsDirectory(dir, cent))
          {
            string year = dir.Substring(dir.LastIndexOf("\\") + 1);
            Debug.WriteLine(dir + " " + year);
            ProcessComps(dir, year);
          }
        }
      }
    }

    private void bScan_Click(object sender, EventArgs e)
    {
      Scan();
      mTests.saveConfigFile();
    }

    //--------------------------------------------------------------------
    // Purpose:
    //     Fix the ContestData directory
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    private void FixContestData(string contestDataDir)
    {
      string[] files;
      string[] dirs;
      string dir;

      //--------------------------------------------------------------------
      //  Check to see if there is a data directory
      //--------------------------------------------------------------------
      dir = contestDataDir + "\\data";
      if (true == Directory.Exists(dir))
      {
        Debug.WriteLine(dir + " problem");
      }

      //--------------------------------------------------------------------
      //  Move the directories under data
      //--------------------------------------------------------------------
      string[] subdirectoryEntries = Directory.GetDirectories(contestDataDir);
      foreach (string comp in subdirectoryEntries)
      {
        string testDir = comp + "\\data";

        if (true == Directory.Exists(testDir))
        {
          string[] subdirectoryEntries2 = Directory.GetDirectories(testDir);
          foreach (string comp2 in subdirectoryEntries2)
          {
            string name = comp2.Substring(comp2.LastIndexOf("\\") + 1);

            string newName = comp + "\\" + name;
            try
            {
              Directory.Move(comp2, newName);
            }
            catch (Exception)
            {
              Debug.WriteLine(comp2 + " to " + newName);
            }
          }

          //--------------------------------------------------------------------
          //  Delete the remaining directory
          //--------------------------------------------------------------------
          subdirectoryEntries2 = Directory.GetDirectories(testDir);
          if (0 == subdirectoryEntries2.Length)
          {
            Debug.WriteLine(" delete " + testDir);
            try
            {
              Directory.Delete(testDir);
            }
            catch (Exception)
            {
              Debug.WriteLine(" delete " + testDir + " Not Empty");
            }
          }
          else
          {
            Debug.WriteLine(" delete " + testDir + " Not Empty");
          }
        }
      }

      //--------------------------------------------------------------------
      //  Check for old input/output dir
      //--------------------------------------------------------------------
      subdirectoryEntries = Directory.GetDirectories(contestDataDir);
      foreach (string comp in subdirectoryEntries)
      {
        foreach (string answerDir in CUR_ANSWER_DIRS)
        {
          dir = comp + "\\" + answerDir;
          if (false == Directory.Exists(dir))
          {
            Debug.WriteLine(dir + " creating");
            Directory.CreateDirectory(dir);
          }
        }

        foreach (string answerDir in OLD_ANSWER_DIRS)
        {
          dir = comp + "\\" + answerDir;
          string dirSample = comp + "\\sample";
          string dirSecret = comp + "\\secret";
          if (true == Directory.Exists(dir))
          {
            Debug.WriteLine(dir + " problem");

            files = Directory.GetFiles(dir);
            foreach (string theFile in files)
            {
              string fileName = theFile.Substring(theFile.LastIndexOf("\\") + 1);
              string fileDirSample = dirSample + "\\" + fileName;
              string fileDirSecret = dirSecret + "\\" + fileName;

              Debug.WriteLine("[" + theFile + "] [" + fileDirSample + "] [" + fileDirSecret + "]");

              File.Copy(theFile, fileDirSample);
              File.Move(theFile, fileDirSecret);
            }

            files = Directory.GetFiles(dir);
            dirs = Directory.GetDirectories(dir);

            if ((0 == files.Length) && (0 == dirs.Length))
            {
              Directory.Delete(dir);
            }
            else
            {
              Debug.WriteLine("dir:" + dir + " is not empty");
            }
          }
        }
      }

      //--------------------------------------------------------------------
      //  Just Files
      //--------------------------------------------------------------------
      files = Directory.GetFiles(contestDataDir);
      dirs = Directory.GetDirectories(contestDataDir);

      if ((0 == dirs.Length) && (0 != files.Length))
      {
        Debug.WriteLine(contestDataDir + " needs Fixing");
        files = Directory.GetFiles(contestDataDir, "*.in");

        foreach (string theFile in files)
        {
          string fileBase = theFile.Substring(0, theFile.LastIndexOf("."));
          string fileName = fileBase.Substring(fileBase.LastIndexOf("\\") + 1);
          Debug.WriteLine("Creating:" + fileBase);
          Directory.CreateDirectory(fileBase);
          string[] fileToMove = Directory.GetFiles(contestDataDir, fileName + ".*");
          foreach (string moveFile in fileToMove)
          {
            string newFileName = moveFile.Substring(moveFile.LastIndexOf("\\") + 1);
            Debug.WriteLine("  Moving:" + moveFile + " to:" + fileBase + "\\" + newFileName);
            File.Move(moveFile, fileBase + "\\" + newFileName);
          }
        }
      }
      //--------------------------------------------------------------------
      //  Old Sol files
      //--------------------------------------------------------------------
      dirs = Directory.GetDirectories(contestDataDir);
      foreach (string dirToCheck in dirs)
      {
        foreach (string fileMatch in OLD_ANSWER_Files)
        {
          files = Directory.GetFiles(dirToCheck, fileMatch);
          foreach (string fileMatchMove in files)
          {
            string fileName = fileMatchMove.Substring(fileMatchMove.LastIndexOf("\\") + 1);
            int loc = fileName.LastIndexOf("sample");

            if (-1 != loc)
            {
              Debug.WriteLine("Move:" + fileMatchMove + " To" + dirToCheck + "\\sample\\" + fileName + " " + loc);
              File.Move(fileMatchMove, dirToCheck + "\\sample\\" + fileName);
            }
            else
            {
              Debug.WriteLine("Move:" + fileMatchMove + " To" + dirToCheck + "\\secret\\" + fileName + " " + loc);
              File.Move(fileMatchMove, dirToCheck + "\\secret\\" + fileName);
            }
          }
        }
      }
      //--------------------------------------------------------------------
      //  Sample Dir Empty
      //--------------------------------------------------------------------
      dirs = Directory.GetDirectories(contestDataDir);
      foreach (string dirToCheck in dirs)
      {
        files = Directory.GetFiles(dirToCheck + "\\sample");
        if (0 == files.Length)
        {
          files = Directory.GetFiles(dirToCheck + "\\secret");
          foreach (string fileToCopy in files)
          {
            string fileNameBase = fileToCopy.Substring(fileToCopy.LastIndexOf("\\"));
            Debug.WriteLine("Copy:" + fileToCopy + " to:" + dirToCheck + "\\sample" + fileNameBase);
            Debug.WriteLine("Copy:" + fileToCopy + " to:" + dirToCheck + "\\sample" + fileNameBase);
            File.Copy(fileToCopy, dirToCheck + "\\sample" + fileNameBase);
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
          Debug.WriteLine("Bad Comp Dir:" + compDir + "\\" + thecomp);
        }
      }

      FixContestData(compDir + "\\ContestData");
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

        if (false == found)
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
      Debug.WriteLine("Done");
    }

    private void cbCompetition_SelectedIndexChanged(object sender, EventArgs e)
    {
      mConfig.Comp = cbCompetition.Text;
      mConfig.saveConfigFile();

      List<string> probs = mTests.GetProblems(cbYear.Text, cbCompetition.Text);
      cbProblem.Items.Clear();
      foreach (string prob in probs)
      {
        cbProblem.Items.Add(prob);
      }
      cbProblem.Text = mConfig.Prob;

    }

    private void cbProblem_SelectedIndexChanged(object sender, EventArgs e)
    {
      mConfig.Prob = cbProblem.Text;
      mConfig.saveConfigFile();

      string sampleDataDir = ROOT_DIR + "\\" + cbYear.Text + "\\" + cbCompetition.Text + "\\ContestData\\" + cbProblem.Text + "\\sample";
      string secretDataDir = ROOT_DIR + "\\" + cbYear.Text + "\\" + cbCompetition.Text + "\\ContestData\\" + cbProblem.Text + "\\secret";

      Debug.WriteLine("Sample:" + sampleDataDir);
      Debug.WriteLine("Secret:" + secretDataDir);

      lvTestData.Items.Clear();
      string[] files = Directory.GetFiles(sampleDataDir, "*.in");
      foreach (string file in files)
      {
        string fileName = file.Substring(file.LastIndexOf("\\"));
        ListViewItem item = new ListViewItem(fileName);

        item.SubItems.Add("sample");
        lvTestData.Items.Add(item);
      }
      files = Directory.GetFiles(secretDataDir, "*.in");
      foreach (string file in files)
      {
        string fileName = file.Substring(file.LastIndexOf("\\"));
        ListViewItem item = new ListViewItem(fileName);
        item.SubItems.Add("secret");
        lvTestData.Items.Add(item);
      }
    }

    private void RunTheTest(string test, string root)
    {
      string program = test;
      string rootDirectory = root;
      char[] delims = { '.' };
      char[] delims2 = { '\\' };
      int totalErrors = 0;
      int totalErrorFiles = 0;

      Log.WriteMessage("Running:" + test + " Starting at:" + DateTime.Now.ToString());

      foreach (string inFiles in Directory.GetFiles(root, "*.in"))
      {
        string outFileX = inFiles.Substring(0, inFiles.LastIndexOf("."));
        string outFile = outFileX + ".ans";

        if (false == File.Exists(outFile))
        {
          outFile = outFileX + ".out";

          if (false == File.Exists(outFile))
          {
            lbResult.Items.Add("problem with problem can't find output file" + outFile);
            Log.WriteMessage("problem with problem can't find output file" + outFile);
            return;
          }
        }

        string toRun = program + " <" + inFiles + " >text.ans";
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
        // Start the process with the info we specified.
        // Call WaitForExit and then the using-statement will close.
        using (Process exeProcess = Process.Start(startInfo))
        {
          try
          {
            exeProcess.WaitForExit(10000);
          }
          catch
          {
            lbResult.Items.Add("Time Exceded:" + outFile);
            Log.WriteMessage("Time Exceded:" + outFile);
            exeProcess.Kill();
            totalErrors++;
            totalErrorFiles++;
          }

          if (false == exeProcess.HasExited)
          {
            lbResult.Items.Add("Time Exceded:" + outFile);
            Log.WriteMessage("Time Exceded:" + outFile);
            try
            {
              exeProcess.Kill();
              Thread.Sleep(4000);
              totalErrors++;
              totalErrorFiles++;
            }
            catch (Exception)
            {

            }
          }
        }
        bool newFile = true;
        lbResult.Items.Add(outFile + ", Run Time:" + DateTime.Now.Subtract(start).TotalMilliseconds.ToString("f2") + "ms");
        Log.WriteMessage(outFile + ", Run Time:" + DateTime.Now.Subtract(start).TotalMilliseconds.ToString("f2") + "ms");

        StreamReader inFile = new StreamReader(outFile);
        try
        {
          StreamReader textFile = new StreamReader("text.ans");

          while (!inFile.EndOfStream)
          {
            string inLine = inFile.ReadLine();
            string textLine = textFile.ReadLine();

            if (inLine != textLine)
            {
              lbResult.Items.Add("   Should be:" + inLine + " Got:" + textLine);
              Log.WriteMessage("   Should be:" + inLine + " Got:" + textLine);
              totalErrors++;
              if (true == newFile)
              {
                totalErrorFiles++;
                newFile = false;
              }
            }
          }
          textFile.Close();
        }
        catch (Exception)
        {
          lbResult.Items.Add("No output for file:" + outFile);
          Log.WriteMessage("No output for file:" + outFile);
          totalErrors++;
          totalErrorFiles++;
        }
        inFile.Close();
      }
      lbResult.Items.Add("Errors:" + totalErrors.ToString() + " in files:" + totalErrorFiles.ToString());
      Log.WriteMessage("Errors:" + totalErrors.ToString() + " in files:" + totalErrorFiles.ToString());
    }

    private void bBrowse_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Executable Files (.exe)|*.exe|All Files (*.*)|*.*";
      openFileDialog.FilterIndex = 1;
      openFileDialog.Multiselect = false;
      DialogResult dialogResult = openFileDialog.ShowDialog();
      if (DialogResult.OK == dialogResult)
      {
        tbRun.Text = openFileDialog.FileName;
      }
    }

    private void bRun_Click(object sender, EventArgs e)
    {
      string sampleDataDir = ROOT_DIR + "\\" + cbYear.Text + "\\" + cbCompetition.Text + "\\ContestData\\" + cbProblem.Text + "\\sample";
      string secretDataDir = ROOT_DIR + "\\" + cbYear.Text + "\\" + cbCompetition.Text + "\\ContestData\\" + cbProblem.Text + "\\secret";

      Debug.WriteLine("Sample:" + sampleDataDir);
      Debug.WriteLine("Secret:" + secretDataDir);

      lbResult.Items.Clear();
      Log.WriteMessage(" ");

      RunTheTest(tbRun.Text, sampleDataDir);
      RunTheTest(tbRun.Text, secretDataDir);
    }
  }
}
