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
            EventHandlers["Inventory:UpdateWeaponAmmo"] += new Action<CitizenFX.Core.Player, string>(WeaponAmmo);
            EventHandlers["Inventory:UpdateItemsDroped"] += new Action<CitizenFX.Core.Player, IDictionary<string, dynamic>, string>(UpdateItemDroped);
            EventHandlers["Inventory:UpdateSerializeItems"] += new Action<CitizenFX.Core.Player, IDictionary<string, dynamic>, NetworkCallbackDelegate>(UpdateSerializeItems);
            EventHandlers["Inventory:RemoveItem"] += new Action<CitizenFX.Core.Player, string, int>(RemoveItem);
            EventHandlers["Inventory:AddLootableWeapons"] += new Action<CitizenFX.Core.Player, string, int, string>(AddLootableWeapons);
            EventHandlers["Inventory:AddLootableItems"] += new Action<CitizenFX.Core.Player, string, string>(AddLootableItems);
            EventHandlers["Inventory:PlayerGiveItem"] += new Action<CitizenFX.Core.Player, int, string, int>(PlayerGiveItem);
            EventHandlers["Inventory:PlayerGiveWeapon"] += new Action<CitizenFX.Core.Player, int, string, int>(PlayerGiveWeapon);
        }

        private void WeaponAmmo([FromSource] CitizenFX.Core.Player Source, string WeaponName)
        {
            Player.RemoveWeaponAmmo(Source, WeaponName, 1);
        }
        private void UpdateItemDroped([FromSource] CitizenFX.Core.Player Source, IDictionary<string, dynamic> ItemsDropedClient, string NewItemsDropedID)
        {
            ItemsDroped = ItemsDropedClient;
            ItemsDropedID += 1;

            TriggerClientEvent("Inventory:UpdateItemsDropedCallback", ItemsDroped, NewItemsDropedID);
        }
        private void UpdateSerializeItems([FromSource] CitizenFX.Core.Player Source, IDictionary<string, dynamic> ItemsDropedClient, NetworkCallbackDelegate CB)
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
        private void RemoveItem([FromSource] CitizenFX.Core.Player Source, string Name, int Amount)
        {
            if (Enum.IsDefined(typeof(Weapon.Hash), Name))
            {
                Player.RemoveWeapon(Source, Name);
            }
            else
            {
                Player.RemoveItem(Source, Name, Amount);
            }
            
        }
        private void AddLootableWeapons([FromSource] CitizenFX.Core.Player Source, string Name, int Ammo, string ID)
        {
            if (Player.GetCurrentWeight(Source) < Config.MaxPlayerWeight)
            {
                if (!Player.HasWeapon(Source, Name))
                {
                    Player.AddWeapon(Source, Name, Ammo);
                    Source.TriggerEvent("Inventory:UpdateItemLootablee", ID, true);
                }
                else
                {
                    UI.ShowNotification(Source, "~y~[Warning]~s~ You already have this weapon in your inventory");
                }
            }
            else
            {
                UI.ShowNotification(Source, "~y~[Warning]~s~ Maximum weight reached, you cannot carry more things.");
            }
        }
        private void AddLootableItems([FromSource] CitizenFX.Core.Player Source, string Name, string ID)
        {
            if (Player.GetCurrentWeight(Source) < Config.MaxPlayerWeight)
            {
                if (Player.CanCarryItem(Source, Name, 1))
                {
                    Player.AddItem(Source, Name, 1);
                    Source.TriggerEvent("Inventory:UpdateItemLootablee", ID);
                }
                else
                {
                    UI.ShowNotification(Source, "~y~[Warning]~s~ Item limit reached");
                }
            }
            else
            {
                UI.ShowNotification(Source, "~y~[Warning]~s~ Maximum weight reached, you cannot carry more things.");
            }
        }
        private string GetItemLimit([FromSource] CitizenFX.Core.Player Source, string Name)
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
        private void PlayerGiveWeapon([FromSource] CitizenFX.Core.Player Source, int PlayerID, string Name, int Ammo)
        {
            CitizenFX.Core.Player TargetPlayer = new PlayerList()[PlayerID];

            if (Player.GetCurrentWeight(TargetPlayer) < Config.MaxPlayerWeight)
            {
                if (!Player.HasWeapon(TargetPlayer, Name))
                {
                    Player.AddWeapon(TargetPlayer, Name, Ammo);
                    Player.RemoveWeapon(Source, Name);

                    UI.ShowNotification(Source, $"~b~[Info]~s~ You gave an ~y~{GetItemLabel(Name)}~s~ to ~b~{Player.GetName(TargetPlayer)}~s~.");
                    UI.ShowNotification(TargetPlayer, $"~b~[Info]~s~ You received an ~y~{GetItemLabel(Name)}~s~ from ~b~{Player.GetName(Source)}~s~.");
                }
                else
                {
                    UI.ShowNotification(Source, $"~y~[Warning]~s~ You tried gave an ~y~{GetItemLabel(Name)}~s~ to ~b~{Player.GetName(TargetPlayer)}~s~, but he already has one.");
                    UI.ShowNotification(TargetPlayer, $"~y~[Warning]~s~ ~b~{Player.GetName(Source)}~s~ tried to gave you an ~y~{GetItemLabel(Name)}~s~, but you already have one.");
                }
            }
            else
            {
                UI.ShowNotification(Source, $"~y~[Warning]~s~ You tried gave an ~y~{GetItemLabel(Name)}~s~ to ~b~{Player.GetName(TargetPlayer)}~s~, but he can't carry more things.");
                UI.ShowNotification(TargetPlayer, $"~y~[Warning]~s~ ~b~{Player.GetName(Source)}~s~ tried to gave you an ~y~{GetItemLabel(Name)}~s~, but you can't carry more things.");
            }
        }
        private void PlayerGiveItem([FromSource] CitizenFX.Core.Player Source, int PlayerID, string Name, int Amount)
        {
            CitizenFX.Core.Player TargetPlayer = new PlayerList()[PlayerID];

            if (Player.GetCurrentWeight(TargetPlayer) < Config.MaxPlayerWeight)
            {
                if (Player.CanCarryItem(TargetPlayer, Name, Amount))
                {
                    Player.RemoveItem(Source, Name, Amount);
                    Player.AddItem(TargetPlayer, Name, Amount);

                    if (Amount > 1)
                    {
                        UI.ShowNotification(Source, $"~b~[Info]~s~ You gave an ~y~{GetItemLabel(Name)}~s~ x{Amount} to ~b~{Player.GetName(TargetPlayer)}~s~.");
                        UI.ShowNotification(TargetPlayer, $"~b~[Info]~s~ You received an ~y~{GetItemLabel(Name)}~s~ x{Amount} from ~b~{Player.GetName(Source)}~s~.");
                    }
                    else
                    {
                        UI.ShowNotification(Source, $"~b~[Info]~s~ You gave an ~y~{GetItemLabel(Name)}~s~ to ~b~{Player.GetName(TargetPlayer)}~s~.");
                        UI.ShowNotification(TargetPlayer, $"~b~[Info]~s~ You received an ~y~{GetItemLabel(Name)}~s~ from ~b~{Player.GetName(Source)}~s~.");
                    }
                }
                else
                {
                    UI.ShowNotification(Source, $"~y~[Warning]~s~ You tried gave an ~y~{GetItemLabel(Name)}~s~ to ~b~{Player.GetName(TargetPlayer)}~s~, but he can't take more.");
                    UI.ShowNotification(TargetPlayer, $"~y~[Warning]~s~ ~b~{Player.GetName(Source)}~s~ tried to gave you an ~y~{GetItemLabel(Name)}~s~, but you can't take more.");
                }
            }
            else
            {
                UI.ShowNotification(Source, $"~y~[Warning]~s~ You tried gave an ~y~{GetItemLabel(Name)}~s~ to ~b~{Player.GetName(TargetPlayer)}~s~, but he can't carry more things.");
                UI.ShowNotification(TargetPlayer, $"~y~[Warning]~s~ ~b~{Player.GetName(Source)}~s~ tried to gave you an ~y~{GetItemLabel(Name)}~s~, but you can't carry more things.");
            }
            
        }
    }
}