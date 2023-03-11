//----------------------------------------------------------------------------
//
//  $Workfile: AES_Common.cs$
//
//  $Revision: 11$
//
//  Project:    AES Libraries
//
//                            Copyright (c) 2022
//                Astronics Advanced Electronic Systems Corp.
//                            All Rights Reserved
//
//     This software may not be reproduced, in part or in its entirety,
//                  without the express written permission
//                 of Astronics Advanced Electronic Systems Corp.
//
//  Modification History:
//  $Log:
//   11   Libraries  1.10        6/26/2017 2:55:27 PM   James Wright    Added
//        the wave Or/Not/And methods for the binary wave
//   10   Libraries  1.9         10/26/2016 12:35:11 PM James Wright
//        CR13773: Added the file headers
//   9    Libraries  1.8         10/25/2016 6:39:42 PM  James Wright
//        CR13773:check in the version numbers, the rev numbers, and fix the
//        report to show the version numbers.
//   8    Libraries  1.7         10/19/2016 11:05:26 AM James Wright
//        CR13773:Make a few methods private
//   7    Libraries  1.6         10/12/2016 1:32:25 PM  James Wright
//        CR13773:Check in clean up
//   6    Libraries  1.5         10/12/2016 10:55:00 AM James Wright
//        CR13773:Change the name from SM15XX to SM1516
//   5    Libraries  1.4         9/21/2016 10:28:09 AM  James Wright
//        CR13773:Check in the common changes
//   4    Libraries  1.3         8/29/2016 6:45:06 PM   James Wright
//        CR13773:The ten unit test code
//   3    Libraries  1.2         8/4/2016 3:22:06 PM    James Wright
//        CR13773:Check in the RS485
//   2    Libraries  1.1         7/29/2016 10:34:44 AM  James Wright
//        CR13773:Update the DMM
//   1    Libraries  1.0         7/27/2016 3:27:33 PM   James Wright     
//  $
//
//----------------------------------------------------------------------------
using System;
using System.Xml;
using System.Collections.Generic;

namespace TestRunner
{
  //----------------------------------------------------------------------------
  //  Class Declarations
  //----------------------------------------------------------------------------
  //
  // Class Name: ConfigFile
  // 
  /// Purpose:
  /// <summary>
  ///     Hold all of the devices on the network
  /// </summary>
  //
  //----------------------------------------------------------------------------
  public class Tests
  {
    //----------------------------------------------------------------------------
    //  Class Constants 
    //----------------------------------------------------------------------------
    const string YEAR_TAG = "year";
    const string COMP_TAG = "comp";
    const string PROB_TAG = "prob";
    const string LOCATION_TAG = "location";

    const string FILE_NAME = "Tests.xml";

    //----------------------------------------------------------------------------
    //  Class Attributes
    //----------------------------------------------------------------------------
    Dictionary<string, Dictionary<string, Dictionary<string, string>>> mTests = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
    static Tests mInstance = null;

    //--------------------------------------------------------------------
    // Purpose:
    //     Constructor
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    private Tests()
    {
      loadConfigFile();
    }

    //--------------------------------------------------------------------
    // Purpose:
    //     Return the singleton
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    public static Tests GetInstance()
    {
      if (null == mInstance)
      {
        mInstance = new Tests();
      }
      return mInstance;
    }

    public void AddIfNew(string year, string comp, string problem, string dir)
    {
      bool keyExists = mTests.ContainsKey(year);
      if (keyExists)
      {
        Console.WriteLine("{0} exists in map", year);
      }
      else
      {
        Console.WriteLine("{0} does not exist in map", year);
        mTests.Add(year, new Dictionary<string, Dictionary<string, string>>());
      }

      Dictionary<string, Dictionary<string, string>> theYear = mTests[year];
      keyExists = theYear.ContainsKey(comp);
      if (keyExists)
      {
        Console.WriteLine("{0} exists in map", comp);
      }
      else
      {
        Console.WriteLine("{0} does not exist in map", comp);
        theYear.Add(comp, new Dictionary<string, string>());
      }

      Dictionary<string, string> theComp = theYear[comp];
      keyExists = theYear.ContainsKey(problem);
      if (keyExists)
      {
        Console.WriteLine("{0} exists in map", comp);
      }
      else
      {
        Console.WriteLine("{0} does not exist in map", comp);
        theComp.Add(problem, dir);
      }
    }

    //--------------------------------------------------------------------
    // Purpose:
    //     Load the Config XML file
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    public void loadConfigFile()
    {
      try
      {
        XmlTextReader reader = new XmlTextReader(FILE_NAME);

        while (reader.Read())
        {
          switch (reader.NodeType)
          {
            case XmlNodeType.Element: // The node is an element.
              if (YEAR_TAG == reader.Name)
              {
                reader.Read();
//                mYear = reader.Value;
              }
              break;
          }
        }
        reader.Close();
      }
      catch (Exception)
      {
      }
    }
    //--------------------------------------------------------------------
    // Purpose:
    //     Save Config XML file
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    public void saveConfigFile()
    {
      try
      {
        XmlDocument document = new XmlDocument();

        XmlNode newNode = document.CreateNode("element", YEAR_TAG, "");
//        newNode.InnerText = mYear;
        document.AppendChild(newNode);
        document.Save(FILE_NAME);
      }
      catch (Exception)
      {

      }
    }
  }
}

