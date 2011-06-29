using System;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace WowPacketParser.Storing.Database
{
    public static class SQLConnector
    {
        public static void StartSQL()
        {
            Console.WriteLine(ConnectionString);
            var name = string.Empty;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                
                var command = new MySqlCommand("SELECT name FROM creature_template WHERE entry=28650", conn);
                conn.Open();

                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        name = result[0].ToString();
                    }
                }

                conn.Close();
            }

            Console.WriteLine(name);
            Console.WriteLine("Done with MySQL stuff.");

        }

        private static string ConnectionString
        {
            get
            {
                return String.Format("Server={0};Port={1};Uid={2};Pwd={3};Database={4};character set=utf8",
                                     ConfigurationManager.AppSettings["Server"],
                                     ConfigurationManager.AppSettings["Port"],
                                     ConfigurationManager.AppSettings["Uid"],
                                     ConfigurationManager.AppSettings["Pwd"],
                                     ConfigurationManager.AppSettings["Database"]);
            }
        }
    }
}
