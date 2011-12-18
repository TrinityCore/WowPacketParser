using System;
using System.Data;
using MySql.Data.MySqlClient;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    public static class SQLConnector
    {
        private static MySqlConnection _conn;

        public static bool Enabled = Settings.GetBoolean("DBEnabled");

        public static void Connect()
        {
            if (!Enabled)
            {
                Console.WriteLine("DB queries are disabled. Will not connect.");
                return;
            }

            Console.WriteLine("Connecting to MySQL server: " + ConnectionString.Replace("Password=" + Settings.GetString("Password") + ";", string.Empty)); // Do not print password
            _conn = new MySqlConnection(ConnectionString);

            try
            {
                _conn.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool Connected()
        {
            return _conn.State == ConnectionState.Open;
        }

        public static void Disconnect()
        {
            if (_conn != null)
                _conn.Close();
        }

        public static MySqlDataReader ExecuteQuery(string input)
        {
            var command = new MySqlCommand(input, _conn);

            try
            {
                return command.ExecuteReader();
            }
            catch (Exception e)
            {

                // Something wrong happened, disabling everything MySQL/DB related
                Enabled = false;
                Console.WriteLine(e.Message + " at query \"" + input + "\"");
                Disconnect();
            }

            return null;
        }

        private static string ConnectionString
        {
            get
            {
                if (Settings.GetString("Server") == ".")
                    return String.Format("Server=localhost;Pipe={0};UserID={1};Password={2};Database={3};CharacterSet={4};ConnectionTimeout=5;ConnectionProtocol=Pipe;",
                                        Settings.GetString("Port"),
                                        Settings.GetString("Username"),
                                        Settings.GetString("Password"),
                                        Settings.GetString("Database"),
                                        Settings.GetString("CharacterSet"));

                return String.Format("Server={0};Port={1};Username={2};Password={3};Database={4};CharSet={5};ConnectionTimeout=5;",
                                    Settings.GetString("Server"),
                                    Settings.GetString("Port"),
                                    Settings.GetString("Username"),
                                    Settings.GetString("Password"),
                                    Settings.GetString("Database"),
                                    Settings.GetString("CharacterSet"));
            }
        }
    }
}
