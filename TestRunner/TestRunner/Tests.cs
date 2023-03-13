//----------------------------------------------------------------------------
//
//  $Workfile: Tests.cs$
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
using System.Xml;
using System.Collections.Generic;

namespace TestRunner
{
  //----------------------------------------------------------------------------
  //  Class Declarations
  //----------------------------------------------------------------------------
  //
  // Class Name: Tests
  // 
  /// Purpose:
  /// <summary>
  ///     Keep track of the tests and answers
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
    Dictionary<string, Dictionary<string, 
        Dictionary<string, Problem>>> mTests = new Dictionary<string, 
                      Dictionary<string, Dictionary<string, Problem>>>();
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
        mTests.Add(year, new Dictionary<string, Dictionary<string, Problem>>());
      }

      Dictionary<string, Dictionary<string, Problem>> theYear = mTests[year];
      keyExists = theYear.ContainsKey(comp);
      if (keyExists)
      {
        Console.WriteLine("{0} exists in map", comp);
      }
      else
      {
        Console.WriteLine("{0} does not exist in map", comp);
        theYear.Add(comp, new Dictionary<string, Problem>());
      }

      Dictionary<string, Problem> theComp = theYear[comp];
      keyExists = theYear.ContainsKey(problem);
      if (keyExists)
      {
        Console.WriteLine("{0} exists in map", comp);
      }
      else
      {
        Console.WriteLine("{0} does not exist in map", comp);
        theComp.Add(problem, new Problem(dir));
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

