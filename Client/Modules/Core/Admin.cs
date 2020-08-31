using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    class Admin : BaseScript
    {
        public Admin()
        {
            EventHandlers["Outbreak.Core.Admin:GetCoords"] += new Action(GetCoords);
            EventHandlers["Outbreak.Core.Admin:TPMarker"] += new Action(TPMarker);
            EventHandlers["Outbreak.Core.Admin:GiveWeapon"] += new Action<List<object>>(GiveWeapon);
        }

        private async void TPMarker()
        {
            var Mark = GetFirstBlipInfoId(8);

            if (DoesBlipExist(Mark))
            {
                var MarkCoords = GetBlipInfoIdCoord(Mark);
                Convert.ToSingle(MarkCoords.X);
                Convert.ToSingle(MarkCoords.Y);
                Convert.ToSingle(MarkCoords.Z);
                float Default = 0.0f;

                for (int i = 1; i < 999; i++)
                {
                    SetPedCoordsKeepVehicle(PlayerPedId(), MarkCoords.X, MarkCoords.Y, i);

                    bool GetGroundZ = GetGroundZFor_3dCoord(MarkCoords.X, MarkCoords.Y, i, ref Default, false);

                    if (GetGroundZ)
                    {
                        SetPedCoordsKeepVehicle(PlayerPedId(), MarkCoords.X, MarkCoords.Y, i + 0.3f);
                        break;
                    }
                    await Delay(10);
                }
                Screen.ShowNotification("~b~You have teleported~b~");
            }
            else
            {
                Screen.ShowNotification("~b~There is no marker to teleport~b~");
            }

        }

        private void GetCoords()
        {
            Vector3 playercoords = GetEntityCoords(PlayerPedId(), true);
            Debug.WriteLine($"^1[Outbreak.Core.Admin]^7: {playercoords}");
        }

        private void GiveWeapon(List<object> args)
        {
            GiveWeaponToPed(PlayerPedId(), (uint)GetHashKey(args[0].ToString()), int.Parse(args[1].ToString()), false, true);
        }
    }
}
