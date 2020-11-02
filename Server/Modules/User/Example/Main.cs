using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Outbreak.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.User
{
    public partial class Example : BaseScript
    {
        public Example()
        {
            Events();

            Command.Register("example", "Admin", new Action<Player, List<object>, string>((Source, Arguments, Raw) =>
            {

                Source.TriggerEvent("Example:Test");


            }), "Suggestion Example from Server-Side");

            Command.Register("giveitem", "Admin", new Action<Player, List<object>, string>((Source, Arguments, Raw) =>
            {
                try
                {
                    if (Arguments.ToList().Count() < 1)
                    {
                        ChatMessage.Error(Source, "Missing arguments to define");
                    }
                    else if (Arguments.ToList().Count() < 3)
                    {
                        IPlayer.AddInventoryItem(Source, Arguments[0].ToString(), Convert.ToInt32(Arguments[1]));
                        
                    }
                    else
                    {
                        Player Player = Players.AsEnumerable().ToList().FirstOrDefault(k => k.Handle == Arguments[2].ToString());
                        if (Player == null)
                        {
                            ChatMessage.Error(Source, "Player ID not found");
                        }
                        else
                        {
                            IPlayer.AddInventoryItem(Player, Arguments[0].ToString(), Convert.ToInt32(Arguments[1]));
                        }
                    }
                }
                catch { }

            }), "Add items to user inventory",
            new { name = "Item", help = "Item to be added" },
            new { name = "Amount", help = "Amount of items" },
            new { name = "ID", help = "Player ID" });
        }
    }
}
