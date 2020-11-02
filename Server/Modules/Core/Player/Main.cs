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

namespace Outbreak.Core
{
    public partial class IPlayer : BaseScript
    {
        public IPlayer()
        { Events();
            Database.Initialize();

        }

        public static ExpandoObject GetDataDatabase([FromSource] Player Source)
        {
            string Identifier = Source.Identifiers[Config.PlayerIdentifier];
            MySqlDataReader Result = Database.ExecuteSelectQuery($"SELECT Name, `Date Of Birth`, Sex, `Group`, Faction FROM users WHERE Identifier = '{Identifier}'");

            dynamic Data = new ExpandoObject();
            while (Result.Read())
            {
                Data.Name = Result["Name"];
                Data.DOB = Result["Date Of Birth"];
                Data.Sex = Result["Sex"];
                Data.Group = Result["Group"];
                Data.Faction = Result["Faction"];
            }
            Database.Connection.Close();

            return Data;
        }
        private bool GetPlayerExistDB(string Identifier)
        {
            MySqlDataReader Result = Database.ExecuteSelectQuery($"SELECT ID FROM users WHERE Identifier = '{Identifier}'");

            bool GetPlayerExistDB = false;
            while (Result.Read())
            {
                GetPlayerExistDB = true;
            }
            Database.Connection.Close();

            return GetPlayerExistDB;
        }
        private string GetPlayerGenderDB(string Identifier)
        {
            MySqlDataReader Result = Database.ExecuteSelectQuery($"SELECT Sex FROM users WHERE Identifier = '{Identifier}'");

            string GetPlayerGenderDB = "";
            while (Result.Read())
            {
                GetPlayerGenderDB = Result["Sex"].ToString();

            }
            Database.Connection.Close();

            return GetPlayerGenderDB;
        }
        private string GetPlayerSkinDB(string Identifier)
        {
            MySqlDataReader Result = Database.ExecuteSelectQuery($"SELECT Skin FROM users WHERE Identifier = '{Identifier}'");

            string GetPlayerSkinDB = "";
            while (Result.Read())
            {
                GetPlayerSkinDB = Result["Skin"].ToString();

            }
            Database.Connection.Close();

            return GetPlayerSkinDB;
        }
        private string GetPlayerPositionDB(string Identifier)
        {
            MySqlDataReader Result = Database.ExecuteSelectQuery($"SELECT Position FROM users WHERE Identifier = '{Identifier}'");

            string GetPlayerPositionDB = "";
            while (Result.Read())
            {

                GetPlayerPositionDB = Result["Position"].ToString();

            }
            Database.Connection.Close();

            return GetPlayerPositionDB;
        }
        public static void AddInventoryItem([FromSource] Player Source, string Name, int Count)
        {
            if (!Enum.IsDefined(typeof(Weapon.Hash), Name) && Inventory.Items.ContainsKey(Name))
            {
                string PlayerInventory = Inventory.GetInventory(Source);
                var Dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(PlayerInventory);

                bool found = false;
                foreach (var Item in Dictionary)
                {
                    if (Item.Key == Name)
                    {
                        Dictionary[Name] += Count;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Dictionary.Add(Name, Count);
                }

                string NewInventory = JsonConvert.SerializeObject(Dictionary);
                Inventory.UpdateInventory(Source, NewInventory);
            }
            else
            {
                ChatMessage.Error(Source, $"Item [{Name}] does not exist!");
            }
            
        }

        public static void AddWeapon([FromSource] Player Source, string Name, int Ammo = 0, dynamic Component = null, int Tint = 1)// Configurar
        {
            string PlayerInventory = Inventory.GetInventory(Source);
            var Dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(PlayerInventory);

            if (Enum.TryParse(Name, out Weapon.Hash WeaponHash))
            {
                bool found = false;
                foreach (var Item in Dictionary)
                {
                    if (Item.Key == Name)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    dynamic Data = new ExpandoObject();
                    Data.Ammo = Ammo;
                    Data.Components = Component; //Not ready yet
                    if (Tint == null)//Not ready yet
                    { Data.Tint = 0; }//Not ready yet
                    else { Data.Tint = Tint; }//Not ready yet

                    Dictionary.Add(Name, Data);
                }
            }

            string NewInventory = JsonConvert.SerializeObject(Dictionary);
            Inventory.UpdateInventory(Source, NewInventory);
        }

        public static void RemoveWeapon([FromSource] Player Source, string Name) //Ready!
        {
            string PlayerInventory = Inventory.GetInventory(Source);
            var Dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(PlayerInventory);

            if (Enum.TryParse(Name, out Weapon.Hash WeaponHash))
            {
                foreach (var Item in Dictionary)
                {
                    if (Item.Key == Name)
                    {
                        Dictionary.Remove(Name);
                        break;
                    }
                }
            }

            string NewInventory = JsonConvert.SerializeObject(Dictionary);
            Inventory.UpdateInventory(Source, NewInventory);
        }

        public static void RemoveWeaponAmmo([FromSource] Player Source, string Name, int Ammo)
        {
            string PlayerInventory = Inventory.GetInventory(Source);
            var Dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(PlayerInventory);

            if (Enum.TryParse(Name, out Weapon.Hash WeaponHash))
            {
                foreach (dynamic Item in Dictionary.Keys.ToList())
                {
                    if (Item == Name)
                    {
                        Dictionary[Item].Ammo = Dictionary[Item].Ammo - Ammo;
                        break;
                    }
                }
            }

            string NewInventory = JsonConvert.SerializeObject(Dictionary);
            Inventory.UpdateInventory(Source, NewInventory);
        }

        public static void AddWeaponAmmo([FromSource] Player Source, string Name, int Ammo)
        {
            string PlayerInventory = Inventory.GetInventory(Source);
            var Dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(PlayerInventory);

            if (Enum.TryParse(Name, out Weapon.Hash WeaponHash))
            {
                foreach (dynamic Item in Dictionary.Keys.ToList())
                {
                    if (Item == Name)
                    {
                        Dictionary[Item].Ammo += Ammo;
                        break;
                    }
                }
            }

            string NewInventory = JsonConvert.SerializeObject(Dictionary);
            Inventory.UpdateInventory(Source, NewInventory);
        }

        public static bool CanCarryInventoryItem([FromSource] Player Source, string Name, int Amount)
        {
            string PlayerInventory = Inventory.GetInventory(Source);
            var Dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(PlayerInventory);

            bool found = false;
            foreach (var Item in Dictionary)
            {
                if (Item.Key == Name)
                {
                    found = true;
                    if (Convert.ToInt32(Inventory.Items[Name].Limit) >= (Convert.ToInt32(Item.Value) + Amount))
                    {
                        return true;
                        break;
                    }
                }
            }
            if (!found)
            {
                return true;
            }

            return false;
        }

        public static bool HasWeapon([FromSource] Player Source, string Name)
        {
            string PlayerInventory = Inventory.GetInventory(Source);
            var Dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(PlayerInventory);

            foreach (var Item in Dictionary)
            {
                if (Item.Key == Name)
                {
                    return true;
                    break;
                }
            }

            return false;
        }

        public static void RemoveInventoryItem([FromSource] Player Source, string Name, int Amount)
        {
            string PlayerInventory = Inventory.GetInventory(Source);
            var Dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(PlayerInventory);

            foreach (dynamic Item in Dictionary)
            {
                if (Item.Key == Name)
                {
                    Dictionary[Name] -= Amount;
                    break;
                }
            }
            if (Dictionary[Name] < 1)
            {
                Dictionary.Remove(Name);
            }

            string NewInventory = JsonConvert.SerializeObject(Dictionary);
            Inventory.UpdateInventory(Source, NewInventory);
        }

        public static string GetName([FromSource] Player Source)
        {
            string Identifier = Source.Identifiers[Config.PlayerIdentifier];
            MySqlDataReader Result = Database.ExecuteSelectQuery($"SELECT Name FROM users WHERE Identifier = '{Identifier}'");

            string Data = "";
            while (Result.Read())
            {
                Data = Result["Name"].ToString();
            }
            Database.Connection.Close();

            return Data;
        }

        public static void Notification([FromSource] Player Source, string Message)
        {
            Source.TriggerEvent("Player:Notification", Message);
        }

        public static float GetCurrentWeight([FromSource] Player Source)
        {
            float TotalWeight = 0f;

            string PlayerInventory = Inventory.GetInventory(Source);
            var Dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(PlayerInventory);

            foreach (var Item in Dictionary.Keys)
            {
                if (Inventory.Items.ContainsKey(Item))
                {
                    if (Enum.IsDefined(typeof(Weapon.Hash), Item))
                    {
                        TotalWeight += Convert.ToSingle(Inventory.Items[Item].Weight);
                    }
                    else
                    {
                        TotalWeight += Convert.ToSingle(Inventory.Items[Item].Weight) * Convert.ToInt32(Dictionary[Item]);
                    }
                }
            }

            return TotalWeight;
        }
    }
}
