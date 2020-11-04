using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.User
{
    public partial class Example
    {
        private void Events()
        {
            EventHandlers["Inventory:RegisterItem[example]"] += new Action<CitizenFX.Core.Player, string>(RegisterItem);
        }
        private void RegisterItem([FromSource] CitizenFX.Core.Player Source, string Item)
        {
            Console.Debug($"{Item} doing something");
        }
    }
}
