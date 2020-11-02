using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Player
    {

        private void Events()
        {
            Exports["spawnmanager"].spawnPlayer(SpawnPosition());

            EventHandlers["playerSpawned"] += new Action(InitPlayer);
            EventHandlers["Player:SetPosition"] += new Action<float, float, float>(SetPlayerPosition);
            EventHandlers["Player:DeathDetection"] += new Action<dynamic>(OnPlayerDeath);
            EventHandlers["Player:Notification"] += new Action<string>(Notification);
        }
        public static void Notification(string Message)
        {
            Screen.ShowNotification(Message);
        }

    }
}
