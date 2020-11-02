using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;


namespace Outbreak.Core
{
    public partial class Identity : BaseScript
    {
        public int Cam { get; set; } = 20;
        public Identity()
        { Events();

        }
        public void NUI()
        {
            Cam = Utils.Game.CreateCamera(Cam, -20f, 0f, 130f);
            SetCamCoord(Cam, 412.4021f, -964.2137f, 38.4747f);
            SendNuiMessage("{ \"Type\": \"Identity\", \"Display\": true }");
            SetNuiFocus(true, true);
            DisplayRadar(false);

            Inventory.QuickSlots(false);
        }
    }
}
