using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Outbreak.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Identity : BaseScript
    {
        public Identity()
        {Events();

            Command.Register("delchar", "User", new Action<CitizenFX.Core.Player, List<object>, string>((Source, Arguments, Raw) =>
            {
                Source.TriggerEvent("Identity:Register");

            }), "Delete your current character");

            Command.Register("delchar", "Admin", new Action<CitizenFX.Core.Player, List<object>, string>((Source, Arguments, Raw) =>
            {
                Source.TriggerEvent("Identity:Register");

            }), "Delete your current character");
        }
    }
}
