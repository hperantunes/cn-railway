using System;
using System.Configuration;

namespace CNRailway.Util
{
    public class Configuration : IConfiguration
    {
        public string DefaultDirectory
        {
            get { return ConfigurationManager.AppSettings["DefaultDirectoryPath"]; }
        }

        public string DefaultFileName
        {
            get { return ConfigurationManager.AppSettings["DefaultFileName"]; }
        }

        public char EmptySlotCharacter
        {
            get { return Convert.ToChar(ConfigurationManager.AppSettings["EmptySlotCharacter"]); }
        }

        public int SortingLineMaximumCapacity
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["SortingLineMaximumCapacity"]); }
        }

        public int YardLocomotiveMaximumCapacity
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["YardLocomotiveMaximumCapacity"]); }
        }

        public bool UseDefaultFileLocation
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultFileLocation"]); }
        }
    }
}
