using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    class Environment : BaseScript
    {
        private Environment()
        {
            SetMapName("Los Santos and Blaine");
            SetGameType("Zombie Survival RPG");

        }
    }
}
