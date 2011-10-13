using System.Configuration;
using System.Globalization;

namespace WowPacketParser.Misc
{
    public static class Settings
    {
        public static string GetString(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static bool GetBoolean(string key)
        {
            var s = ConfigurationManager.AppSettings[key];
            if (s != null)
            {
                bool value;
                if (bool.TryParse(s, out value))
                    return value;
            }
            return false;
        }

        public static int GetInt32(string key)
        {
            var s = ConfigurationManager.AppSettings[key];
            if (s != null)
            {
                int value;
                if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                    return value;
            }
            return 0;
        }

        public static float GetFloat(string key)
        {
            var s = ConfigurationManager.AppSettings[key];
            if (s != null)
            {
                float value;
                if (float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                    return value;
            }
            return 0;
        }
    }
}
