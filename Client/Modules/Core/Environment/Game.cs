using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core.Environment
{
    class Game : BaseScript
    {
        public Game()
        {
            SetArtificialLightsState(true);
            SetScenarioGroupEnabled("LSA_Planes", false);
            StartAudioScene("CHARACTER_CHANGE_IN_SKY_SCENE");
            SetDistantCarsEnabled(true);
            SetMaxWantedLevel(0);

            Tick += DisableServices;
        }

        private async Task DisableServices()
        {
            for (int i = 0; i <= 14; i++)
            {
                EnableDispatchService(i, false);
            }

            await Delay(500);
        }
    }
}
