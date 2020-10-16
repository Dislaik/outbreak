using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Environment : BaseScript
    {
        private dynamic SafeZones { get; } = new[]
        {
            new {X = 450.5966f , Y = -998.9636f, Z = 30.6896f, Width = 80.0f, Height = 65.0f, Rotation = 0}
        };
        private dynamic RadiationZones { get; } = new[]
        {
            new { }
        };
        private dynamic Interiors { get; } = new[] {
            new {X = 438.6989f, Y = -983.5912f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
            new {X = 441.9033f, Y = -976.4308f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
            new {X = 449.6308f, Y = -975.7319f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
            new {X = 446.3341f, Y = -986.2154f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
            new {X = 451.6483f, Y = -981.1121f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 6.0f, Intensity = 0.4f},
            new {X = 456.1187f, Y = -990.7253f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
            new {X = 456.0132f, Y = -986.0571f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
            new {X = 446.6374f, Y = -992.9275f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
            new {X = 439.0417f, Y = -993.5868f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f}
        };

        public Environment()
        {
            Events();

            Tick += CheckVehicles;
            Tick += VehicleLights;
            Tick += SafeZone;
            Tick += InteriorLights;
            Tick += DisableServices;
        }

        private async Task CheckVehicles()
        {
            dynamic Players = GetActivePlayers();

            foreach (int Player in Players)
            {
                int PlayerPed = GetPlayerPed(Player);
                int VehicleHandle = -1;
                bool success;
                int Handle = FindFirstVehicle(ref VehicleHandle);

                do
                {
                    await Delay(10);

                    int VehicleClass = GetVehicleClass(VehicleHandle);

                    if (IsEntityInAir(VehicleHandle) && VehicleClass == 15 || IsEntityInAir(VehicleHandle) && VehicleClass == 16)
                    {
                        DeleteVehicle(ref VehicleHandle);
                    }

                    if (IsPedInVehicle(PlayerPed, VehicleHandle, false))
                    {

                        if (GetVehicleEngineHealth(VehicleHandle) > 900)
                        {
                            SetVehicleEngineHealth(VehicleHandle, 700f);
                        }
                    }
                    else
                    {
                        BringVehicleToHalt(VehicleHandle, 0.1f, 1, false);
                        SetVehicleEngineOn(VehicleHandle, false, true, false);

                        if (GetVehicleEngineHealth(VehicleHandle) > 900)
                        {

                            if (Utils.Math.Random(1, 100) <= Config.PercentageVehiclesUndriveable)
                            {
                                SetEntityRenderScorched(VehicleHandle, true);
                                SetVehicleIsConsideredByPlayer(VehicleHandle, false);
                            }

                            SetVehRadioStation(VehicleHandle, "OFF");

                            SetVehicleEngineHealth(VehicleHandle, 700f);
                        }
                    }

                    success = FindNextVehicle(Handle, ref VehicleHandle);
                } while (success);

                EndFindVehicle(Handle);
            }

            await Delay(100);
        }

        private async Task VehicleLights()
        {
            int PlayerVehicle = GetVehiclePedIsIn(PlayerPedId(), false);
            if (IsPedInAnyVehicle(PlayerPedId(), true))
            {
                bool LightOn = true;
                bool HighBeamsOn = true;
                GetVehicleLightsState(PlayerVehicle, ref LightOn, ref HighBeamsOn);
                bool Left = GetIsLeftVehicleHeadlightDamaged(PlayerVehicle);
                bool Right = GetIsRightVehicleHeadlightDamaged(PlayerVehicle);
                Vector3 VehicleDir = GetEntityForwardVector(PlayerVehicle);
                List<int> Bones = new List<int>();

                if ((LightOn != false || HighBeamsOn != false) && (Left == false || Right == false) && GetIsVehicleEngineRunning(PlayerVehicle))
                {
                    Bones.Add(GetEntityBoneIndexByName(PlayerVehicle, "headlight_l"));
                    Bones.Add(GetEntityBoneIndexByName(PlayerVehicle, "headlight_r"));
                    Bones.Add(GetEntityBoneIndexByName(PlayerVehicle, "extralight_1"));
                    Bones.Add(GetEntityBoneIndexByName(PlayerVehicle, "extralight_2"));
                    Bones.Add(GetEntityBoneIndexByName(PlayerVehicle, "extralight_3"));
                    Bones.Add(GetEntityBoneIndexByName(PlayerVehicle, "extralight_4"));

                    float Brightness = 0.7f;
                    float Radius = 25f;
                    float Distance = 25f;
                    if (HighBeamsOn)
                    {
                        Brightness = 1f;
                        Radius = 35f;
                        Distance = 30f;
                    }

                    foreach (int i in Bones)
                    {
                        Vector3 BonePos = GetWorldPositionOfEntityBone(PlayerVehicle, i);
                        DrawSpotLightWithShadow(BonePos.X + (VehicleDir.X * 0.1f), BonePos.Y + (VehicleDir.Y * 0.1f), BonePos.Z + (VehicleDir.Z * 0.1f), VehicleDir.X, VehicleDir.Y, VehicleDir.Z, 255, 255, 255, Distance, Brightness, 0.5f, Radius, 1.0f, i);
                        DrawSpotLight(BonePos.X + (VehicleDir.X * 0.1f), BonePos.Y + (VehicleDir.Y * 0.1f), BonePos.Z + (VehicleDir.Z * 0.1f), -VehicleDir.X, -VehicleDir.Y, -VehicleDir.Z, 255, 255, 255, 0.2f, Brightness*99.9f, 0.5f, 99.0f, 1.0f);
                    }
                }
            }

            await Task.FromResult(0);
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

                        if (v.X < (PedsCoords.X + (v.Width / 2f)) && v.X > (PedsCoords.X - (v.Width / 2f)) && v.Y < (PedsCoords.Y + (v.Height / 2f)) && v.Y > (PedsCoords.Y - (v.Height / 2f)))
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
        private async Task InteriorLights()
        {
            foreach (var v in Interiors)
            {
                DrawLightWithRange(v.X, v.Y, v.Z, v.R, v.G, v.B, v.Range, v.Intensity);
            }

            await Task.FromResult(0);
        }
        private async Task DisableServices()
        {
            for (int i = 0; i <= 14; i++)
            {
                EnableDispatchService(i, false);
            }

            await Delay(500);
        }
    }
}