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
    public class Death : BaseScript
    {
        private bool PlayerDead = false;
        private int OnPressed = 0;

        public Death()
        {
            EventHandlers["Outbreak.Core.Player:DeathDetection"] += new Action<dynamic>(OnPlayerDeath);

            Tick += DeathDetection;
            Tick += DeathMessage;
        }

        private async Task DeathDetection()
        {
            if (NetworkIsPlayerActive(PlayerId()))
            {
                if (IsPedFatallyInjured(PlayerPedId()) && !PlayerDead)
                {
                    PlayerDead = true;
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
                    PlayerDead = false;
                }
            }

            if (PlayerDead)
            {
                if (Game.IsControlPressed(0, Control.Pickup))
                {
                    OnPressed += 1;
                    if (OnPressed > 30)
                    {
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

            TriggerEvent("Outbreak.Core.Player:DeathDetection", Data);
            TriggerServerEvent("Outbreak.Core.Player:DeathDetection", Data);
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
            if (PlayerDead)
            {
                Utils.Game.DrawText2D("You are dead\nHold down E for respawn", 0.5f, 0.5f, 0.5f, 2, 0, 255, 255, 255, 255);
            }

            await Task.FromResult(0);
        }
    }
}
