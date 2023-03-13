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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace TestRunner
{
  //----------------------------------------------------------------------------
  //  Class Declarations
  //----------------------------------------------------------------------------
  //
  // Class Name: Problem
  // 
  /// Purpose:
  /// <summary>
  ///     All the data on the problem
  /// </summary>
  //
  //----------------------------------------------------------------------------
  class Problem
  {
    string mLocation = "";
    List<string> mSampleIn = new List<string>();
    List<string> mSampleOut = new List<string>();
    List<string> mSecretIn = new List<string>();
    List<string> mSecretOut = new List<string>();

    public Problem(string location)
    {
      mLocation = location;
      Debug.WriteLine(">>>"+location);

      string dir = location + "\\secret";
      string[] files = Directory.GetFiles(dir, "*.in");
      foreach (string fileToAdd in files)
      {
        mSecretIn.Add(fileToAdd);
      }
      files = Directory.GetFiles(dir, "*.dat");
      foreach (string fileToAdd in files)
      {
        mSecretIn.Add(fileToAdd);
      }
      files = Directory.GetFiles(dir, "*.out");
      foreach (string fileToAdd in files)
      {
        mSecretOut.Add(fileToAdd);
      }
      files = Directory.GetFiles(dir, "*.ans");
      foreach (string fileToAdd in files)
      {
        mSecretOut.Add(fileToAdd);
      }

      dir = location + "\\sample";
      files = Directory.GetFiles(dir, "*.in");
      foreach (string fileToAdd in files)
      {
        mSampleIn.Add(fileToAdd);
      }
      files = Directory.GetFiles(dir, "*.dat");
      foreach (string fileToAdd in files)
      {
        mSampleIn.Add(fileToAdd);
      }
      files = Directory.GetFiles(dir, "*.out");
      foreach (string fileToAdd in files)
      {
        mSampleOut.Add(fileToAdd);
      }
      files = Directory.GetFiles(dir, "*.ans");
      foreach (string fileToAdd in files)
      {
        mSampleOut.Add(fileToAdd);
      }
    }
  }
}
