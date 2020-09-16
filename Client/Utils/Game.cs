using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Utils
{
    public class Game
    {
        public static void DrawText3D(float X, float Y, float Z, string Text)
        {
            float _ScreenX = (Screen.Resolution.Width / 2f);
            float _ScreenY = (Screen.Resolution.Height / 2f);
            var OnScreen = GetScreenCoordFromWorldCoord(X, Y, Z, ref _ScreenX, ref _ScreenY); //Screen.WorldToScreen(X, Y, Z)
            var PlayerCam = GetFinalRenderedCamCoord();
            var Distance = GetDistanceBetweenCoords(PlayerCam.X, PlayerCam.Y, PlayerCam.Z, X, Y, Z, true);
            var Scale = ((1 / Distance) * 2);
            var Fov = ((1 / GetGameplayCamFov()) * 100);
            Scale *= Fov;
            if (OnScreen)
            {
                SetTextScale((0.0f + Scale), (0.35f + Scale));
                SetTextFont(0);
                SetTextProportional(true);
                SetTextColour(255, 255, 255, 255);
                SetTextDropshadow(0, 0, 0, 0, 255);
                SetTextEdge(2, 0, 0, 0, 150);
                SetTextDropShadow();
                SetTextOutline();
                SetTextEntry("STRING");
                SetTextCentre(true);
                AddTextComponentString(Text);
                DrawText(_ScreenX, _ScreenY);
            }

        }
    }

}
