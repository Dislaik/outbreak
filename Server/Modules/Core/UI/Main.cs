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
    public partial class UI : BaseScript
    {
        public UI()
        {Events();
            
        }
        public static void ShowNotification([FromSource] CitizenFX.Core.Player Source, string Message)
        {
            TriggerClientEvent(Source, "UI:ShowNotification", Message);
        }

        public static void ShowSubtitle([FromSource] CitizenFX.Core.Player Source, string Message, int Duration = 2500)
        {
            TriggerClientEvent(Source, "UI:ShowSubtitle", Message, Duration);
        }
    }
}
