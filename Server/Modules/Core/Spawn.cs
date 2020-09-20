using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using MySqlConnector;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core.Players
{
    public class Spawn : BaseScript
    {
        public Spawn()
        {
            Database.Initialize();

            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers["Outbreak.Core.Player:InitPlayerRegister"] += new Action<Player>(OnPlayerRegister);
            EventHandlers["Outbreak.Core.Player:SetPlayerGender"] += new Action<Player, string, string, string>(OnSetPlayerGender);
        }

        private async void OnPlayerConnecting([FromSource] Player source, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            deferrals.defer();

            await Delay(0);

            var Identifier = source.Identifiers[Config.PlayerIdentifier];
            var IP = source.Identifiers["ip"];

            if (!string.IsNullOrEmpty(Identifier))
            {
                if (!GetPlayerExist(Identifier))
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
        private bool GetPlayerExist(string Identifier)
        {
            MySqlDataReader Result = Database.ExecuteSelectQuery($"SELECT ID FROM users WHERE Identifier = '{Identifier}'");

            bool GetPlayerExist = false;
            while (Result.Read())
            {
                GetPlayerExist = true;
            }
            Database.Connection.Close();

            return GetPlayerExist;
        }
        private string GetPlayerGender(string Identifier)
        {
            MySqlDataReader Result = Database.ExecuteSelectQuery($"SELECT Sex FROM users WHERE Identifier = '{Identifier}'");

            string GetPlayerGender = "";
            while (Result.Read())
            {
                GetPlayerGender = Result["Sex"].ToString();

            }
            Database.Connection.Close();

            return GetPlayerGender;
        }
        public void OnPlayerRegister([FromSource] Player source)
        {
            string Identifier = source.Identifiers[Config.PlayerIdentifier];

            if (string.IsNullOrEmpty(GetPlayerGender(Identifier)))
            {
                TriggerClientEvent(source, "Outbreak.Core.Player:PlayerRegister");
            }
            else { TriggerClientEvent(source, "Outbreak.Core.Player:PlayerAlreadyRegistered", GetPlayerGender(Identifier)); }

        }
        public void OnSetPlayerGender([FromSource] Player source, string name, string age, string gender)
        {
            Convert.ToInt32(age);
            string Identifier = source.Identifiers[Config.PlayerIdentifier];
            Database.ExecuteUpdateQuery($"UPDATE users SET Name = '{name}', Age = '{age}', Sex = '{gender}' WHERE Identifier = '{Identifier}'");
        }
    }
}
