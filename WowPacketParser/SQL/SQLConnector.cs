using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace WowPacketParser.SQL
{
    public static class SQLConnector
    {
        private static MySqlConnection _conn;

        public static void Connect()
        {
#if DEBUG
            Console.WriteLine("Connecting to MySQL server: " + ConnectionString);
#endif
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

        public static void Disconnect()
        {
            if (_conn != null)
                _conn.Close();
        }

        public static MySqlDataReader ExecuteQuery(string input)
        {
            var command = new MySqlCommand(input, _conn);
            return command.ExecuteReader();
        }

        private static string ConnectionString
        {
            get
            {
                return String.Format("Server={0};Port={1};Username={2};Password={3};Database={4};CharSet={5}",
                                    ConfigurationManager.AppSettings["Server"],
                                    ConfigurationManager.AppSettings["Port"],
                                    ConfigurationManager.AppSettings["Username"],
                                    ConfigurationManager.AppSettings["Password"],
                                    ConfigurationManager.AppSettings["Database"],
                                    ConfigurationManager.AppSettings["CharacterSet"]);
            }
        }
    }
}
