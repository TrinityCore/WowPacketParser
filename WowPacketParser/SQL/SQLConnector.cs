using System;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using MySql.Data.MySqlClient;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    public static class SQLConnector
    {
        [ThreadStatic]
        private static MySqlConnection _conn;

        public static bool Enabled = Settings.DBEnabled;

        public static void Connect()
        {
            if (!Enabled)
            {
                Trace.WriteLine("DB queries are disabled. Will not connect.");
                return;
            }

            Trace.WriteLine("Connecting to MySQL server: " + ConnectionString.Replace("Password=" + Settings.Password + ";", string.Empty)); // Do not print password
            _conn = new MySqlConnection(ConnectionString);

            try
            {
                _conn.Open();
            }
            catch(Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public static bool Connected()
        {
            return _conn.State == ConnectionState.Open;
        }

        public static void Disconnect()
        {
            Enabled = false;
            if (_conn != null)
                _conn.Close();
        }

        [SuppressMessage("Microsoft.Security", "CA2100", Justification = "No user input.")]
        public static MySqlDataReader ExecuteQuery(string input)
        {
            try
            {
                using (var command = new MySqlCommand(input, _conn))
                    return command.ExecuteReader();
            }
            catch (Exception e)
            {
                // Something wrong happened, disabling everything MySQL/DB related
                Enabled = false;
                Trace.WriteLine(e.Message + " at query \"" + input + "\"");
                Disconnect();
            }

            return null;
        }

        private static string ConnectionString
        {
            get
            {
                var server = Settings.Server;
                var protocol = String.Empty;
                var portOrPipe = "Port";

                if (server == ".")
                {
                    server = "localhost";
                    portOrPipe = "Pipe";
                    protocol = "ConnectionProtocol=Pipe;";
                }

                return String.Format("Server={0};{1}={2};Username={3};Password={4};Database={5};CharSet={6};ConnectionTimeout=5;{7}",
                    server, portOrPipe, Settings.Port, Settings.Username, Settings.Password, Settings.Database,
                    Settings.CharacterSet, protocol);
            }
        }
    }
}
