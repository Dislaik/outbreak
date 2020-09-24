using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Outbreak
{
    public class Init : BaseScript
    {
        public Init()
        {
            EventHandlers["onResourceStarting"] += new Action(OnResourceStarting);
        }

        public void OnResourceStarting()
        {
            Debug.WriteLine("");
            Debug.WriteLine("^1[Outbreak]^7 https://github.com/dislaik/outbreak - Version 0.4.2");
            Debug.WriteLine("^1[Outbreak]^7 Zombie Outbreak Ready!");
            Debug.WriteLine("");
        }
    }
}
