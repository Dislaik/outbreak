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
    public class Admin : BaseScript
    {

        public Admin()
        {
            Menu MainMenu = new Menu("Admin Menu", "Manage the server")
            {
                TitleFont = 2,
                HeaderColor = new int[] { 199, 0, 57, 255 }
            };

            MainMenu.Register(MainMenu);
            MainMenu.AddItem("Get Coords", "Prints on the screen your own coords.");
            MainMenu.AddItem("TP Marker", "Teleport to the marker.");
            MainMenu.AddItem("Get a pistol", "Get a simple weapon.");

            MainMenu.OnItemSelect += (name, index) =>
            {
                if (index == 1)
                {
                    GetCoords();
                }
                else if (index == 2)
                {
                    TPMarker();
                }
                else if (index == 3)
                {
                    GiveWeapon();
                }

            };

            Tick += async () =>
            {
                MainMenu.Initiation();

                if (Game.IsControlJustPressed(0, Control.InteractionMenu) && !MainMenu.IsAnyMenuOpen())
                {
                    MainMenu.Changer();
                }

                await Task.FromResult(0);
            };

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

        private void GiveWeapon()
        {
            GiveWeaponToPed(PlayerPedId(), (uint)GetHashKey("WEAPON_PISTOL"), 250, false, true);
        }
    }
}
