using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak
{
    public class Default
    {
        public Default()
        {
            SetArtificialLightsState(true);
            SetScenarioGroupEnabled("LSA_Planes", false);
            StartAudioScene("CHARACTER_CHANGE_IN_SKY_SCENE");
            SetDistantCarsEnabled(true);
            SetMaxWantedLevel(0);
        }
    }
}