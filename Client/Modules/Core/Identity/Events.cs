using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Identity
    {
        private void Events()
        {
            EventHandlers["Identity:Register"] += new Action(NUI);

            RegisterNuiCallbackType("Identity:Submit");
            EventHandlers["__cfx_nui:Identity:Submit"] += new Action<IDictionary<string, object>, CallbackDelegate>((Data, CB) =>
            {
                SendNuiMessage("{ \"Type\": \"Identity\", \"Display\": false }");
                Utils.Game.DeleteCamera(Cam);
                TriggerServerEvent("Identity:SetPlayerIdentity", $"{Data["FirstName"]} {Data["LastName"]}", Data["DateOfBirth"].ToString(), Data["Sex"].ToString(), "User", "Survivor");

                if (Data["Sex"].ToString() == "Male")
                {
                    Skin.SetModelToPlayer("mp_m_freemode_01");
                } 
                else if (Data["Sex"].ToString() == "Female")
                {
                    Skin.SetModelToPlayer("mp_f_freemode_01");
                }

                Skin.DefaultComponents(Data["Sex"].ToString());
                Skin.NUI(Data["Sex"].ToString(), "true");
                Player.Loaded = true;
                Player.GetData();
            });
        }
    }
}
