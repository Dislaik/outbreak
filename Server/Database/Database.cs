using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data;

namespace Outbreak
{
    public class Database
    {
        private const string HOST = "127.0.0.1";
        private const string USER = "root";
        private const string PASSWORD = "";
        private const string DATABASE = "outbreak";
        public static MySqlConnection Connection;

        public static void Initialize()
        {
            MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder
            {
                Server = HOST,
                UserID = USER,
                Password = PASSWORD,
                Database = DATABASE
            };

            Connection = new MySqlConnection(Builder.ToString());
        }
        public static MySqlDataReader ExecuteSelectQuery(string Sql)
        {
            MySqlCommand Command = new MySqlCommand(Sql, Connection);

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            MySqlDataReader Result = Command.ExecuteReader();

            return Result;
        }
        public static void ExecuteInsertQuery(string Sql)
        {
            MySqlCommand Command = new MySqlCommand(Sql, Connection);

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            Command.ExecuteNonQuery();

            Connection.Close();
        }
        public static void ExecuteUpdateQuery(string Sql)
        {
            ExecuteInsertQuery(Sql);
        }
    }
}
