using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Skin
    {
        private void Events()
        {
            RegisterNuiCallbackType("Skin:Submit");
            EventHandlers["__cfx_nui:Skin:Submit"] += new Action<IDictionary<string, object>, CallbackDelegate>((Data, CB) =>
            {
                Dictionary<string, string> PlayerSkinDictionary = new Dictionary<string, string>
                {
                    { "Skin", Data["Skin"].ToString() },
                    { "Face", Data["Face"].ToString() },
                    { "Hair", Data["Hair"].ToString() },
                    { "HairColor", Data["HairColor"].ToString() },
                    { "EyesColor", Data["EyesColor"].ToString() },
                    { "Blemishes", Data["Blemishes"].ToString() },
                    { "Beard", Data["Beard"].ToString() },
                    { "Eyebrows", Data["Eyebrows"].ToString() },
                    { "Wrinkles", Data["Wrinkles"].ToString() },
                    { "Complexion", Data["Complexion"].ToString() },
                    { "SunDamage", Data["SunDamage"].ToString() },
                    { "Freckles", Data["Freckles"].ToString() },
                    { "ChestHair", Data["ChestHair"].ToString() },
                    { "BodyBlemishes", Data["BodyBlemishes"].ToString() },
                    { "Hat", Data["Hat"].ToString() },
                    { "Top", Data["Top"].ToString() },
                    { "Undershirt", Data["Undershirt"].ToString() },
                    { "Torso", Data["Torso"].ToString() },
                    { "Legs", Data["Legs"].ToString() },
                    { "Shoes", Data["Shoes"].ToString() }
                };

                string PlayerSkin = Utils.String.DictionaryToString(PlayerSkinDictionary);
                Console.Info($"Skin Created: {PlayerSkin}");

                TriggerServerEvent("Skin:SetPlayerSkin", PlayerSkin);
                NUI(Player.Sex, "false");
                SetNuiFocus(false, false);
                MenuOpen = false;
                DisplayRadar(true);
                Utils.Game.DeleteCamera(Cam);
            });

            RegisterNuiCallbackType("Skin:Update");
            EventHandlers["__cfx_nui:Skin:Update"] += new Action<IDictionary<string, object>, CallbackDelegate>((Data, CB) =>
            {

                SetPedHeadBlendData(PlayerPedId(), Convert.ToInt32(Data["Face"]), 0, 0, Convert.ToInt32(Data["Skin"]), 0, 0, 0f, 0f, 0f, false);
                SetPedComponentVariation(PlayerPedId(), 2, Convert.ToInt32(Data["Hair"]), 0, 0);
                SetPedHairColor(PlayerPedId(), Convert.ToInt32(Data["HairColor"]), 0);
                SetPedEyeColor(PlayerPedId(), Convert.ToInt32(Data["EyesColor"]));
                if (Convert.ToInt32(Data["Blemishes"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 0, 0, 0f); }
                else { SetPedHeadOverlay(PlayerPedId(), 0, Convert.ToInt32(Data["Blemishes"]), 1f); }
                if (Convert.ToInt32(Data["Beard"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 1, 0, 0f); }
                else { SetPedHeadOverlay(PlayerPedId(), 1, Convert.ToInt32(Data["Beard"]), 1f); }
                SetPedHeadOverlay(PlayerPedId(), 2, Convert.ToInt32(Data["Eyebrows"]), 1f);
                SetPedHeadOverlayColor(PlayerPedId(), 1, 1, Convert.ToInt32(Data["HairColor"]), 0);
                SetPedHeadOverlayColor(PlayerPedId(), 2, 1, Convert.ToInt32(Data["HairColor"]), 0);
                SetPedHeadOverlayColor(PlayerPedId(), 10, 1, Convert.ToInt32(Data["HairColor"]), 0);
                if (Convert.ToInt32(Data["Wrinkles"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 3, 0, 0f); }
                else { SetPedHeadOverlay(PlayerPedId(), 3, Convert.ToInt32(Data["Wrinkles"]), 1f); }
                if (Convert.ToInt32(Data["Complexion"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 6, 0, 0f); }
                else { SetPedHeadOverlay(PlayerPedId(), 6, Convert.ToInt32(Data["Complexion"]), 1f); }
                if (Convert.ToInt32(Data["SunDamage"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 7, 0, 0f); }
                else { SetPedHeadOverlay(PlayerPedId(), 7, Convert.ToInt32(Data["SunDamage"]), 1f); }
                if (Convert.ToInt32(Data["Freckles"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 9, 0, 0f); }
                else { SetPedHeadOverlay(PlayerPedId(), 9, Convert.ToInt32(Data["Freckles"]), 1f); }
                if (Convert.ToInt32(Data["ChestHair"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 10, 0, 0f); }
                else { SetPedHeadOverlay(PlayerPedId(), 10, Convert.ToInt32(Data["ChestHair"]), 1f); }
                if (Convert.ToInt32(Data["BodyBlemishes"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 11, 0, 0f); }
                else { SetPedHeadOverlay(PlayerPedId(), 11, Convert.ToInt32(Data["BodyBlemishes"]), 1f); }
                SetPedPropIndex(PlayerPedId(), 0, Convert.ToInt32(Data["Hat"]), 0, true);
                SetPedComponentVariation(PlayerPedId(), 11, Convert.ToInt32(Data["Top"]), 0, 0);
                SetPedComponentVariation(PlayerPedId(), 8, Convert.ToInt32(Data["Undershirt"]), 0, 0);
                SetPedComponentVariation(PlayerPedId(), 3, Convert.ToInt32(Data["Torso"]), 0, 0);
                SetPedComponentVariation(PlayerPedId(), 4, Convert.ToInt32(Data["Legs"]), 0, 0);
                SetPedComponentVariation(PlayerPedId(), 6, Convert.ToInt32(Data["Shoes"]), 0, 0);
            });

            RegisterNuiCallbackType("Skin:RotateLeft");
            EventHandlers["__cfx_nui:Skin:RotateLeft"] += new Action<IDictionary<string, object>, CallbackDelegate>((Data, CB) =>
            {
                float currentHeading = GetEntityHeading(PlayerPedId());
                PlayerHeading = (currentHeading + Convert.ToSingle(Data["Left"]));

            });

            RegisterNuiCallbackType("Skin:RotateRight");
            EventHandlers["__cfx_nui:Skin:RotateRight"] += new Action<IDictionary<string, object>, CallbackDelegate>((Data, CB) =>
            {
                float currentHeading = GetEntityHeading(PlayerPedId());
                PlayerHeading = (currentHeading - Convert.ToSingle(Data["Right"]));
            });

            RegisterNuiCallbackType("Skin:View");
            EventHandlers["__cfx_nui:Skin:View"] += new Action<IDictionary<string, object>, CallbackDelegate>((Data, CB) =>
            {
                Vector3 PlayerCoords = GetEntityCoords(PlayerPedId(), true);

                if (ViewCamera)
                {
                    SetCamCoord(Cam, PlayerCoords.X + 0.3f, PlayerCoords.Y + 2f, PlayerCoords.Z);
                    ViewCamera = false;
                }
                else
                {
                    SetCamCoord(Cam, PlayerCoords.X + 0.2f, PlayerCoords.Y + 0.5f, PlayerCoords.Z + 0.6f);
                    ViewCamera = true;
                }
            });

            EventHandlers["Skin:LoadPlayerSkin"] += new Action<string, IDictionary<string, object>>(LoadPlayer);
        }
    }
}
