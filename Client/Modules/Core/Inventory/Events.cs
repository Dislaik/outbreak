using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Inventory
    {
        private void Events()
        {
            EventHandlers["Inventory:Update"] += new Action(UpdatePlayerInventory);
            EventHandlers["Inventory:UpdateItemsDropedCallback"] += new Action<IDictionary<string, dynamic>, int>(UpdateItemsDropedCallback);
            EventHandlers["Inventory:UpdateItemLootablee"] += new Action<string>(UpdateItemLootablee);

            RegisterNuiCallbackType("Inventory:Close");
            EventHandlers["__cfx_nui:Inventory:Close"] += new Action<IDictionary<string, object>, CallbackDelegate>((Data, CB) =>
            {
                NUI(false);
            });

            RegisterNuiCallbackType("Inventory:Use");
            EventHandlers["__cfx_nui:Inventory:Use"] += new Action<IDictionary<string, object>, CallbackDelegate>((Data, CB) =>
            {
                if (Data.TryGetValue("Ammo", out var WeaponAmmo))
                {
                    CurrentWeapon = Data["Item"].ToString();
                    WeaponPlayerInventory(Data["Item"].ToString(), Convert.ToInt32(WeaponAmmo));
                }
                else
                {
                    TriggerServerEvent($"Inventory:RegisterItem[{Data["Item"]}]", Data["Item"].ToString());
                }

            });

            RegisterNuiCallbackType("Inventory:Give");
            EventHandlers["__cfx_nui:Inventory:Give"] += new Action<IDictionary<string, object>, CallbackDelegate>((Data, CB) =>
            {
                Vector3 PlayerCoords = GetEntityCoords(PlayerPedId(), true);

                foreach (dynamic TargetPlayer in GetActivePlayers())
                {
                    Vector3 PlayerTarget = GetEntityCoords(GetPlayerPed(TargetPlayer), true);
                    float Distance = Vdist(PlayerCoords.X, PlayerCoords.Y, PlayerCoords.Z, PlayerTarget.X, PlayerTarget.Y, PlayerTarget.Z);

                    if (GetPlayerFromServerId(TargetPlayer) != -1 && Distance < 1.5f)
                    {
                        if (Data.TryGetValue("Amount", out var Amount))
                        {
                            if (Convert.ToInt32(Amount) < 1 && !string.IsNullOrEmpty(Amount.ToString()))
                            {
                                UI.ShowNotification("~r~[Error]~s~ Invalid amount to give");
                            }
                            else
                            {

                                if (Enum.IsDefined(typeof(Weapon.Hash), Data["Item"].ToString()))
                                {
                                    TriggerServerEvent("Inventory:PlayerGiveWeapon", GetPlayerServerId(TargetPlayer), Data["Item"].ToString(), Convert.ToInt32(Data["Ammo"]));
                                    SetCurrentPedWeapon(PlayerPedId(), (uint)GetHashKey("weapon_unarmed"), true);
                                }
                                else
                                {
                                    if (Convert.ToInt32(Amount) <= Convert.ToInt32(Data["Limit"]))
                                    {
                                        TriggerServerEvent("Inventory:PlayerGiveItem", GetPlayerServerId(TargetPlayer), Data["Item"].ToString(), Convert.ToInt32(Amount));
                                        SetCurrentPedWeapon(PlayerPedId(), (uint)GetHashKey("weapon_unarmed"), true);
                                    }
                                    else
                                    {
                                        UI.ShowNotification("~r~[Error]~s~ Invalid amount to give");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        UI.ShowNotification("~b~[Info]~s~ No players nearby");
                    }
                }
            });

            RegisterNuiCallbackType("Inventory:Throw");
            EventHandlers["__cfx_nui:Inventory:Throw"] += new Action<IDictionary<string, object>, CallbackDelegate>((Data, CB) =>
            {
                if (Data.TryGetValue("Amount", out var Amount))
                {
                    if (!string.IsNullOrEmpty(Amount.ToString()))
                    {
                        if (Convert.ToInt32(Amount) < 1)
                        {
                            UI.ShowNotification("[Error] Invalid amount to throw");
                        }
                        else
                        {
                            RequestAnimDict("amb@medic@standing@kneel@base");
                            TaskPlayAnim(PlayerPedId(), "amb@medic@standing@kneel@base", "base", 5f, 10f, -1, 1, 0f, false, false, false);

                            if (Data.TryGetValue("Item", out var Weapon) && Data.TryGetValue("Ammo", out var Ammo))
                            {
                                TriggerServerEvent("Inventory:RemoveItem", Weapon.ToString(), 1);
                                CreateItemsDroped(Weapon.ToString(), 1, Convert.ToInt32(Ammo));
                                RemoveWeaponFromPed(PlayerPedId(), (uint)GetHashKey(Weapon.ToString()));
                                SetCurrentPedWeapon(PlayerPedId(), (uint)GetHashKey("weapon_unarmed"), true);
                            }
                            else if (Data.TryGetValue("Item", out var Item) && Data.TryGetValue("Limit", out var Limit))
                            {
                                if (Convert.ToInt32(Amount) > Convert.ToInt32(Limit))
                                {
                                    TriggerServerEvent("Inventory:RemoveItem", Item.ToString(), Convert.ToInt32(Limit));
                                    CreateItemsDroped(Item.ToString(), Convert.ToInt32(Limit), 0);
                                }
                                else
                                {
                                    TriggerServerEvent("Inventory:RemoveItem", Item.ToString(), Convert.ToInt32(Amount));
                                    CreateItemsDroped(Item.ToString(), Convert.ToInt32(Amount), 0);
                                }
                            }
                        }
                    }
                }
            });

            RegisterNuiCallbackType("Loot:Close");
            EventHandlers["__cfx_nui:Loot:Close"] += new Action<IDictionary<string, object>, CallbackDelegate>((Data, CB) =>
            {
                ClearPedTasks(PlayerPedId());
                LootNUI(false);
            });

            RegisterNuiCallbackType("Loot:Lootable");
            EventHandlers["__cfx_nui:Loot:Lootable"] += new Action<IDictionary<string, object>, CallbackDelegate>((Data, CB) =>
            {
                if (Enum.IsDefined(typeof(Weapon.Hash), ItemsDroped[Data["Item"].ToString()].Name))
                {
                    TriggerServerEvent("Inventory:AddLootableWeapons", ItemsDroped[Data["Item"].ToString()].Name, ItemsDroped[Data["Item"].ToString()].Ammo, Data["Item"].ToString());
                }
                else
                {
                    TriggerServerEvent("Inventory:AddLootableItems", ItemsDroped[Data["Item"].ToString()].Name, Data["Item"].ToString());
                }
            });

            Tick += UpdateWeaponAmmo;
        }

        private void WeaponPlayerInventory(string Weapon, int Ammo)
        {

            if (GetSelectedPedWeapon(PlayerPedId()) == GetHashKey(Weapon))
            {
                WeaponEquipped = true;
                LastWeaponEquipped = Weapon;
            }
            else
            {
                WeaponEquipped = false;
            }

            if (!WeaponEquipped || LastWeaponEquipped != Weapon)
            {
                RemoveWeaponFromPed(PlayerPedId(), (uint)GetHashKey(LastWeaponEquipped));
                SetPedAmmo(PlayerPedId(), (uint)GetHashKey(Weapon), 0);
                GiveWeaponToPed(PlayerPedId(), (uint)GetHashKey(Weapon), Convert.ToInt32(Ammo), true, true);
                WeaponEquipped = true;
                LastWeaponEquipped = Weapon;
            }
            else
            {
                SetCurrentPedWeapon(PlayerPedId(), (uint)GetHashKey("WEAPON_UNARMED"), true);
                WeaponEquipped = false;
            }

        }
        private async Task UpdateWeaponAmmo()
        {
            if (IsPedShooting(PlayerPedId()))
            {
                TriggerServerEvent("Inventory:UpdateWeaponAmmo", CurrentWeapon);
            }

            await Task.FromResult(0);
        }
        public static void CreateItemsDroped(string Name, int Amount, int? Ammo)
        {
            Vector3 PlayerCoords = GetEntityCoords(PlayerPedId(), true);
            float PlayerHeading = GetEntityHeading(PlayerPedId());
            int Object = CreateObject(GetHashKey("prop_big_bag_01"), PlayerCoords.X, PlayerCoords.Y, PlayerCoords.Z - 1f, false, false, true);
            SetEntityHeading(Object, PlayerHeading);
            SetEntityCollision(Object, false, false);

            dynamic Drop = new ExpandoObject();
            if (Enum.IsDefined(typeof(Weapon.Hash), Name))
            {
                Drop.Coords = GetEntityCoords(Object, false);
                Drop.Name = Name;
                Drop.Amount = 1;
                Drop.Ammo = Ammo;
            }
            else
            {
                Drop.Coords = GetEntityCoords(Object, false);
                Drop.Name = Name;
                Drop.Amount = Amount;
                Drop.Ammo = 0;
            }

            bool found = false;
            foreach (var i in ItemsDroped.Keys)
            {
                float Distance = Vdist(Drop.Coords.X, Drop.Coords.Y, Drop.Coords.Z, ItemsDroped[i.ToString()].Coords.X, ItemsDroped[i.ToString()].Coords.Y, ItemsDroped[i.ToString()].Coords.Z);

                if (Distance < 1.2f)
                {
                    DeleteObject(ref Object);
                    Drop.Coords = ItemsDroped[i.ToString()].Coords;

                    if (!Enum.IsDefined(typeof(Weapon.Hash), Name))
                    {
                        if (ItemsDroped[i.ToString()].Name == Name)
                        {
                            ItemsDroped[i.ToString()].Amount += Amount;
                            found = true;
                            break;
                        }

                    }
                }

            }
            if (!found)
            {
                ItemsDroped.Add(ItemsDropedID.ToString(), Drop);
                ItemsDropedID += 1;
            }

            TriggerServerEvent("Inventory:UpdateItemsDroped", ItemsDroped, ItemsDropedID);

            LootNUI(true);
        }
        private void UpdateItemLootablee(string ID)
        {
            foreach (var i in ItemsDroped.Keys)
            {
                if (i.ToString() == ID)
                {
                    ItemsDroped[i.ToString()].Amount -= 1;
                    break;
                }
            }
            if (ItemsDroped[ID].Amount < 1)
            {
                ItemsDroped.Remove(ID);
            }

            TriggerServerEvent("Inventory:UpdateItemsDroped", ItemsDroped, ItemsDropedID);

            UpdateLootNUI();
            RemoveObjectDrop();
        }
        private void UpdateItemsDropedCallback(IDictionary<string, dynamic> Items, int NewItemsDropedID)
        {
            ItemsDroped = Items;
            ItemsDropedID = NewItemsDropedID;

            TriggerServerEvent("Inventory:UpdateSerializeItems", GroupingItemsDroped, new Action<string>((Data) =>
            {
                ItemsDropedJSON = Data;
                UpdateLootNUI();
            }));
        }
        public static void CreateItemsDropedServer()
        {
            foreach (var i in ItemsDroped.Keys)
            {
                int Object = CreateObject(GetHashKey("prop_big_bag_01"), ItemsDroped[i.ToString()].Coords.X, ItemsDroped[i.ToString()].Coords.Y, ItemsDroped[i.ToString()].Coords.Z - 0.2f, false, false, true);
                SetEntityCollision(Object, false, false);
            }
        }
        private async Task LootItemsDroped()
        {
            GroupingItemsDroped.Clear();

            foreach (var i in ItemsDroped.Keys)
            {
                Vector3 PlayerCoords = GetEntityCoords(PlayerPedId(), true);
                float Distance = Vdist(PlayerCoords.X, PlayerCoords.Y, PlayerCoords.Z, ItemsDroped[i.ToString()].Coords.X, ItemsDroped[i.ToString()].Coords.Y, ItemsDroped[i.ToString()].Coords.Z);

                if (Config.Debug)
                {
                    Utils.Game.DrawText3D(
                        $"Name: {ItemsDroped[i.ToString()].Name}\n" +
                        $"Amount: {ItemsDroped[i.ToString()].Amount}\n" +
                        $"Ammo: {ItemsDroped[i.ToString()].Ammo}",
                        ItemsDroped[i.ToString()].Coords.X,
                        ItemsDroped[i.ToString()].Coords.Y,
                        ItemsDroped[i.ToString()].Coords.Z
                    );
                    DrawMarker(1, ItemsDroped[i.ToString()].Coords.X, ItemsDroped[i.ToString()].Coords.Y, ItemsDroped[i.ToString()].Coords.Z - 1.2f, 0f, 0f, 0f, 0f, 0f, 0f, 2f, 2f, 10.0f, 104, 255, 80, 40, false, false, 2, false, null, null, false);
                }

                if (Distance < 1.2f)
                {
                    GroupingItemsDroped.Add(i.ToString(), ItemsDroped[i.ToString()]);
                }

            }

            if (Game.IsControlJustPressed(0, Control.ReplayStartStopRecordingSecondary) && GroupingItemsDroped.Count > 0 && !Player.Dead)
            {
                RequestAnimDict("amb@medic@standing@kneel@base");
                TaskPlayAnim(PlayerPedId(), "amb@medic@standing@kneel@base", "base", 5f, 10f, -1, 1, 0f, false, false, false);
                TriggerServerEvent("Inventory:UpdateSerializeItems", GroupingItemsDroped, new Action<string>((Data) =>
                {
                    ItemsDropedJSON = Data;
                    UpdateLootNUI();
                    LootNUI(true);
                }));
            }
            await Task.FromResult(0);
        }
        private void RemoveObjectDrop()
        {
            foreach (var i in GroupingItemsDroped.Keys)
            {
                if (GroupingItemsDroped.Count == 1 && GroupingItemsDroped[i].Amount == 0)
                {
                    Vector3 PlayerCoords = GetEntityCoords(PlayerPedId(), true);
                    int Object = GetClosestObjectOfType(PlayerCoords.X, PlayerCoords.Y, PlayerCoords.Z, 1.2f, (uint)GetHashKey("prop_big_bag_01"), false, false, false);
                    DeleteObject(ref Object);
                    ClearPedTasks(PlayerPedId());
                    LootNUI(false);
                    break;
                }
            }
        }
    }
}
