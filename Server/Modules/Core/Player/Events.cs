using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using MySqlConnector;
using Newtonsoft.Json;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class IPlayer
    {
        private void Events()
        {
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers["Player:Spawned"] += new Action<Player>(OnPlayerRegister);
            EventHandlers["Player:Data"] += new Action<Player, NetworkCallbackDelegate>(CallbackGroup);
            EventHandlers["Identity:SetPlayerIdentity"] += new Action<Player, string, string, string, string, string, string>(SetPlayerRegistered);
            EventHandlers["Skin:SetPlayerSkin"] += new Action<Player, string>(SetPlayerSkin);
            EventHandlers["Player:GetPosition"] += new Action<Player, string>(SetPlayerPositionDB);
            EventHandlers["Player:InitPosition"] += new Action<Player>(OnPlayerPosition);
            EventHandlers["Player:GetInventory"] += new Action<Player, NetworkCallbackDelegate>(CallbackGetInventory);
            EventHandlers["Player:Test"] += new Action<Player, NetworkCallbackDelegate>(CallbackGetItemsDroped);
            EventHandlers["Player:ClearInventory"] += new Action<Player>(ClearInventory);
        }

        private async void OnPlayerConnecting([FromSource] Player source, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            await Delay(0);

            var Identifier = source.Identifiers[Config.PlayerIdentifier];
            var IP = source.Identifiers["ip"];

            if (!string.IsNullOrEmpty(Identifier))
            {
                if (!GetPlayerExistDB(Identifier))
                {
                    Database.ExecuteInsertQuery($"INSERT INTO users (Identifier, Nickname) VALUES ('{Identifier}', '{playerName}')");
                    Console.Info($"{playerName} [{Identifier}] - Joined for first time to server!");
                }
                else
                {
                    Console.Info($"{playerName} - User Authenticated {Identifier}");
                }
            }
            else
            {
                deferrals.done($"You dont have {Config.PlayerIdentifier} identifier used for this server.");
            }

            deferrals.done();
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
        private void CallbackGroup([FromSource] Player Source, NetworkCallbackDelegate CB)
        {
            CB.Invoke(GetDataDatabase(Source));
        }
        public void SetPlayerRegistered([FromSource] Player source, string name, string dob, string gender, string group, string faction, string money)
        {
            string Identifier = source.Identifiers[Config.PlayerIdentifier];
            Debug.WriteLine($"{name} {dob} {gender} {group}");
            Database.ExecuteUpdateQuery($"UPDATE users SET Name = '{name}', `Date Of Birth` = '{dob}', Sex = '{gender}', `Group` = '{group}', Faction = '{faction}', Money = '{money}' WHERE Identifier = '{Identifier}'");
        }
        public void SetPlayerSkin([FromSource] Player Source, string Skin)
        {
            string Identifier = Source.Identifiers[Config.PlayerIdentifier];
            Database.ExecuteUpdateQuery($"UPDATE users SET Skin = '{Skin}' WHERE Identifier = '{Identifier}'");

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
        private void CallbackGetInventory([FromSource] Player Source, NetworkCallbackDelegate CB)
        {
            string PlayerInventory = Inventory.GetInventory(Source);
            var InventoryItems = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(PlayerInventory);

            foreach (dynamic Item in InventoryItems.Keys.ToList())
            {
                if (Enum.IsDefined(typeof(Weapon.Hash), Item))
                {
                    InventoryItems[Item].Label = Inventory.Items[Item].Label;
                    InventoryItems[Item].Description = Inventory.Items[Item].Description;
                    InventoryItems[Item].Weight = Inventory.Items[Item].Weight;
                }
                else
                {
                    var OldValue = InventoryItems[Item];
                    InventoryItems[Item] = Inventory.Items[Item];
                    InventoryItems[Item].Amount = OldValue;
                }
            }

            var NUIInventory = JsonConvert.SerializeObject(InventoryItems);
            CB.Invoke(NUIInventory);
        }

        private void CallbackGetItemsDroped([FromSource] Player Source, NetworkCallbackDelegate CB)
        {
            if (Inventory.ItemsDroped.Count > 0)
            {
                CB.Invoke(Inventory.ItemsDroped, Inventory.ItemsDropedID);
            }
        }

        private void ClearInventory([FromSource] Player Source)
        {
            Inventory.UpdateInventory(Source,"{}");
        }
    }
}
