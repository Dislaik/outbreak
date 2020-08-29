using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public class Environment : BaseScript
    {

        public Environment()
        {
            SetArtificialLightsState(true);
            StartAudioScene("CHARACTER_CHANGE_IN_SKY_SCENE");
            //SeethroughSetHiLightIntensity(0.0f);

            var Interior = new[] {
                new {X = 438.6989f, Y = -983.5912f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
                new {X = 441.9033f, Y = -976.4308f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
                new {X = 449.6308f, Y = -975.7319f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
                new {X = 446.3341f, Y = -986.2154f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
                new {X = 451.6483f, Y = -981.1121f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 6.0f, Intensity = 0.4f},
                new {X = 456.1187f, Y = -990.7253f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
                new {X = 456.0132f, Y = -986.0571f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
                new {X = 446.6374f, Y = -992.9275f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f},
                new {X = 439.0417f, Y = -993.5868f, Z = 32.67834f, R = 170, G = 255, B = 200, Range = 8.0f, Intensity = 0.4f}
            };

            InteriorLights(Interior);
        }

        private async void InteriorLights(dynamic Interior)
        {
            while (true)
            {
                foreach (var v in Interior)
                {
                    DrawLightWithRange(v.X, v.Y, v.Z, v.R, v.G, v.B, v.Range, v.Intensity);
                }
                await Delay(0);
            }
        }
    }
}
