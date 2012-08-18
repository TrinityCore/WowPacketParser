using System;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using MySql.Data.MySqlClient;
using PacketParser.Misc;

namespace PacketParser.SQL
{
    public static class SQLConnector
    {
        [ThreadStatic]
        public static MySqlConnection Conn;

        private static bool connectionClosed = false;
        public static bool Enabled
        {
            get
            {
                return ParserSettings.MySQL.Enabled && !connectionClosed;
            }
            set
            {
                connectionClosed = !value;
            }
        }

        private static void Connect()
        {
            connectionClosed = false;
            if (!Enabled)
            {
                Trace.WriteLine("DB queries are disabled. Will not connect.");
                return;
            }

            Trace.WriteLine("Connecting to MySQL server: " + ConnectionString.Replace("Password=" + ParserSettings.MySQL.Password + ";", String.Empty)); // Do not print password
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
            connectionClosed = true;
            if (Conn != null)
                Conn.Close();
        }

        [SuppressMessage("Microsoft.Security", "CA2100", Justification = "No user input.")]
        public static MySqlDataReader ExecuteQuery(string input)
        {
            try
            {
                using (var command = new MySqlCommand(input, Conn))
                    return command.ExecuteReader();
            }
            catch (Exception e)
            {
                // Something wrong happened, disabling everything MySQL/DB related
                Trace.WriteLine(e.Message + " at query \"" + input + "\"");
                Disconnect();
            }

            return null;
        }

        private static string ConnectionString
        {
            get
            {
                var server = ParserSettings.MySQL.Server;
                var protocol = String.Empty;
                var portOrPipe = "Port";

                if (server == ".")
                {
                    server = "localhost";
                    portOrPipe = "Pipe";
                    protocol = "ConnectionProtocol=Pipe;";
                }

                return String.Format("Server={0};{1}={2};Username={3};Password={4};Database={5};CharSet={6};ConnectionTimeout=5;{7}",
                    server, portOrPipe, ParserSettings.MySQL.Port, ParserSettings.MySQL.Username, ParserSettings.MySQL.Password, ParserSettings.MySQL.PacketParserDB,
                    ParserSettings.MySQL.CharacterSet, protocol);
            }
        }

        public static void ReadDB()
        {
            if (!Enabled) return;

            // Enable SSH Tunnel
            if (ParserSettings.SSHTunnel.Enabled)
            {
                Trace.WriteLine("Enabling SSH Tunnel");
                SSHTunnel.Connect();
            }

            var startTime = DateTime.Now;
            Trace.WriteLine("Loading DB...");

            try
            {
                Connect();
                SQLDatabase.GrabNameData();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.GetType());
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.StackTrace);
                connectionClosed = true; // Something failed, disabling everything SQL related
            }

            var endTime = DateTime.Now;
            var span = endTime.Subtract(startTime);
            Trace.WriteLine(String.Format("Finished loading DB in {0}.", span.ToFormattedString()));
            Trace.WriteLine(Environment.NewLine);
        }
    }
}
