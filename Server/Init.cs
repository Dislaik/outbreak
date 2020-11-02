using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak
{
    public class Init : BaseScript
    {
        public Init()
        {
            EventHandlers["onResourceStarting"] += new Action<string>(OnResourceStarting);
        }

        public void OnResourceStarting(string ResourceName)
        {
            if (ResourceName == "outbreak")
            {
                Debug.WriteLine("");
                Debug.WriteLine("^1[Outbreak]^7 https://github.com/dislaik/outbreak - Version 0.6.5");
                Debug.WriteLine("^1[Outbreak]^7 Zombie Outbreak Ready!");
                Debug.WriteLine("");
            }

            SetMapName("San Andreas");
            SetGameType("Zombie Survival RPG");
        }
    }
}
