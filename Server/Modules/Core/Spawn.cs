using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using MySqlConnector;
using static CitizenFX.Core.Native.API;
using Newtonsoft.Json;

namespace Outbreak.Core.Players
{
    public class Spawn : BaseScript
    {
        public Spawn()
        {
            Database.Initialize();

            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers["Outbreak.Core.Player:InitPlayerRegister"] += new Action<Player>(OnPlayerRegister);
            EventHandlers["Outbreak.Core.Player:SetPlayerRegistered"] += new Action<Player, string, int, string, string>(SetPlayerRegistered);
            EventHandlers["Outbreak.Core.Player:GetPlayerPosition"] += new Action<Player, string>(GetPlayerPosition);
            EventHandlers["Outbreak.Core.Player:InitPlayerPosition"] += new Action<Player>(OnPlayerPosition);
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
                    Database.ExecuteInsertQuery($"INSERT INTO users (Identifier, IP, Nickname) VALUES ('{Identifier}', '{IP}', '{playerName}')");
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
        public void OnPlayerRegister([FromSource] Player source)
        {
            string Identifier = source.Identifiers[Config.PlayerIdentifier];

            if (string.IsNullOrEmpty(GetPlayerGenderDB(Identifier)))
            {
                TriggerClientEvent(source, "Outbreak.Core.Player:PlayerRegister");
            }
            else 
            {
                string json = GetPlayerSkinDB(Identifier);
                var PlayerSkin = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
                int Skin = PlayerSkin["Skin"];
                int Face = PlayerSkin["Face"];
                int Hair = PlayerSkin["Hair"];
                int HairColor = PlayerSkin["HairColor"];
                int Eyes = PlayerSkin["Eyes"];
                int Eyebrows = PlayerSkin["Eyebrows"];
                int Beard = PlayerSkin["Beard"];

                TriggerClientEvent(source, "Outbreak.Core.Player:PlayerAlreadyRegistered", GetPlayerGenderDB(Identifier), Skin, Face, Hair, HairColor, Eyes, Eyebrows, Beard);
            }

        }
        public void SetPlayerRegistered([FromSource] Player source, string name, int age, string gender, string skin)
        {
            string Identifier = source.Identifiers[Config.PlayerIdentifier];
            Database.ExecuteUpdateQuery($"UPDATE users SET Name = '{name}', Age = '{age}', Sex = '{gender}', Skin = '{skin}' WHERE Identifier = '{Identifier}'");
        }

        public void GetPlayerPosition([FromSource] Player source, string position)
        {
            string Identifier = source.Identifiers[Config.PlayerIdentifier];
            Database.ExecuteUpdateQuery($"UPDATE users SET Position = '{position}' WHERE Identifier = '{Identifier}'");
        }

        public void OnPlayerPosition([FromSource] Player source)
        {
            string Identifier = source.Identifiers[Config.PlayerIdentifier];

            if (!string.IsNullOrEmpty(GetPlayerPositionDB(Identifier)))
            {
                string json = GetPlayerPositionDB(Identifier);
                var PlayerPosition = JsonConvert.DeserializeObject<Dictionary<string, float>>(json);
                float X = PlayerPosition["X"];
                float Y = PlayerPosition["Y"];
                float Z = PlayerPosition["Z"]-1f;

                TriggerClientEvent(source, "Outbreak.Core.Player:SetPlayerPosition", X, Y, Z);
            }
        }
    }
}
