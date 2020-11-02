using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak
{
    public class Console
    {
        public static void Debug(string Data) => CitizenFX.Core.Debug.WriteLine(Data);
        public static void Info(string Data) => CitizenFX.Core.Debug.WriteLine($"^5[Info]^7 " + Data);
        public static void Warning(string Data) => CitizenFX.Core.Debug.WriteLine($"^3[Warning]^7 " + Data);
        public static void Error(string Data) => CitizenFX.Core.Debug.WriteLine($"^1[Error]^7 " + Data);
    }

    public class Command
    {
        public static void Register(string Command, string Group, Action<Player, List<object>, string> Handler, string Suggestion, params object[] Args)
        {
            RegisterCommand(Command, new Action<int, List<object>, string>((Source, Arguments, Raw) =>
            {
                Player Player = new PlayerList()[Source];
                dynamic PlayerData = Core.IPlayer.GetDataDatabase(Player);
                string PlayerGroup = PlayerData.Group;

                if (Group == PlayerGroup)
                {
                    Handler(Player, Arguments, Raw);
                }
                else
                {
                    ChatMessage.Error(Player, "Insufficient permissions to use this command.");
                }

            }), false);
            BaseScript.TriggerClientEvent("chat:addSuggestion", $"/{Command}", Suggestion, Args);
        }
    }

    public class ChatMessage
    {
        public static void Error([FromSource] Player Source, string Message)
        {
            Source.TriggerEvent("chat:addMessage", new { template = "<b style=\"color: #E13A44;\">{0}</b> {1}", multiline = true, args = new[] { "[Error]", Message } });
        }
        public static void Warning([FromSource] Player Source, string Message)
        {
            Source.TriggerEvent("chat:addMessage", new { template = "<b style=\"color: #E7BF43;\">{0}</b> {1}", multiline = true, args = new[] { "[Warning]", Message } });
        }
        public static void Info([FromSource] Player Source, string Message)
        {
            Source.TriggerEvent("chat:addMessage", new { template = "<b style=\"color: #4F5FE6;\">{0}</b> {1}", multiline = true, args = new[] { "[Info]", Message } });
        }
    }

}