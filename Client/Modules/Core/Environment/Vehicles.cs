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
            SetArtificialLightsState(true);
            //DisableVehicleDistantlights(true);
            //DisplayDistantVehicles(false);
            SetScenarioGroupEnabled("LSA_Planes", false); // Maybe Works?
            StartAudioScene("CHARACTER_CHANGE_IN_SKY_SCENE");
            SetDistantCarsEnabled(true);
            SetMaxWantedLevel(0);
            DisableServices();

            Tick += CheckVehicles;
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

                if (IsEntityInAir(VehicleHandle) || VehicleClass == 15 || VehicleClass == 16)
                {
                    DeleteVehicle(ref VehicleHandle);
                }

                if (IsPedInVehicle(PlayerPedId(), VehicleHandle, false) /*&& GetIsVehicleEngineRunning(VehicleHandle)*/)
                {

                    if (GetVehicleEngineHealth(VehicleHandle) > 900)
                    {
                        
                        //SetVehicleEngineOn(VehicleHandle, false, true, false);

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

        private void DisableServices()
        {
            for (int i = 0; i <= 14; i++)
            {
                EnableDispatchService(i, false);
            }
        }

        /*Citizen.CreateThread(function()-- car lights
	        while true do Wait(0)

                local players = GetActivePlayers()

                local h = 0
		        for _,p in pairs(players) do
			        local ped = GetPlayerPed(p)

                    local veh = GetVehiclePedIsIn(ped)
			        if IsPedInAnyVehicle(ped) and veh then
                        local _,lightson,headbeam=GetVehicleLightsState(veh)

                        local left = GetIsLeftVehicleHeadlightDamaged(veh)

                        local right = GetIsRightVehicleHeadlightDamaged(veh)
                        --WriteHint(145,{ "lights: ~a~ beam: ~a~, left: ~a~, right: ~a~",tostring(lightson),tostring(headbeam),tostring(left),tostring(right)})
				        local myvehdir = GetEntityForwardVector(veh)
                        --WriteHint(145,{
                    "Veh dir: ~a~ ~a~ ~a~ ~a~",tostring(myvehdir.x), tostring(myvehdir.y), tostring(myvehdir.z),tostring(#myvehdir)})
				        local bones ={ }
                    --local alwayslight ={ }
                    if (lightson~= 0 or headbeam~= 0) and(left == false or right == false) and GetIsVehicleEngineRunning(veh) then

                            bones.headlight_l = GetEntityBoneIndexByName(veh, "headlight_l")

                            bones.headlight_r = GetEntityBoneIndexByName(veh, "headlight_r")

                            bones.extralight_1 = GetEntityBoneIndexByName(veh, "extralight_1")

                            bones.extralight_2 = GetEntityBoneIndexByName(veh, "extralight_2")

                            bones.extralight_3 = GetEntityBoneIndexByName(veh, "extralight_3")

                            bones.extralight_4 = GetEntityBoneIndexByName(veh, "extralight_4")

                            -- alwayslight.taillight_l = GetEntityBoneIndexByName(veh, "taillight_l")
                            -- alwayslight.taillight_r = GetEntityBoneIndexByName(veh, "taillight_r")



                            local brightness = 0.7

                            local radius = 25.0

                            local distance = 25.0

                            if headbeam == 1 then
                                  distance = 30.0

                                brightness = 1.0

                                radius = 35.0

                            end


                            for k, v in pairs(bones) do
                            if v~= -1 then
                                     --WriteHint(145,{ "boneindex: ~1~",v})
							        local bonepos = GetWorldPositionOfEntityBone(veh, v)

                                    DrawSpotLightWithShadow(bonepos.x + (myvehdir.x * 0.1), bonepos.y + (myvehdir.y * 0.1), bonepos.z + (myvehdir.z * 0.1),
                                    myvehdir.x, myvehdir.y, myvehdir.z,
                                    255, 255, 255,
                                    distance, brightness,
                                    0.5, radius, 1.0, h)

                                    h = h + 1

                                    DrawSpotLight(bonepos.x + (myvehdir.x * 0.1), bonepos.y + (myvehdir.y * 0.1), bonepos.z + (myvehdir.z * 0.1),
                                    -myvehdir.x, -myvehdir.y, -myvehdir.z,
                                    255, 255, 255,
                                    0.2, brightness * 99.9,
                                    0.5, 99.0, 99.0, 1.0)

                                end
                            end

                            -- for k, v in pairs(alwayslight) do
                            -- if v~= -1 then
                                 -- local bonepos = GetWorldPositionOfEntityBone(veh, v)
                                 -- DrawSpotLight(bonepos.x - (myvehdir.x * 0.2), bonepos.y - (myvehdir.y * 0.2), bonepos.z - (myvehdir.z * 0.2),
                                 --myvehdir.x, myvehdir.y, myvehdir.z,
                                 --255, 0, 0,
                                 --0.3, brightness * 99.9,
                                 --0.5, 99.0, 99.0, 1.0)
                             -- end
                         -- end
                     end

                    end
                end

            end
        end)*/
    }
}