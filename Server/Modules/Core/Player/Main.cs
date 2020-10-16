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
    public partial class Player_ : BaseScript
    {

        public Player_()
        {
            Events();
        }

        private void CallbackGroup([FromSource] Player Source, NetworkCallbackDelegate CB)
        {
            CB.Invoke(GetDataDatabase(Source));
        }

        private ExpandoObject GetDataDatabase([FromSource] Player Source)
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

        private async void OnPlayerConnecting([FromSource] Player source, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            deferrals.defer();

            await Delay(0);

            var Identifier = source.Identifiers[Config.PlayerIdentifier];
            var IP = source.Identifiers["ip"];

            if (!string.IsNullOrEmpty(Identifier))
            {
                if (!GetPlayerExistDB(Identifier))
                {
                    Database.ExecuteInsertQuery($"INSERT INTO users (Identifier, Nickname) VALUES ('{Identifier}', '{playerName}')");
                    Debug.WriteLine($"^1[Outbreak]^5[INFO]^7 {playerName} [{Identifier}] - Joined for first time to server!");
                }
                else
                {
                    Debug.WriteLine($"^1[Outbreak]^5[INFO]^7 {playerName} - User Authenticated {Identifier}");
                }
            }
            else
            {
                deferrals.done($"You dont have {Config.PlayerIdentifier} identifier used for this server.");
            }

            deferrals.done();
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
        public void OnPlayerRegister([FromSource] Player Source)
        {
            string Identifier = Source.Identifiers[Config.PlayerIdentifier];

            if (string.IsNullOrEmpty(GetPlayerGenderDB(Identifier)))
            {
                TriggerClientEvent(Source, "Identity:Register");
            }
            else
            {
                string JSON = GetPlayerSkinDB(Identifier);
                var PlayerSkin = JsonConvert.DeserializeObject<IDictionary<string, object>>(JSON);

                TriggerClientEvent(Source, "Skin:LoadPlayerSkin", GetPlayerGenderDB(Identifier), PlayerSkin);
            }

        }
        public void SetPlayerRegistered([FromSource] Player source, string name, string dob, string gender, string group, string faction)
        {
            string Identifier = source.Identifiers[Config.PlayerIdentifier];
            Debug.WriteLine($"{name} {dob} {gender} {group}");
            Database.ExecuteUpdateQuery($"UPDATE users SET Name = '{name}', `Date Of Birth` = '{dob}', Sex = '{gender}', `Group` = '{group}', Faction = '{faction}' WHERE Identifier = '{Identifier}'");
        }

        public void SetPlayerPositionDB([FromSource] Player Source, string position)
        {
            string Identifier = Source.Identifiers[Config.PlayerIdentifier];
            Database.ExecuteUpdateQuery($"UPDATE users SET Position = '{position}' WHERE Identifier = '{Identifier}'");
        }

        public void OnPlayerPosition([FromSource] Player Source)
        {
            string Identifier = Source.Identifiers[Config.PlayerIdentifier];

            if (!string.IsNullOrEmpty(GetPlayerPositionDB(Identifier)))
            {
                string json = GetPlayerPositionDB(Identifier);
                var PlayerPosition = JsonConvert.DeserializeObject<Dictionary<string, float>>(json);
                float X = PlayerPosition["X"];
                float Y = PlayerPosition["Y"];
                float Z = PlayerPosition["Z"];

                TriggerClientEvent(Source, "Player:SetPosition", X, Y, Z);
            }
        }

        public void SetPlayerSkin([FromSource] Player Source, string Skin)
        {
            string Identifier = Source.Identifiers[Config.PlayerIdentifier];
            Database.ExecuteUpdateQuery($"UPDATE users SET Skin = '{Skin}' WHERE Identifier = '{Identifier}'");

        }
    }
}
