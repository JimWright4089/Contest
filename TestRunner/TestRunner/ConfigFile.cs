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
using System.Xml;

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
  ///     Hold all the data from the config ofthe app
  /// </summary>
  //
  //----------------------------------------------------------------------------
  public class ConfigFile
  {
    //----------------------------------------------------------------------------
    //  Class Constants 
    //----------------------------------------------------------------------------
    const string YEAR_TAG = "year";
    const string CONFIG_TAG = "config";
    const string COMP_TAG = "comp";
    const string PROB_TAG = "prob";

    //----------------------------------------------------------------------------
    //  Class Attributes
    //----------------------------------------------------------------------------
    string mYear = "2022";
    string mComp = "ICPC_PacNW";
    string mProb = "archeryaccuracy";
    static ConfigFile mInstance = null;

    //--------------------------------------------------------------------
    // Purpose:
    //     Constructor
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    private ConfigFile()
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
    public static ConfigFile GetInstance()
    {
      if (null == mInstance)
      {
        mInstance = new ConfigFile();
      }
      return mInstance;
    }

    //--------------------------------------------------------------------
    // Purpose:
    //     Get/Set the year
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    public string Year   // property
    {
      get { return mYear; }   // get method
      set { mYear = value; }  // set method
    }

    //--------------------------------------------------------------------
    // Purpose:
    //     Get/Set the comp
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    public string Comp   // property
    {
      get { return mComp; }   // get method
      set { mComp = value; }  // set method
    }

    //--------------------------------------------------------------------
    // Purpose:
    //     Get/Set the prob
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    public string Prob   // property
    {
      get { return mProb; }   // get method
      set { mProb = value; }  // set method
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
        XmlTextReader reader = new XmlTextReader("Settings.xml");

        while (reader.Read())
        {
          switch (reader.NodeType)
          {
            case XmlNodeType.Element: // The node is an element.
              if (YEAR_TAG == reader.Name)
              {
                reader.Read();
                mYear = reader.Value;
              }
              if (COMP_TAG == reader.Name)
              {
                reader.Read();
                mComp = reader.Value;
              }
              if (PROB_TAG == reader.Name)
              {
                reader.Read();
                mProb = reader.Value;
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

        XmlNode configNode = document.CreateNode("element", CONFIG_TAG, "");
        XmlNode yearNode = document.CreateNode("element", YEAR_TAG, "");
        yearNode.InnerText = mYear;
        configNode.AppendChild(yearNode);
        XmlNode compNode = document.CreateNode("element", COMP_TAG, "");
        compNode.InnerText = mComp;
        configNode.AppendChild(compNode);
        XmlNode probNode = document.CreateNode("element", PROB_TAG, "");
        probNode.InnerText = mProb;
        configNode.AppendChild(probNode);
        document.AppendChild(configNode);
        document.Save("Settings.xml");
      }
      catch (Exception)
      {

      }
    }
  }
}

