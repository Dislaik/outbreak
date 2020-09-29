using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core.Player
{
    class Spawn : BaseScript
    {
        private Dictionary<string, string> PlayerCoordsDictionary = new Dictionary<string, string>();

        public Spawn()
        {
            Exports["spawnmanager"].spawnPlayer(SpawnPosition());
            Skin.DefaultCharacterComponents("Male");

            EventHandlers["playerSpawned"] += new Action(InitPlayer);
            EventHandlers["Outbreak.Core.Player:SetPlayerPosition"] += new Action<float, float, float>(SetPlayerPosition);
            Tick += GetPlayerCoords;
        }
        public dynamic SpawnPosition()
        {
            dynamic obj = new ExpandoObject();
            obj.x = Config.PlayerSpawn.X;
            obj.y = Config.PlayerSpawn.Y;
            obj.z = Config.PlayerSpawn.Z;
            obj.heading = Config.PlayerSpawn.Heading;
            obj.model = "mp_m_freemode_01";
            return obj;
        }
        public void SetPlayerPosition(float X, float Y, float Z)
        {
            SetEntityCoords(PlayerPedId(), X, Y, Z, false, false, false, true);
        }
        public async Task GetPlayerCoords()
        {
            if (Skin.PlayerLoaded)
            {
                Vector3 PlayerCoords = GetEntityCoords(PlayerPedId(), false);
                PlayerCoordsDictionary.Add("X", (PlayerCoords.X).ToString());
                PlayerCoordsDictionary.Add("Y", (PlayerCoords.Y).ToString());
                PlayerCoordsDictionary.Add("Z", (PlayerCoords.Z).ToString());

                string SendPlayerCoords = Utils.String.DictionaryToString(PlayerCoordsDictionary);
                TriggerServerEvent("Outbreak.Core.Player:GetPlayerPosition", SendPlayerCoords);
                PlayerCoordsDictionary.Clear();
            }
            
            await Delay(60000);
        }
        public void InitPlayer()
        {
            TriggerServerEvent("Outbreak.Core.Player:InitPlayerRegister");
            TriggerServerEvent("Outbreak.Core.Player:InitPlayerPosition");
        }
    }
}
