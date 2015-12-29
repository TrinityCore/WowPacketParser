using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace WowPacketParser.Misc
{
    public class Configuration
    {
        private readonly KeyValueConfigurationCollection _settingsCollection;

        public Configuration()
        {
            _settingsCollection = GetConfiguration();
        }

        public Configuration(KeyValueConfigurationCollection configCollection)
        {
            _settingsCollection = configCollection;
        }

        private static KeyValueConfigurationCollection GetConfiguration()
        {
            var args = Environment.GetCommandLineArgs();
            var opts = new Dictionary<string, string>();
            string configFile = null;
            KeyValueConfigurationCollection settings = null;
            for (int i = 1; i < args.Length - 1; ++i)
            {
                string opt = args[i];
                if (opt[0] != '/')
                    break;

                // analyze options
                string optname = opt.Substring(1);
                switch (optname)
                {
                    case "ConfigFile":
                        configFile = args[i + 1];
                        break;
                    default:
                        opts.Add(optname, args[i + 1]);
                        break;
                }
                ++i;
            }
            // load different config file
            if (configFile != null)
            {
                string configPath = Path.Combine(Environment.CurrentDirectory, configFile);

                try
                {
                    // Map the new configuration file.
                    var configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configPath };

                    // Get the mapped configuration file
                    var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                    settings = ((AppSettingsSection)config.GetSection("appSettings")).Settings;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not load config file {0}, reason: {1}", configPath, ex.Message);
                }
            }
            if (settings == null)
                settings = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings;

            // override config options with options from command line
            foreach (var pair in opts)
            {
                settings.Remove(pair.Key);
                settings.Add(pair.Key, pair.Value);
            }

            return settings;
        }

        public string GetString(string key, string defValue)
        {
            KeyValueConfigurationElement s = _settingsCollection[key];
            return s?.Value ?? defValue;
        }

        public string[] GetStringList(string key, string[] defValue)
        {
            KeyValueConfigurationElement s = _settingsCollection[key];

            if (s?.Value == null)
                return defValue;

            var arr = s.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < arr.Length; i++)
                arr[i] = arr[i].Trim();

            return arr;
        }

        public bool GetBoolean(string key, bool defValue)
        {
            KeyValueConfigurationElement s = _settingsCollection[key];
            if (s?.Value == null)
                return defValue;

            bool aux;
            if (bool.TryParse(s.Value, out aux))
                return aux;

            Console.WriteLine("Warning: \"{0}\" is not a valid boolean value for key \"{1}\"", s.Value, key);
            return defValue;
        }

        public int GetInt(string key, int defValue)
        {
            KeyValueConfigurationElement s = _settingsCollection[key];
            if (string.IsNullOrEmpty(s?.Value))
                return defValue;

            int aux;
            if (int.TryParse(s.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out aux))
                return aux;

            Console.WriteLine("Warning: \"{0}\" is not a valid integer value for key \"{1}\"", s.Value, key);
            return defValue;
        }

        public TEnum GetEnum<TEnum>(string key, TEnum defValue) where TEnum : struct
        {
            KeyValueConfigurationElement s = _settingsCollection[key];
            if (string.IsNullOrEmpty(s?.Value))
                return defValue;

            int value;
            if (!int.TryParse(s.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
            {
                Console.WriteLine("Warning: \"{0}\" is not a valid integer value for key \"{1}\"", s.Value, key);
                return defValue;
            }

            if (Enum.IsDefined(typeof(TEnum), value))
                return (TEnum)(object)value;

            TEnum enumValue;
            if (Enum.TryParse(value.ToString(), out enumValue))
                return enumValue;

            Console.WriteLine("Warning: \"{0}\" is not a valid enum value for key \"{1}\", enum \"{2}\"", s.Value, key, typeof(TEnum).Name);
            return defValue;
        }
    }
}
