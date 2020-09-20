using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core.Player
{
    public class Identity : BaseScript
    {
        private string PlayerFirstName { get; set; } = null;
        private string PlayerLastName { get; set; } = null;
        private string PlayerSex = null;
        private string PlayerAge = null;
        private bool LockKey = true;
        private bool EntryTextIsOpen { get; set; } = true;

        public Identity()
        {
            EventHandlers["playerSpawned"] += new Action(InitPlayerRegister);
            EventHandlers["Outbreak.Core.Player:PlayerRegister"] += new Action(MenuIdentity);
            EventHandlers["Outbreak.Core.Player:PlayerAlreadyRegistered"] += new Action<string>(Skin.SetPlayerModels);
        }
        private void InitPlayerRegister()
        {
            TriggerServerEvent("Outbreak.Core.Player:InitPlayerRegister");
        }
        private void MenuIdentity()
        {

            Menu MainMenu = new Menu("Identity", "Character creation")
            {
                TitleFont = 2,
                CanExit = false
            };
            MainMenu.Register(MainMenu);

            MainMenu.AddItem("First Name:", "First name of your character");
            MainMenu.AddItem("Last Name:", "Last name of your character");

            List<string> Age = new List<string>();
            for (int i = 18; i < 60; i++)
            {
                Age.Add(i.ToString());
            }
            MainMenu.AddItemList("Age:", "Age of your character", Age);

            List<string> Sex = new List<string>
            {
                "Male",
                "Female"
            };
            MainMenu.AddItemList("Sex:", "Gender of your character", Sex);

            MainMenu.AddItem("Finish", "Done motherfucker!");
            
            MainMenu.OnItemSelect += (name, index) =>
            {
                if (index == 1)
                {
                    if (EntryTextIsOpen)
                    {
                        EntryTextIsOpen = false;
                        EntryTextFirstName();
                    }
                    else { EntryTextIsOpen = true; }

                }
                else if (index == 2)
                {
                    if (EntryTextIsOpen)
                    {
                        EntryTextIsOpen = false;
                        EntryTextLastName();
                    }
                    else { EntryTextIsOpen = true; }
                }
                else if (index == 5)
                {
                    if (string.IsNullOrEmpty(PlayerFirstName) || string.IsNullOrEmpty(PlayerLastName))
                    {
                        Screen.ShowNotification("~r~[ERROR]~w~ Enter character name to continue");
                    }
                    else if (PlayerFirstName.Contains(" ") || PlayerLastName.Contains(" "))
                    {
                        Screen.ShowNotification("~r~[ERROR]~w~ Remove spaces in your name to continue");
                    }
                    /*else if (char.IsUpper(char.Parse(PlayerFirstName.First().ToString().ToUpper())) || char.IsUpper(char.Parse(PlayerLastName.First().ToString().ToUpper())))
                    {
                        Screen.ShowNotification("~r~[ERROR]~w~ Change capital initials to continue");
                    }*/
                    else if (string.IsNullOrEmpty(PlayerSex))
                    {
                        Screen.ShowNotification("~r~[ERROR]~w~ Select a gender to continue");
                    }
                    else
                    {
                        LockKey = false;
                        TriggerServerEvent("Outbreak.Core.Player:SetPlayerGender", PlayerFirstName + " "+ PlayerLastName, PlayerAge, PlayerSex);
                        MainMenu.ClosedMenu();
                        Screen.ShowNotification("~b~[INFO]~w~ Player register completed!");
                    }
                }
            };

            MainMenu.OnItemListSelectSides += (name, index, namelist, indexlist) =>
            {
                for (int i = 0; i <= Age.Count; i++)
                {
                    if (index == 3 && indexlist == i)
                    {
                        PlayerAge = namelist;
                    }
                }

                if (index == 4)
                {
                    if (indexlist == 1)
                    {
                        if (PlayerSex != "Male")
                        {
                            Skin.SetModelToPlayer("mp_m_freemode_01");
                            Skin.DefaultCharacterComponents("Male");

                            PlayerSex = "Male";
                        }
                    }
                    else if (indexlist == 2)
                    {
                        if (PlayerSex != "Female")
                        {
                            Skin.SetModelToPlayer("mp_f_freemode_01");
                            Skin.DefaultCharacterComponents("Female");

                            PlayerSex = "Female";
                        }
                    }
                }
            };

            Tick += async () =>
            {
                MainMenu.Initiation();
                if (MainMenu.IsAnyMenuOpen())
                {
                    LockKeys(LockKey);
                }

                if (!string.IsNullOrEmpty(PlayerFirstName))
                {
                    MainMenu.ListOfItems[1] = $"First Name: {PlayerFirstName}";
                }

                if (!string.IsNullOrEmpty(PlayerLastName))
                {
                    MainMenu.ListOfItems[2] = $"Last Name: {PlayerLastName}";
                }


                await Task.FromResult(0);
            };

        }

        private void LockKeys(bool Disable)
        {
            for (int i = 0; i <= 357; i++)
            {
                DisableControlAction(0, i, Disable);
            }
        }

        public async void EntryTextFirstName()
        {
            AddTextEntry("FMMC_KEY_TIP1", "First Name");
            DisplayOnscreenKeyboard(1, "FMMC_KEY_TIP1", "", "", "", "", "", 10);

            while (UpdateOnscreenKeyboard() == 0)
            {
                DisableAllControlActions(0);
                await Delay(0);
            }
            if (UpdateOnscreenKeyboard() == 1)
            {
                PlayerFirstName = GetOnscreenKeyboardResult();
            }
        }

        public async void EntryTextLastName()
        {
            AddTextEntry("FMMC_KEY_TIP1", "Last Name");
            DisplayOnscreenKeyboard(1, "FMMC_KEY_TIP1", "", "", "", "", "", 10);

            while (UpdateOnscreenKeyboard() == 0)
            {
                DisableAllControlActions(0);
                await Delay(0);
            }
            if (UpdateOnscreenKeyboard() == 1)
            {
                PlayerLastName = GetOnscreenKeyboardResult();
            }
        }
    }
}