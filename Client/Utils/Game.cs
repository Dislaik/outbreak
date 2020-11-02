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
        public static void DrawText3D(string Text, float X, float Y, float Z)
        {
            float _ScreenX = (Screen.Resolution.Width / 2f);
            float _ScreenY = (Screen.Resolution.Height / 2f);
            var OnScreen = GetScreenCoordFromWorldCoord(X, Y, Z, ref _ScreenX, ref _ScreenY);
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
                SetTextDropShadow();
                SetTextOutline();
                SetTextEntry("STRING");
                SetTextCentre(true);
                AddTextComponentString(Text);
                DrawText(_ScreenX, _ScreenY);
            }

        }

        public static void DrawText2D(string Text, float X, float Y, float Scale, int Font, int Justification, int Red, int Green, int Blue, int Alpha)
        {
            SetTextScale(Scale, Scale);
            SetTextFont(Font);
            SetTextProportional(true);
            SetTextColour(Red, Green, Blue, Alpha);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            SetTextJustification(Justification);
            AddTextComponentString(Text);
            DrawText(X, Y);
        }

        public static int CreateCamera(int Cam, float RotX, float RotY, float RotZ)
        {
            if (!DoesCamExist(Cam))
            {
                Cam = CreateCam("DEFAULT_SCRIPTED_CAMERA", true);
            }
            SetCamActive(Cam, true);
            RenderScriptCams(true, true, 500, true, true);
            SetCamRot(Cam, RotX, RotY, -RotZ, 2);

            return Cam;
        }
        public static void DeleteCamera(int Cam)
        {
            SetCamActive(Cam, false);
            RenderScriptCams(false, true, 500, true, true);
        }
    }
}