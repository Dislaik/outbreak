using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Player_
    {
        private void Events()
        {
            Database.Initialize();

            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers["Player:Spawned"] += new Action<Player>(OnPlayerRegister);
            EventHandlers["Player:Data"] += new Action<Player, NetworkCallbackDelegate>(CallbackGroup);
            EventHandlers["Identity:SetPlayerIdentity"] += new Action<Player, string, string, string, string, string>(SetPlayerRegistered);
            EventHandlers["Skin:SetPlayerSkin"] += new Action<Player, string>(SetPlayerSkin);
            EventHandlers["Player:GetPosition"] += new Action<Player, string>(SetPlayerPositionDB);
            EventHandlers["Player:InitPosition"] += new Action<Player>(OnPlayerPosition);
        }
    }
}
