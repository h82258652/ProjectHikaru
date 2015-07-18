using System.Configuration;

namespace HikaruDesktop.Datas
{
    public static class AppConfig
    {
        public static double Left
        {
            get
            {
                string left = ReadString("Left");
                double value;
                if (double.TryParse(left, out value))
                {
                    return value;
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                WriteString("Left", value.ToString());
            }
        }

        public static double Top
        {
            get
            {
                string top = ReadString("Top");
                double value;
                if (double.TryParse(top, out value))
                {
                    return value;
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                WriteString("Top", value.ToString());
            }
        }

        private static bool WriteString(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[key].Value = value;
                config.Save();
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string ReadString(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}