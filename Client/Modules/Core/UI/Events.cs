using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;


namespace Outbreak.Core
{
    public partial class UI
    {
        private void Events()
        {
            EventHandlers["UI:ShowNotification"] += new Action<string>(ShowNotification);
            EventHandlers["UI:ShowSubtitle"] += new Action<string, int>(ShowSubtitle);
        }
        public static void ShowNotification(string Message)
        {
            Screen.ShowNotification(Message);
        }

        public static void ShowSubtitle(string Message, int Duration = 2500)
        {
            Screen.ShowSubtitle(Message, Duration);
        }
    }
}