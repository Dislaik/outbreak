using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Player : BaseScript
    {
        public static bool Loaded { get; set; } = false;
        public static string Name { get; set; }
        public static string DOB { get; set; }
        public static string Sex { get; set; }
        public static string Group { get; set; }
        public static string Faction { get; set; }
        public static bool Dead { get; set; } = false;
        private int OnPressed { get; set; } = 0;
        //List<int> Players = new List<int>();

        public Player()
        { Events();


            Tick += GetPlayerCoords;
            Tick += DeathDetection;
            Tick += DeathMessage;
            Tick += WeaponWheelHidden;
        }

        public static void GetData()
        {
            TriggerServerEvent("Player:Data", new Action<dynamic>((Data) =>
            {
                Name = Data.Name;
                DOB = Data.DOB;
                Sex = Data.Sex;
                Group = Data.Group;
                Faction = Data.Faction;

                Console.Info($"Player data loaded!");
            }));
        }
        public void InitPlayer()
        {
            TriggerServerEvent("Player:Spawned");
            TriggerServerEvent("Player:InitPosition");

            TriggerServerEvent("Player:Test", new Action<IDictionary<string, dynamic>, int>((Data, ItemsID) =>
            {
                Inventory.ItemsDroped = Data;
                Inventory.ItemsDropedID = ItemsID;
                Inventory.CreateItemsDropedServer();
            }));

        }

        public async Task GetPlayerCoords()
        {

            if (Loaded)
            {
                Vector3 PlayerCoords = GetEntityCoords(PlayerPedId(), true);
                Dictionary<string, dynamic> PlayerCoordsDictionary = new Dictionary<string, dynamic>
                {
                    { "X", (PlayerCoords.X).ToString() },
                    { "Y", (PlayerCoords.Y).ToString() },
                    { "Z", (PlayerCoords.Z).ToString() }
                };

                string SendPlayerCoords = Utils.String.DictionaryToString(PlayerCoordsDictionary);
                TriggerServerEvent("Player:GetPosition", SendPlayerCoords);
            }

            await Delay(60000);
        }

        public void SetPlayerPosition(float X, float Y, float Z)
        {
            SetEntityCoords(PlayerPedId(), X, Y, Z, false, false, false, true);
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

        private async Task DeathDetection()
        {
            if (NetworkIsPlayerActive(PlayerId()))
            {
                if (IsPedFatallyInjured(PlayerPedId()) && !Dead)
                {
                    Dead = true;
                    int Killer = GetPedSourceOfDeath(PlayerPedId());
                    int Weapon = GetPedCauseOfDeath(PlayerPedId());
                    int KillerID = NetworkGetPlayerIndexFromPed(Killer);

                    if (Killer != PlayerPedId() && NetworkIsPlayerActive(KillerID))
                    {
                        PlayerDeathByPlayer(KillerID, Weapon);
                    }
                    else
                    {
                        PlayerDeath(Weapon);
                    }
                }
                else if (!IsPedFatallyInjured(PlayerPedId()))
                {
                    Dead = false;
                }
            }

            if (Dead)
            {
                if (Game.IsControlPressed(0, Control.Pickup))
                {
                    OnPressed += 1;
                    if (OnPressed > 30)
                    {
                        TriggerServerEvent("Player:ClearInventory");
                        DoScreenFadeOut(1000);
                        await Delay(2000);
                        NetworkResurrectLocalPlayer(Config.PlayerDeathRespawn.X, Config.PlayerDeathRespawn.Y, Config.PlayerDeathRespawn.Z, Config.PlayerDeathRespawn.Heading, true, false);
                        ClearPedBloodDamage(PlayerPedId());
                        StopScreenEffect("DeathFailOut");
                        DoScreenFadeIn(1000);
                        PlaySoundFrontend(-1, "Hit", "RESPAWN_ONLINE_SOUNDSET", true);
                    }
                }
                else { OnPressed = 0; }
            }

            await Delay(0);
        }

        private void PlayerDeath(int Weapon)
        {
            Vector3 PlayerCoords = GetEntityCoords(PlayerPedId(), false);

            dynamic Data = new ExpandoObject();
            Data.PlayerCoords = PlayerCoords;
            Data.Weaponn = Weapon;

            TriggerEvent("Player:DeathDetection", Data);
            TriggerServerEvent("Player:DeathDetection", Data);
        }

        private void PlayerDeathByPlayer(int Killer, int Weapon)
        {
            Vector3 PlayerCoords = GetEntityCoords(PlayerPedId(), false);
            Vector3 KillerCoords = GetEntityCoords(GetPlayerPed(Killer), false);

            dynamic Data = new ExpandoObject();
            Data.PlayerCoords = PlayerCoords;
            Data.KillerCoords = KillerCoords;
            Data.Killer = GetPlayerServerId(Killer);
            Data.Weaponn = Weapon;
        }

        private void OnPlayerDeath(dynamic Data)
        {
            AnimpostfxPlay("DeathFailOut", 0, true);
            PlaySoundFrontend(-1, "Bed", "WastedSounds", true);
        }

        private async Task DeathMessage()
        {
            if (Dead)
            {
                Utils.Game.DrawText2D("You are dead\nHold down E for respawn", 0.5f, 0.5f, 0.5f, 2, 0, 255, 255, 255, 255);
            }

            await Task.FromResult(0);
        }

        private async Task WeaponWheelHidden()
        {
            //HideHudAndRadarThisFrame();
            BlockWeaponWheelThisFrame();
            DisableControlAction(0, 37,true);

            await Task.FromResult(0);
        }

        /*public static int GetPlayers()
        {
            for (int i = 0; i <= Config.PlayerServerSlots; i++)
            {
                if (NetworkIsPlayerActive(i))
                {
                    GetActivePlayers();
                }
            }

            return Players;
        }*/
    }
}
