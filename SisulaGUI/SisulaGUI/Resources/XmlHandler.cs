using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SisulaGUI;
using System.IO;
using System.Diagnostics;

namespace SisulaGUI.Resources
{
    public static class XmlHandler
    {

        #region Members

        private static List<string> _XmlFiles;

        #endregion Members

        #region Properties
        public static List<string> XmlFiles
        {
            get { return _XmlFiles; }
            set { _XmlFiles = value; }
        }

        #endregion Properties

        #region Constructors
        #endregion Constructors

        #region Methods
        //Function that serializes a class to a xml-file 
        public static void Serialize<T>(T OutClass)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            //create output-directory in current folder if the directory does not exist
            if (!Directory.Exists(".\\Output\\"))
            {
                Directory.CreateDirectory(".\\Output");
            }
            using (TextWriter writer = new StreamWriter(".\\Output\\" + OutClass.GetType().ToString().Replace("SisulaGUI.Model.", "") + ".xml"))
            {
                serializer.Serialize(writer, (object)OutClass);
            }

        }
        //Function that deserializes xml-file to a class
        public static void Deserialize<T>(ref T InClass, string FilePath)
        {
            if (XmlFiles.Contains(FilePath))
            {
                XmlSerializer deserialize = new XmlSerializer(typeof(T));
                //Edit for dynamic readings from the output folder
                TextReader reader = new StreamReader(FilePath);
                object obj = deserialize.Deserialize(reader);
                InClass = (T)obj;
                reader.Close();
            }
            else
            {
                Debug.WriteLine("{0} not found in Output-folder", FilePath.Replace(".\\Output\\", ""));
            }
        }

        //Stores all files in the outputfolder
        public static void GetSisulaXmlFiles()
        {
            XmlFiles = Directory.GetFiles(".\\Output", "*.xml").ToList<string>();
        }

        //shows all files stored
        public static void ShowSisulaXmlFiles()
        {
            int i = 1;
            foreach (string file in XmlFiles)
            {
                Debug.Write("XmlFiles" + "[" + i + "]" + ":");
                Debug.WriteLine(file);
                i++;
            }
        }

        #endregion Methods



    }
}
