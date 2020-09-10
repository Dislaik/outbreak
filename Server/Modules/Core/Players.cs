using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public class Players : BaseScript
    {
        Config Config = new Config();
        public Players()
        {
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
        }

        private async void OnPlayerConnecting([FromSource] Player source, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            deferrals.defer();

            await Delay(0);

            string Identifier = source.Identifiers[Config.PlayerIdentifier];
            Debug.WriteLine($"^1[Outbreak]^7 {playerName} - Identifier authenticated {Identifier}");


            deferrals.done();
        }

    }
}
