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
        const string YEAR_TAG        = "year";

        //----------------------------------------------------------------------------
        //  Class Attributes
        //----------------------------------------------------------------------------
        string mYear = "2022";
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
            if(null == mInstance)
            {
                mInstance = new ConfigFile();
            }
            return mInstance;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Get/Set the port
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
                newNode.InnerText = mYear;
                document.AppendChild(newNode);
                document.Save("Settings.xml");
            }
            catch(Exception)
            {

            }
        }
    }
}

