using System;
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
            return default(bool);
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
            return default(int);
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
            return default(float);
        }

        public static T GetEnum<T>(string key)
        {
            return GetEnum<T>(key, true);
        }

        public static T GetEnum<T>(string key, bool fromInt)
        {
            var s = ConfigurationManager.AppSettings[key];
            if (s != null)
            {
                if (fromInt)
                {
                    int value;
                    if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                        return (T)Enum.ToObject(typeof(T), value);
                }
                else
                {
                    // cant use Enum.TryParse as that is not supported in .NET 3.5
                    if (Enum.IsDefined(typeof(T), s))
                        return (T)Enum.Parse(typeof(T), s);
                }
            }
            return default(T);
        }
    }
}
