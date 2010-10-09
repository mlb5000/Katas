using System.Configuration;

namespace AutoBuild.Core
{
    public class ConfigurationSettings : IConfigurationSettings
    {
        private string nantExePath = ConfigurationManager.AppSettings["NAntExePath"];

        public string NAntExePath
        {
            get { return nantExePath; }
            set { nantExePath = value; }
        }
    }
}