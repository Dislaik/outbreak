using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Inventory : BaseScript
    {
        private bool WeaponEquipped { get; set; } = false;
        private string LastWeaponEquipped { get; set; }
        private string CurrentWeapon { get; set; }
        public static int ItemsDropedID { get; set; } = 0;
        private static IDictionary<string, dynamic> GroupingItemsDroped { get; set; } = new Dictionary<string, dynamic>();
        public static string Money { get; set; } = Config.PlayerMoneyStarted;
        private string Items { get; set; }
        public static IDictionary<string, dynamic> ItemsDroped { get; set; } = new Dictionary<string, dynamic>();
        private static string ItemsDropedJSON { set; get; }


        public Inventory()
        { Events();

            TriggerServerEvent("Player:GetInventory", new Action<string>((Data) =>
            {
                Items = Data;
            }));

            Tick += Toggle;
            Tick += LootItemsDroped;
        }

        private async Task Toggle()
        {
            if (Game.IsControlJustReleased(0, Control.ReplayStartStopRecordingSecondary) && !Player.Dead)
            {
                NUI(true);
            }

            await Task.FromResult(0);
        }

        private void NUI(bool Display)
        {
            string Display_;
            if (Display) { 
                Display_ = "true";
                AnimpostfxPlay("SwitchHUDIn", 0, true);
            }else { 
                Display_ = "false";
                AnimpostfxStop("SwitchHUDIn");
            }

            string JSON = "" +
                "{" +
                    $"\"Type\": \"Inventory\"," +
                    $"\"Display\": {Display_}," +
                    $"\"Items\": {Items}" +
                "}" +
            "";

            SendNuiMessage(JSON);
            SetNuiFocus(Display, Display);
        }

        private void UpdatePlayerInventory()
        {
            TriggerServerEvent("Player:GetInventory", new Action<string>((Data) =>
            {
                Items = Data;
                UpdatePlayerNUI();
            }));
        }
        private void UpdatePlayerNUI()
        {
            string JSON = "" +
                "{" +
                    $"\"Type\": \"UpdateInventory\"," +
                    $"\"Items\": {Items}" +
                "}" +
            "";

            SendNuiMessage(JSON);
        }

        public static void QuickSlots(bool Display)
        {
            string Display_;
            if (Display) { Display_ = "true"; }
            else { Display_ = "false"; }

            string JSON = "" +
                "{" +
                    $"\"Type\": \"QuickSlots\"," +
                    $"\"Display\": {Display_}" +
                "}" +
            "";

            SendNuiMessage(JSON);
        }
        private static void LootNUI(bool Display)
        {
            string Display_;
            if (Display) { Display_ = "true"; }
            else { Display_ = "false"; }

            string JSON = "" +
                "{" +
                    $"\"Type\": \"Loot\"," +
                    $"\"Display\": {Display_}" +
                "}" +
            "";

            SendNuiMessage(JSON);
        }

        private static void UpdateLootNUI()
        {
            string JSON = "" +
                "{" +
                    $"\"Type\": \"UpdateLoot\"," +
                    $"\"Items\": {ItemsDropedJSON}" +
                "}" +
            "";

            SendNuiMessage(JSON);
        }

    }
}
