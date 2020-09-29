using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core.Environment
{
    class Zones : BaseScript
    {
        private dynamic SafeZones { get; } = new[]
        {
            new {X = 449.2966f , Y = -984.9636f, Z = 30.6896f, Radius = 40.0f }
        };

        private dynamic RadiationZones { get; } = new[]
        {
            new { }
        };

        public Zones()
        {
            Tick += SafeZone;
            SafeZoneBlip();
        }

        private async Task SafeZone()
        {
            foreach (var v in SafeZones)
            {
                int PedHandle = -1;
                bool success;
                int Handle = FindFirstPed(ref PedHandle);

                do
                {
                    await Delay(10);

                    if (IsPedHuman(PedHandle) && !IsPedAPlayer(PedHandle) && !IsPedDeadOrDying(PedHandle, true))
                    {
                        Vector3 PedsCoords = GetEntityCoords(PedHandle, false);
                        float Distance = GetDistanceBetweenCoords(v.X, v.Y, v.Z, PedsCoords.X, PedsCoords.Y, PedsCoords.Z, true);
                        if (Distance <= v.Radius)
                        {
                            //string ZombieGroup = "ZOMBIE";
                            //Debug.WriteLine($"{GetHashKey(ZombieGroup)}");
                            DeleteEntity(ref PedHandle);
                        }
                    }

                    success = FindNextPed(Handle, ref PedHandle);
                } while (success);

                EndFindPed(Handle);
            }

            await Delay(500);
        }

        private void SafeZoneBlip()
        {
            foreach (var i in SafeZones)
            {
                int Blip = AddBlipForRadius(i.X, i.Y, i.Z, i.Radius);
                SetBlipHighDetail(Blip, true);
                SetBlipColour(Blip, 2);
                SetBlipAlpha(Blip, 128);
            }
        }
    }
}
