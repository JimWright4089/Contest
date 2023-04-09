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
    const string YEARS_TAG = "years";
    const string COMPS_TAG = "comps";
    const string COMP_TAG = "comp";
    const string NAME_TAG = "name";
    const string PROBS_TAG = "probs";
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
    //     Return a list of years
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    public List<string> GetYears()
    {
      List<string> years = new List<string>();
      foreach (string key in mTests.Keys)
      {
        years.Add(key);
      }

      return years;
    }

    //--------------------------------------------------------------------
    // Purpose:
    //     A List of competions
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    public List<string> GetComp(string year)
    {
      List<string> comp = new List<string>();
      foreach (string key in mTests[year].Keys)
      {
        comp.Add(key);
      }

      return comp;
    }


    //--------------------------------------------------------------------
    // Purpose:
    //     A List of problems
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    public List<string> GetProblems(string year, string comp)
    {
      List<string> prob = new List<string>();
      foreach (string key in mTests[year][comp].Keys)
      {
        prob.Add(key);
      }

      return prob;
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

        List<string> years = GetYears();
        XmlNode yearsNode = document.CreateNode("element", YEARS_TAG, "");

        foreach (string year in years)
        {
          XmlNode newYearNode = document.CreateNode("element", YEAR_TAG, "");

          XmlAttribute newAttrib = document.CreateAttribute(NAME_TAG);
          newAttrib.InnerText = year;
          newYearNode.Attributes.Append(newAttrib);

          List<string> comps = GetComp(year);
          XmlNode compsNode = document.CreateNode("element", COMPS_TAG, "");
          foreach (string comp in comps)
          {
            XmlNode newCompNode = document.CreateNode("element", COMP_TAG, "");
            XmlAttribute newCompAttrib = document.CreateAttribute(NAME_TAG);
            newCompAttrib.InnerText = comp;
            newCompNode.Attributes.Append(newCompAttrib);

            List<string> probs = GetProblems(year, comp);
            foreach (string prob in probs)
            {
              XmlNode newProbNode = document.CreateNode("element", PROB_TAG, "");
              XmlAttribute newProbAttrib = document.CreateAttribute(NAME_TAG);
              newProbAttrib.InnerText = prob;
              newProbNode.Attributes.Append(newProbAttrib);
              newCompNode.AppendChild(newProbNode);
            }
            newYearNode.AppendChild(newCompNode);
          }
          yearsNode.AppendChild(newYearNode);
        }
        document.AppendChild(yearsNode);
        document.Save(FILE_NAME);

      }
      catch (Exception)
      {

      }
    }
  }
}

