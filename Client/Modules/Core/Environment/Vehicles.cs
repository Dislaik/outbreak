using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core.Environment
{
    class Vehicles : BaseScript
    {
        public Vehicles()
        {
            Tick += CheckVehicles;
            Tick += VehicleLights;
        }

        private async Task CheckVehicles()
        {
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

                if (IsPedInVehicle(PlayerPedId(), VehicleHandle, false))
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
    }
}