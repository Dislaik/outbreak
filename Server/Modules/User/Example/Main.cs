using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.User
{
    public partial class Example : BaseScript
    {
        public Example()
        {
            Events();

            Command.Register("example", "Admin", new Action<CitizenFX.Core.Player, List<object>, string>((Source, Arguments, Raw) =>
            {

                //Source.TriggerEvent("Example:Test");


            }), "Suggestion Example from Server-Side");

        }
    }
}
