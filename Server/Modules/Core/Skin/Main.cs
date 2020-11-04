using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Outbreak.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Skin : BaseScript
    {
        public Skin()
        {Events();

            Command.Register("skin", "Admin", new Action<CitizenFX.Core.Player, List<object>, string>((Source, Arguments, Raw) =>
            {
                dynamic PlayerData = Player.GetDataDatabase(Source);
                string PlayerSex = PlayerData.Sex;
                Source.TriggerEvent("Skin:OpenNUI", PlayerSex, true);

            }), "Reskin your character");

        }
    }
}
