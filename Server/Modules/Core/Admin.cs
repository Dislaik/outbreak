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
    class Admin : BaseScript
    {

       public Admin()
        {
            //Commands
            GetCoords("getcoords");
            TPMarker("tpmarker");
        }

        private void GetCoords(string command)
        {
            RegisterCommand(command, new Action<int, List<object>, string>((source, args, rawCommand) =>
            {
                TriggerClientEvent("chat:addSuggestion", "/" + command, "Prints in client console your actual position.");
                TriggerClientEvent("Outbreak.Core.Admin:GetCoords");

            }), false);

        }

        private void TPMarker(string command)
        {
            RegisterCommand(command, new Action<int, List<object>, string>((source, args, rawCommand) =>
            {
                TriggerClientEvent("chat:addSuggestion", "/" + command, "Teleport to your marker.");
                TriggerClientEvent("Outbreak.Core.Admin:TPMarker");

            }), false);
        }
    }
}
