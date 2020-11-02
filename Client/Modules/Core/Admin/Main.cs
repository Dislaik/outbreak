using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Admin : BaseScript
    {
        Translation Translate { get; } = new Translation(Config.Lang);
        public Admin()
        { Events(); Translate.Global();

            Menu MainMenu = new Menu(Translate._("Admin:Menu_Title"), Translate._("Admin:Menu_Subtitle"))
            {
                TitleFont = 4,
                HeaderColor = new int[] { 199, 0, 57, 255 }
            }; MainMenu.Register(MainMenu);

            MainMenu.AddItem("Get Coords", "Prints on the screen your own coords.");
            MainMenu.AddItem("TP Marker", "Teleport to the marker.");

            List<string> What = new List<string>
            {
                "WEAPON_PISTOL",
                "WEAPON_ASSAULTRIFLE",
                "WEAPON_BAT"
            };
            MainMenu.AddItemList("Get Weapon", "Get a weapon.", What);

            MainMenu.OnItemSelect += (name, index) =>
            {
                if (name == "Get Coords")
                {
                    GetCoords();
                }
                else if (name == "TP Marker")
                {
                    TPMarker();
                }
            };

            MainMenu.OnItemListSelect += (name, index, namelist, indexlist) =>
            {
                if (name == "Get Weapon")
                {
                    GiveWeapon(namelist);
                }
            };

            Tick += async () =>
            {
                MainMenu.Initiation();

                if (Game.IsControlJustPressed(0, Control.InteractionMenu) && !MainMenu.IsAnyMenuOpen())
                {
                    MainMenu.InteractMenu();
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
            Console.Info($"{playercoords}");
        }

        private void GiveWeapon(string Weapon)
        {
            GiveWeaponToPed(PlayerPedId(), (uint)GetHashKey(Weapon), 250, false, true);
        }
    }
}