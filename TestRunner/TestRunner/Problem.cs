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
  }
}
