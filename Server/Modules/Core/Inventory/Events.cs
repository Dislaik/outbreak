using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Newtonsoft.Json;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System.Dynamic;
using CitizenFX.Core.Native;
using System.ComponentModel.Design;

namespace Outbreak.Core
{
    public partial class Inventory
    {
        private void Events()
        {
            EventHandlers["Inventory:UpdateWeaponAmmo"] += new Action<Player, string>(WeaponAmmo);
            EventHandlers["Inventory:UpdateItemsDroped"] += new Action<Player, IDictionary<string, dynamic>, string>(UpdateItemDroped);
            EventHandlers["Inventory:UpdateSerializeItems"] += new Action<Player, IDictionary<string, dynamic>, NetworkCallbackDelegate>(UpdateSerializeItems);
            EventHandlers["Inventory:RemoveItem"] += new Action<Player, string, int>(RemoveItem);
            EventHandlers["Inventory:AddLootableWeapons"] += new Action<Player, string, int, string>(AddLootableWeapons);
            EventHandlers["Inventory:AddLootableItems"] += new Action<Player, string, string>(AddLootableItems);
            EventHandlers["Inventory:PlayerGiveItem"] += new Action<Player, int, string, int>(PlayerGiveItem);
            EventHandlers["Inventory:PlayerGiveWeapon"] += new Action<Player, int, string, int>(PlayerGiveWeapon);
        }

        private void WeaponAmmo([FromSource]Player Source, string WeaponName)
        {
            IPlayer.RemoveWeaponAmmo(Source, WeaponName, 1);
        }
        private void UpdateItemDroped([FromSource] Player Source, IDictionary<string, dynamic> ItemsDropedClient, string NewItemsDropedID)
        {
            ItemsDroped = ItemsDropedClient;
            ItemsDropedID += 1;

            TriggerClientEvent("Inventory:UpdateItemsDropedCallback", ItemsDroped, NewItemsDropedID);
        }
        private void UpdateSerializeItems([FromSource] Player Source, IDictionary<string, dynamic> ItemsDropedClient, NetworkCallbackDelegate CB)
        {

            foreach (dynamic Item in ItemsDropedClient.Keys.ToList())
            {


                if (Enum.IsDefined(typeof(Weapon.Hash), ItemsDropedClient[Item].Name))
                {
                    ItemsDropedClient[Item].Label = Inventory.Items[ItemsDropedClient[Item].Name].Label;
                    ItemsDropedClient[Item].Description = Inventory.Items[ItemsDropedClient[Item].Name].Description;
                    ItemsDropedClient[Item].Weight = Inventory.Items[ItemsDropedClient[Item].Name].Weight;
                }
                else
                {
                    ItemsDropedClient[Item].Label = Inventory.Items[ItemsDropedClient[Item].Name].Label;
                    ItemsDropedClient[Item].Description = Inventory.Items[ItemsDropedClient[Item].Name].Description;
                    ItemsDropedClient[Item].Weight = Inventory.Items[ItemsDropedClient[Item].Name].Weight;
                    ItemsDropedClient[Item].Limit = Inventory.Items[ItemsDropedClient[Item].Name].Limit;

                }
            }

            string NewInventory = JsonConvert.SerializeObject(ItemsDropedClient);
            CB.Invoke(NewInventory);
        }
        private void RemoveItem([FromSource] Player Source, string Name, int Amount)
        {
            if (Enum.IsDefined(typeof(Weapon.Hash), Name))
            {
                IPlayer.RemoveWeapon(Source, Name);
            }
            else
            {
                IPlayer.RemoveInventoryItem(Source, Name, Amount);
            }
            
        }
        private void AddLootableWeapons([FromSource] Player Source, string Name, int Ammo, string ID)
        {
            if (IPlayer.GetCurrentWeight(Source) < Config.MaxPlayerWeight)
            {
                if (!IPlayer.HasWeapon(Source, Name))
                {
                    IPlayer.AddWeapon(Source, Name, Ammo);
                    Source.TriggerEvent("Inventory:UpdateItemLootablee", ID, true);
                }
                else
                {
                    IPlayer.Notification(Source, "~y~[Warning]~s~ You already have this weapon in your inventory");
                }
            }
            else
            {
                IPlayer.Notification(Source, "~y~[Warning]~s~ Maximum weight reached, you cannot carry more things.");
            }
        }
        private void AddLootableItems([FromSource] Player Source, string Name, string ID)
        {
            if (IPlayer.GetCurrentWeight(Source) < Config.MaxPlayerWeight)
            {
                if (IPlayer.CanCarryInventoryItem(Source, Name, 1))
                {
                    IPlayer.AddInventoryItem(Source, Name, 1);
                    Source.TriggerEvent("Inventory:UpdateItemLootablee", ID);
                }
                else
                {
                    IPlayer.Notification(Source, "~y~[Warning]~s~ Item limit reached");
                }
            }
            else
            {
                IPlayer.Notification(Source, "~y~[Warning]~s~ Maximum weight reached, you cannot carry more things.");
            }
        }
        private string GetItemLimit([FromSource] Player Source, string Name)
        {
            foreach (dynamic Item in Items.Keys)
            {
                if (Item == Name)
                {
                    return Items[Item].Limit;
                }
            }
            return $"Item [{Name}] does not exist!";
        }
        private string GetItemLabel(string Name)
        {
            foreach (dynamic Item in Items.Keys)
            {
                if (Item == Name)
                {
                    return Items[Item].Label;
                }
            }
            return $"Item [{Name}] does not exist!";
        }
        private void PlayerGiveWeapon([FromSource] Player Source, int PlayerID, string Name, int Ammo)
        {
            Player TargetPlayer = new PlayerList()[PlayerID];

            if (IPlayer.GetCurrentWeight(TargetPlayer) < Config.MaxPlayerWeight)
            {
                if (!IPlayer.HasWeapon(TargetPlayer, Name))
                {
                    IPlayer.AddWeapon(TargetPlayer, Name, Ammo);
                    IPlayer.RemoveWeapon(Source, Name);

                    IPlayer.Notification(Source, $"~b~[Info]~s~ You gave an ~y~{GetItemLabel(Name)}~s~ to ~b~{IPlayer.GetName(TargetPlayer)}~s~.");
                    IPlayer.Notification(TargetPlayer, $"~b~[Info]~s~ You received an ~y~{GetItemLabel(Name)}~s~ from ~b~{IPlayer.GetName(Source)}~s~.");
                }
                else
                {
                    IPlayer.Notification(Source, $"~y~[Warning]~s~ You tried gave an ~y~{GetItemLabel(Name)}~s~ to ~b~{IPlayer.GetName(TargetPlayer)}~s~, but he already has one.");
                    IPlayer.Notification(TargetPlayer, $"~y~[Warning]~s~ ~b~{IPlayer.GetName(Source)}~s~ tried to gave you an ~y~{GetItemLabel(Name)}~s~, but you already have one.");
                }
            }
            else
            {
                IPlayer.Notification(Source, $"~y~[Warning]~s~ You tried gave an ~y~{GetItemLabel(Name)}~s~ to ~b~{IPlayer.GetName(TargetPlayer)}~s~, but he can't carry more things.");
                IPlayer.Notification(TargetPlayer, $"~y~[Warning]~s~ ~b~{IPlayer.GetName(Source)}~s~ tried to gave you an ~y~{GetItemLabel(Name)}~s~, but you can't carry more things.");
            }
        }
        private void PlayerGiveItem([FromSource] Player Source, int PlayerID, string Name, int Amount)
        {
            Player TargetPlayer = new PlayerList()[PlayerID];

            if (IPlayer.GetCurrentWeight(TargetPlayer) < Config.MaxPlayerWeight)
            {
                if (IPlayer.CanCarryInventoryItem(TargetPlayer, Name, Amount))
                {
                    IPlayer.RemoveInventoryItem(Source, Name, Amount);
                    IPlayer.AddInventoryItem(TargetPlayer, Name, Amount);

                    if (Amount > 1)
                    {
                        IPlayer.Notification(Source, $"~b~[Info]~s~ You gave an ~y~{GetItemLabel(Name)}~s~ x{Amount} to ~b~{IPlayer.GetName(TargetPlayer)}~s~.");
                        IPlayer.Notification(TargetPlayer, $"~b~[Info]~s~ You received an ~y~{GetItemLabel(Name)}~s~ x{Amount} from ~b~{IPlayer.GetName(Source)}~s~.");
                    }
                    else
                    {
                        IPlayer.Notification(Source, $"~b~[Info]~s~ You gave an ~y~{GetItemLabel(Name)}~s~ to ~b~{IPlayer.GetName(TargetPlayer)}~s~.");
                        IPlayer.Notification(TargetPlayer, $"~b~[Info]~s~ You received an ~y~{GetItemLabel(Name)}~s~ from ~b~{IPlayer.GetName(Source)}~s~.");
                    }
                }
                else
                {
                    IPlayer.Notification(Source, $"~y~[Warning]~s~ You tried gave an ~y~{GetItemLabel(Name)}~s~ to ~b~{IPlayer.GetName(TargetPlayer)}~s~, but he can't take more.");
                    IPlayer.Notification(TargetPlayer, $"~y~[Warning]~s~ ~b~{IPlayer.GetName(Source)}~s~ tried to gave you an ~y~{GetItemLabel(Name)}~s~, but you can't take more.");
                }
            }
            else
            {
                IPlayer.Notification(Source, $"~y~[Warning]~s~ You tried gave an ~y~{GetItemLabel(Name)}~s~ to ~b~{IPlayer.GetName(TargetPlayer)}~s~, but he can't carry more things.");
                IPlayer.Notification(TargetPlayer, $"~y~[Warning]~s~ ~b~{IPlayer.GetName(Source)}~s~ tried to gave you an ~y~{GetItemLabel(Name)}~s~, but you can't carry more things.");
            }
            
        }
    }
}