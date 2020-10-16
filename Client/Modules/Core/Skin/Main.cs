using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Skin : BaseScript
    {
        private static float PlayerHeading = -20f;
        private static bool MenuOpen = false;
        private static bool ViewCamera = true;
        private static int Cam = 2;

        public Skin()
        {
            Events();


            Command.Register("skin", "User", new Action<int, List<object>, string>((source, args, raw) =>
            {
                NUI(Player.Sex, "true");

            }), "On NUI");

            Command.Register("off", "User", new Action<int, List<object>, string>((source, args, raw) =>
            {
                SendNuiMessage("{ \"Type\": \"Skin\", \"Sex\": \"Male\", \"Display\": false }");
                SendNuiMessage("{ \"Type\": \"Skin\", \"Sex\": \"Female\", \"Display\": false }");
                SetNuiFocus(false, false);

            }), "Off NUI");


            Tick += SkinCamera;
        }

        public static void NUI(string Sex, string Display)
        {
            string JSON = "" +
                "{" +
                    $"\"Type\": \"Skin\"," +
                    $"\"Sex\": \"{Sex}\"," +
                    $"\"Display\": {Display}" +
                "}" +
            "";

            Cam = Utils.Game.CreateCamera(Cam, 0f, 0f, 180f);
            Vector3 PlayerCoords = GetEntityCoords(PlayerPedId(), true);
            SetCamCoord(Cam, PlayerCoords.X + 0.2f, PlayerCoords.Y + 0.5f, PlayerCoords.Z + 0.6f);
            SendNuiMessage(JSON);
            SetNuiFocus(true, true);
            DisplayRadar(false);
            MenuOpen = true;
        }

        private async Task SkinCamera()
        {
            if (MenuOpen)
            {
                SetEntityHeading(PlayerPedId(), PlayerHeading);
            }

            await Delay(50);
        }
        private async void LoadPlayer(string Sex, IDictionary<string, object> PlayerSkin)
        {
            if (Sex == "Male")
            {
                SetModelToPlayer("mp_m_freemode_01");
            }
            else if (Sex == "Female")
            {
                SetModelToPlayer("mp_f_freemode_01");
            }

            await Delay(3200);

            PlayerSkinComponents(PlayerSkin);
            Player.Loaded = true;
            Player.GetData();

        }
        public static async void SetModelToPlayer(string Model)
        {
            int Hash = GetHashKey(Model);
            RequestModel((uint)Hash);
            while (!HasModelLoaded((uint)Hash))
            {
                await Delay(0);
            }

            SetPlayerModel(PlayerId(), (uint)Hash);
        }

        private void PlayerSkinComponents(IDictionary<string, object> PlayerSkin)
        {
            SetPedHeadBlendData(PlayerPedId(), Convert.ToInt32(PlayerSkin["Face"]), 0, 0, Convert.ToInt32(PlayerSkin["Skin"]), 0, 0, 0f, 0f, 0f, false);
            SetPedComponentVariation(PlayerPedId(), 2, Convert.ToInt32(PlayerSkin["Hair"]), 0, 0);
            SetPedHairColor(PlayerPedId(), Convert.ToInt32(PlayerSkin["HairColor"]), 0);
            SetPedEyeColor(PlayerPedId(), Convert.ToInt32(PlayerSkin["EyesColor"]));
            if (Convert.ToInt32(PlayerSkin["Blemishes"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 0, 0, 0f); }
            else { SetPedHeadOverlay(PlayerPedId(), 0, Convert.ToInt32(PlayerSkin["Blemishes"]), 1f); }
            if (Convert.ToInt32(PlayerSkin["Beard"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 1, 0, 0f); }
            else { SetPedHeadOverlay(PlayerPedId(), 1, Convert.ToInt32(PlayerSkin["Beard"]), 1f); }
            SetPedHeadOverlay(PlayerPedId(), 2, Convert.ToInt32(PlayerSkin["Eyebrows"]), 1f);
            SetPedHeadOverlayColor(PlayerPedId(), 1, 1, Convert.ToInt32(PlayerSkin["HairColor"]), 0);
            SetPedHeadOverlayColor(PlayerPedId(), 2, 1, Convert.ToInt32(PlayerSkin["HairColor"]), 0);
            SetPedHeadOverlayColor(PlayerPedId(), 10, 1, Convert.ToInt32(PlayerSkin["HairColor"]), 0);
            if (Convert.ToInt32(PlayerSkin["Wrinkles"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 3, 0, 0f); }
            else { SetPedHeadOverlay(PlayerPedId(), 3, Convert.ToInt32(PlayerSkin["Wrinkles"]), 1f); }
            if (Convert.ToInt32(PlayerSkin["Complexion"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 6, 0, 0f); }
            else { SetPedHeadOverlay(PlayerPedId(), 6, Convert.ToInt32(PlayerSkin["Complexion"]), 1f); }
            if (Convert.ToInt32(PlayerSkin["SunDamage"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 7, 0, 0f); }
            else { SetPedHeadOverlay(PlayerPedId(), 7, Convert.ToInt32(PlayerSkin["SunDamage"]), 1f); }
            if (Convert.ToInt32(PlayerSkin["Freckles"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 9, 0, 0f); }
            else { SetPedHeadOverlay(PlayerPedId(), 9, Convert.ToInt32(PlayerSkin["Freckles"]), 1f); }
            if (Convert.ToInt32(PlayerSkin["ChestHair"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 10, 0, 0f); }
            else { SetPedHeadOverlay(PlayerPedId(), 10, Convert.ToInt32(PlayerSkin["ChestHair"]), 1f); }
            if (Convert.ToInt32(PlayerSkin["BodyBlemishes"]) == -1) { SetPedHeadOverlay(PlayerPedId(), 11, 0, 0f); }
            else { SetPedHeadOverlay(PlayerPedId(), 11, Convert.ToInt32(PlayerSkin["BodyBlemishes"]), 1f); }
            SetPedPropIndex(PlayerPedId(), 0, Convert.ToInt32(PlayerSkin["Hat"]), 0, true);
            SetPedComponentVariation(PlayerPedId(), 11, Convert.ToInt32(PlayerSkin["Top"]), 0, 0);
            SetPedComponentVariation(PlayerPedId(), 8, Convert.ToInt32(PlayerSkin["Undershirt"]), 0, 0);
            SetPedComponentVariation(PlayerPedId(), 3, Convert.ToInt32(PlayerSkin["Torso"]), 0, 0);
            SetPedComponentVariation(PlayerPedId(), 4, Convert.ToInt32(PlayerSkin["Legs"]), 0, 0);
            SetPedComponentVariation(PlayerPedId(), 6, Convert.ToInt32(PlayerSkin["Shoes"]), 0, 0);
        }

        public static void DefaultComponents(string Sex)
        {
            if (Sex == "Male")
            {
                SetPedHeadBlendData(PlayerPedId(), 0, 0, 0, 0, 0, 0, 0f, 0f, 0f, false);
                SetPedComponentVariation(PlayerPedId(), 2, 0, 0, 0);
                SetPedHairColor(PlayerPedId(), 0, 0);
                SetPedEyeColor(PlayerPedId(), 0);
                SetPedHeadOverlay(PlayerPedId(), 0, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 1, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 2, 0, 1f);
                SetPedHeadOverlayColor(PlayerPedId(), 1, 1, 0, 0);
                SetPedHeadOverlayColor(PlayerPedId(), 2, 1, 0, 0);
                SetPedHeadOverlayColor(PlayerPedId(), 10, 1, 0, 0);
                SetPedHeadOverlay(PlayerPedId(), 3, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 6, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 7, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 9, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 10, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 11, 0, 0f);
                SetPedPropIndex(PlayerPedId(), 0, 8, 0, true);
                SetPedComponentVariation(PlayerPedId(), 11, 15, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 8, 15, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 3, 15, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 4, 21, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 6, 34, 0, 0);
            }
            else if (Sex == "Female")
            {
                SetPedHeadBlendData(PlayerPedId(), 21, 0, 0, 0, 0, 0, 0f, 0f, 0f, false);
                SetPedComponentVariation(PlayerPedId(), 2, 0, 0, 0);
                SetPedHairColor(PlayerPedId(), 0, 0);
                SetPedEyeColor(PlayerPedId(), 0);
                SetPedHeadOverlay(PlayerPedId(), 0, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 1, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 2, 0, 1f);
                SetPedHeadOverlayColor(PlayerPedId(), 1, 1, 0, 0);
                SetPedHeadOverlayColor(PlayerPedId(), 2, 1, 0, 0);
                SetPedHeadOverlayColor(PlayerPedId(), 10, 1, 0, 0);
                SetPedHeadOverlay(PlayerPedId(), 3, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 6, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 7, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 9, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 10, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 11, 0, 0f);
                SetPedPropIndex(PlayerPedId(), 0, 120, 0, true);
                SetPedComponentVariation(PlayerPedId(), 11, 22, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 8, 14, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 3, 15, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 4, 20, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 6, 35, 0, 0);
            }
        }
    }
}
