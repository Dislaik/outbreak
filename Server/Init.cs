using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Outbreak
{
    public class Main : BaseScript
    {
        public Main()
        {
            EventHandlers["onResourceStarting"] += new Action(Init);
        }

        private void Init()
        {
            Debug.WriteLine("");
            Debug.WriteLine("^1[Outbreak]^7 https://github.com/dislaik/outbreak - Version 0.1.7");
            Debug.WriteLine("^1[Outbreak]^7 Zombie Outbreak Ready!");
            Debug.WriteLine("");
        }

    }
}