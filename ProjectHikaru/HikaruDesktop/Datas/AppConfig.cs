using System.Configuration;
using System.Linq;

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

        public static int Mode
        {
            get
            {
                string mode = ReadString("Mode");
                int value;
                if (int.TryParse(mode, out value))
                {
                    return value;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                WriteString("Mode", value.ToString());
            }
        }

        public static bool Topmost
        {
            get
            {
                string topmost = ReadString("Topmost");
                bool value;
                if (bool.TryParse(topmost,out value))
                {
                    return value;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                WriteString("Topmost", value.ToString());
            }
        }

        private static bool WriteString(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings.AllKeys.Contains(key))
                {
                    config.AppSettings.Settings[key].Value = value;   
                }
                else
                {
                    config.AppSettings.Settings.Add(key, value);
                }
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