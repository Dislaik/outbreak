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
        public static MySqlConnection Connection;

        public static void Initialize()
        {
            MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder
            {
                Server = Config.Host,
                Port = Config.Port,
                UserID = Config.User,
                Password = Config.Password,
                Database = Config.Database
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
