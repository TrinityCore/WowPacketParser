using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    public static class SQLConnector
    {
        [ThreadStatic]
        public static MySqlConnection Conn;

        public static bool Enabled = Settings.DBEnabled;

        public static void Connect()
        {
            if (!Enabled)
            {
                Trace.WriteLine("DB queries are disabled. Will not connect.");
                return;
            }

            Trace.WriteLine("Connecting to MySQL server: " + ConnectionString.Replace("Password=" + Settings.Password + ";", String.Empty)); // Do not print password
            Conn = new MySqlConnection(ConnectionString);

            try
            {
                Conn.Open();
            }
            catch(Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public static bool Connected()
        {
            return Conn.State == ConnectionState.Open;
        }

        public static void Disconnect()
        {
            Enabled = false;
            Conn?.Close();
        }

        [SuppressMessage("Microsoft.Security", "CA2100", Justification = "No user input.")]
        public static MySqlCommand CreateCommand(string input)
        {
            try
            {
                using (var command = new MySqlCommand(input, Conn))
                {
                    command.CommandTimeout = 2147483; // max timeout val, 0 doesn't work
                    return command;
                }
            }
            catch (Exception e)
            {
                // Something wrong happened, disabling everything MySQL/DB related
                //Enabled = false;
                Trace.WriteLine(e.Message + " at query \"" + input + "\"");
                //Disconnect();
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

                return
                    $"Server={server};{portOrPipe}={Settings.Port};Username={Settings.Username};Password={Settings.Password};" +
                    $"Database={Settings.WPPDatabase};CharSet={Settings.CharacterSet};ConnectionTimeout=5;{protocol}";
            }
        }

        public static void ReadDB()
        {
            if (!Enabled)
                return;

            // Enable SSH Tunnel
            if (SSHTunnel.Enabled)
            {
                Trace.WriteLine("Enabling SSH Tunnel");
                SSHTunnel.Connect();
            }

            var startTime = DateTime.Now;
            Trace.WriteLine("Loading DB...");

            try
            {
                Connect();
                // Load names from world db first
                SQLDatabase.LoadSQL();
                // then fill gaps with object_names table
                SQLDatabase.GrabNameData();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.GetType());
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.StackTrace);
                Enabled = false; // Something failed, disabling everything SQL related
            }

            var endTime = DateTime.Now;
            var span = endTime.Subtract(startTime);
            Trace.WriteLine($"Finished loading DB in {span.ToFormattedString()}.");
            Trace.WriteLine(Environment.NewLine);
        }
    }
}
