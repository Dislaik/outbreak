using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core.Player
{
    public class Skin : BaseScript
    {
        public static async void SetPlayerModels(string Sex)
        {
            if (Sex == "Male")
            {
                SetModelToPlayer("mp_m_freemode_01");
            }
            else if (Sex == "Female")
            {
                SetModelToPlayer("mp_f_freemode_01");
            }

            await Delay(2000);

            DefaultCharacterComponents(Sex);
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
        public static async void DefaultCharacterComponents(string Sex)
        {
            await Delay(500);

            if (Sex == "Male")
            {
                SetPedHeadBlendData(PlayerPedId(), 0, 0, 0, 0, 0, 0, 0f, 0f, 0f, false);
                SetPedHairColor(PlayerPedId(), 0, 0);
                SetPedHeadOverlay(PlayerPedId(), 3, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 0, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 1, 0, 0f);
                SetPedEyeColor(PlayerPedId(), 0);
                SetPedHeadOverlay(PlayerPedId(), 2, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 4, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 8, 0, 0f);
                SetPedComponentVariation(PlayerPedId(), 2, 4, 0, 0);
                SetPedHeadOverlayColor(PlayerPedId(), 1, 1, 0, 0);
                SetPedHeadOverlayColor(PlayerPedId(), 2, 1, 0, 0);
                SetPedHeadOverlayColor(PlayerPedId(), 4, 1, 0, 0);
                SetPedHeadOverlayColor(PlayerPedId(), 8, 1, 0, 0);
                SetPedHeadOverlay(PlayerPedId(), 5, 0, 0f);
                SetPedHeadOverlayColor(PlayerPedId(), 5, 2, 0, 0);
                SetPedHeadOverlay(PlayerPedId(), 6, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 7, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 9, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 10, 0, 0f);
                SetPedHeadOverlayColor(PlayerPedId(), 10, 1, 0, 0);
                SetPedHeadOverlay(PlayerPedId(), 11, 0, 0f);
                SetPedPropIndex(PlayerPedId(), 2, -1, 0, true);


                SetPedComponentVariation(PlayerPedId(), 8, 15, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 11, 56, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 3, 0, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 10, 0, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 4, 1, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 6, 1, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 1, 0, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 9, 0, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 7, 0, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 5, 0, 0, 0);


                SetPedPropIndex(PlayerPedId(), 0, -1, 0, true);
                SetPedPropIndex(PlayerPedId(), 1, 0, 0, true);
                SetPedPropIndex(PlayerPedId(), 6, -1, 0, true);
                SetPedPropIndex(PlayerPedId(), 7, -1, 0, true);
            }
            else if (Sex == "Female")
            {
                SetPedHeadBlendData(PlayerPedId(), 21, 0, 0, 0, 0, 0, 0f, 0f, 0f, false);
                SetPedHairColor(PlayerPedId(), 0, 0);
                SetPedHeadOverlay(PlayerPedId(), 3, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 0, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 1, 0, 0f);
                SetPedEyeColor(PlayerPedId(), 0);
                SetPedHeadOverlay(PlayerPedId(), 2, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 4, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 8, 0, 0f);
                SetPedComponentVariation(PlayerPedId(), 2, 2, 0, 0);
                SetPedHeadOverlayColor(PlayerPedId(), 1, 1, 0, 0);
                SetPedHeadOverlayColor(PlayerPedId(), 2, 1, 0, 0);
                SetPedHeadOverlayColor(PlayerPedId(), 4, 1, 0, 0);
                SetPedHeadOverlayColor(PlayerPedId(), 8, 1, 0, 0);
                SetPedHeadOverlay(PlayerPedId(), 5, 0, 0f);
                SetPedHeadOverlayColor(PlayerPedId(), 5, 2, 0, 0);
                SetPedHeadOverlay(PlayerPedId(), 6, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 7, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 9, 0, 0f);
                SetPedHeadOverlay(PlayerPedId(), 10, 0, 0f);
                SetPedHeadOverlayColor(PlayerPedId(), 10, 1, 0, 0);
                SetPedHeadOverlay(PlayerPedId(), 11, 0, 0f);
                SetPedPropIndex(PlayerPedId(), 2, -1, 0, true);


                SetPedComponentVariation(PlayerPedId(), 8, 15, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 11, 49, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 3, 14, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 10, 0, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 4, 1, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 6, 3, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 1, 0, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 9, 0, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 7, 0, 0, 0);
                SetPedComponentVariation(PlayerPedId(), 5, 0, 0, 0);


                SetPedPropIndex(PlayerPedId(), 0, -1, 0, true);
                SetPedPropIndex(PlayerPedId(), 1, 5, 0, true);
                SetPedPropIndex(PlayerPedId(), 6, -1, 0, true);
                SetPedPropIndex(PlayerPedId(), 7, -1, 0, true);
            }
        }
    }
}
