using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Newtonsoft.Json;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System.Dynamic;
using System.Reflection.Emit;
using CitizenFX.Core.Native;

namespace Outbreak.Core
{
    public partial class Inventory : BaseScript
    {
        public static Dictionary<string, dynamic> Items = new Dictionary<string, dynamic>();
        public static IDictionary<string, dynamic> ItemsDroped = new Dictionary<string, dynamic>();
        public static int ItemsDropedID  = 0;

        public Inventory()
        {Events();

            Database.Initialize();
            GetItemDatabase();

            Command.Register("off", "Admin", new Action<Player, List<object>, string>((Source, Arguments, Raw) =>
            {
                Source.TriggerEvent("Inventory:OffNUI", false);

            }), "Off NUI");

        }
        private void GetItemDatabase()
        {
            MySqlDataReader Result = Database.ExecuteSelectQuery($"SELECT * FROM items");

            while (Result.Read())
            {
                dynamic Data = new ExpandoObject();
                Data.Label = Result["Label"].ToString();
                Data.Description = Result["Description"].ToString();
                Data.Weight = Result["Weight"].ToString();
                Data.Limit = Result["Limit"].ToString();

                Items.Add(Result["Name"].ToString(), Data);
            }

            Database.Connection.Close();
        }
        public static void UpdateInventory([FromSource] Player Source, string Items)
        {
            string Identifier = Source.Identifiers[Config.PlayerIdentifier];
            Database.ExecuteUpdateQuery($"UPDATE users SET Inventory = '{Items}' WHERE Identifier = '{Identifier}'");
            TriggerClientEvent("Inventory:Update");
        }
        public static string GetInventory([FromSource] Player Source)
        {
            string Identifier = Source.Identifiers[Config.PlayerIdentifier];
            MySqlDataReader Result = Database.ExecuteSelectQuery($"SELECT Inventory FROM users WHERE Identifier = '{Identifier}'");

            string Data = "";
            while (Result.Read())
            {
                Data = Result["Inventory"].ToString();

            }
            Database.Connection.Close();

            return Data;
        }

    }
}
