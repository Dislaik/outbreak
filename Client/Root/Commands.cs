using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak
{
    public class Command
    {
        public static void Register(string Command, string Group, Action<int, List<object>, string> Handler, string Suggestion, params object[] args)
        {
                RegisterCommand(Command, new Action<int, List<object>, string>((source, args, raw) =>
                {
                    if (Group == Core.Player.Group)
                    {
                        Handler(source, args, raw);
                    }
                }), false);
            BaseScript.TriggerEvent("chat:addSuggestion", $"/{Command}", Suggestion, args);
        }
    }
}