using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace WowPacketParser.Misc
{
    public class Configuration
    {
          private IConfiguration configuration;


        public Configuration(IConfiguration configuration=null)
        {
            this.configuration = configuration?? new KeyValueConfiguration(GetConfiguration());
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
                if (!opt.StartsWith("--", StringComparison.CurrentCultureIgnoreCase))
                    break;

                // analyze options
                string optname = opt.Substring(2);
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
                    // Get the mapped configuration file
                    var config = ConfigurationManager.OpenExeConfiguration(configPath);


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
                Settings.Instance.Remove(pair.Key);
                Settings.Instance.Add(pair.Key, pair.Value);
            }

            return settings;
        }

        public string GetString(string key, string defValue)
        {
            return configuration[key] ?? defValue;
        }

        public string[] GetStringList(string key, string[] defValue)
        {

            if (configuration[key] == null)
                return defValue;

            var arr = configuration[key].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < arr.Length; i++)
                arr[i] = arr[i].Trim();

            return arr;
        }

        public bool GetBoolean(string key, bool defValue)
        {

            if (configuration[key] == null)
                return defValue;

            bool aux;
            if (bool.TryParse(configuration[key], out aux))
                return aux;

            Console.WriteLine("Warning: \"{0}\" is not a valid boolean value for key \"{1}\"", configuration[key], key);
            return defValue;
        }

        public int GetInt(string key, int defValue)
        {
            if (string.IsNullOrEmpty(configuration[key]))
                return defValue;

            int aux;
            if (int.TryParse(configuration[key], NumberStyles.Integer, CultureInfo.InvariantCulture, out aux))
                return aux;

            Console.WriteLine("Warning: \"{0}\" is not a valid integer value for key \"{1}\"", configuration[key], key);
            return defValue;
        }

        public TEnum GetEnum<TEnum>(string key, TEnum defValue) where TEnum : struct
        {
            if (string.IsNullOrEmpty(configuration[key]))
                return defValue;

            int value;
            if (!int.TryParse(configuration[key], NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
            {
                Console.WriteLine("Warning: \"{0}\" is not a valid integer value for key \"{1}\"", configuration[key], key);
                return defValue;
            }

            if (Enum.IsDefined(typeof(TEnum), value))
                return (TEnum)(object)value;

            TEnum enumValue;
            if (Enum.TryParse(value.ToString(), out enumValue))
                return enumValue;

            Console.WriteLine("Warning: \"{0}\" is not a valid enum value for key \"{1}\", enum \"{2}\"", configuration[key], key, typeof(TEnum).Name);
            return defValue;
        }

        internal void Remove(string key)
        {
            configuration[key] = null;
        }

        internal void Add(string key, string value)
        {
            configuration[key] = value;
        }
    }
}
