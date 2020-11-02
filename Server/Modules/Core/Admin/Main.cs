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

namespace Outbreak.Core
{
    public partial class Admin : BaseScript
    {
        public Admin()
        { Events();

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

            }), "Add items to player inventory",
            new { name = "Item", help = "Item to be added" },
            new { name = "Amount", help = "Amount of items" },
            new { name = "ID", help = "Player ID" });

            Command.Register("giveweapon", "Admin", new Action<Player, List<object>, string>((Source, Arguments, Raw) =>
            {
                try
                {
                    if (Arguments.ToList().Count() < 1)
                    {
                        ChatMessage.Error(Source, "Missing arguments to define");
                    }
                    else if (Arguments.ToList().Count() < 3)
                    {
                        //IPlayer.AddInventoryItem(Source, Arguments[0].ToString(), Convert.ToInt32(Arguments[1]));
                        IPlayer.AddWeapon(Source, Arguments[0].ToString(), Convert.ToInt32(Arguments[1]));

                    }
                    else
                    {
                        Player TargetPlayer = Players.AsEnumerable().ToList().FirstOrDefault(k => k.Handle == Arguments[2].ToString());
                        if (TargetPlayer == null)
                        {
                            ChatMessage.Error(Source, "Player ID not found");
                        }
                        else
                        {
                            //IPlayer.AddInventoryItem(TargetPlayer, Arguments[0].ToString(), Convert.ToInt32(Arguments[1]));
                            IPlayer.AddWeapon(TargetPlayer, Arguments[0].ToString(), Convert.ToInt32(Arguments[1]));

                        }
                    }
                }
                catch { }

            }), "Add Weapon to player inventory",
            new { name = "Weapon", help = "Weapon name" },
            new { name = "Ammo", help = "Weapon ammo" },
            new { name = "ID", help = "Player ID" });

        }
    }
}
