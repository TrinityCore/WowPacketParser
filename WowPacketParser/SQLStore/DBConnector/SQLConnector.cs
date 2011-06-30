using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace WowPacketParser.SQLStore.DBConnector
{
    public static class SQLConnector
    {
        private static MySqlConnection conn;

        public static void Connect()
        {
#if DEBUG
            Console.WriteLine("Connecting to MySQL server: " + ConnectionString);
#endif
            conn = new MySqlConnection(ConnectionString);
            try
            {
                conn.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Disconnect()
        {
            if (conn != null)
            {
#if DEBUG
                Console.WriteLine("Closing connection to MySQL server.");
#endif
                conn.Close();
            }
        }

        public static MySqlDataReader ExecuteQuery(string input)
        {
            MySqlCommand command = new MySqlCommand(input, conn);
            return command.ExecuteReader();
        }

        private static string ConnectionString
        {
            get
            {
                return String.Format("Server={0};Port={1};Username={2};Password={3};Database={4};character set=utf8",
                                    ConfigurationManager.AppSettings["Server"],
                                    ConfigurationManager.AppSettings["Port"],
                                    ConfigurationManager.AppSettings["Username"],
                                    ConfigurationManager.AppSettings["Password"],
                                    ConfigurationManager.AppSettings["Database"]);
            }
        }
    }
}
