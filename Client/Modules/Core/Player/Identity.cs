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
        private string SexChoice = null;
        private bool LockKey = true;
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

            Menu MainMenu = new Menu("Identity", "Choice the sex of you character")
            {
                TitleFont = 2,
                CanExit = false
            };

            MainMenu.Register(MainMenu);
            MainMenu.AddItem("Male", "This is a real men.");
            MainMenu.AddItem("Female", "This is a real woman.");
            MainMenu.AddItem("Finish", "Done motherfucker!");
            
            MainMenu.OnItemSelect += (name, index) =>
            {
                if (index == 1)
                {
                    Skin.SetModelToPlayer("mp_m_freemode_01");
                    Skin.DefaultCharacterComponents("Male");

                    SexChoice = "Male";
                }
                else if (index == 2)
                {
                    Skin.SetModelToPlayer("mp_f_freemode_01");
                    Skin.DefaultCharacterComponents("Female");

                    SexChoice = "Female";
                }
                else if (index == 3)
                {

                    if (string.IsNullOrEmpty(SexChoice))
                    {
                        Screen.ShowNotification("~r~[ERROR]~w~ Select a gender to continue.");
                    }
                    else
                    {
                        LockKey = false;
                        TriggerServerEvent("Outbreak.Core.Player:SetPlayerGender", SexChoice);
                        MainMenu.ClosedMenu();
                        Screen.ShowNotification("~b~[INFO]~w~ Player register complete!");
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
                await Task.FromResult(0);
            };
        }

        private void LockKeys(bool Disable)
        {
            DisableControlAction(0, 1, Disable);
            DisableControlAction(0, 2, Disable);
            DisableControlAction(0, 24, Disable);
            DisableControlAction(0, 257, Disable);
            DisableControlAction(0, 25, Disable);
            DisableControlAction(0, 263, Disable);
            DisableControlAction(0, 32, Disable);
            DisableControlAction(0, 34, Disable);
            DisableControlAction(0, 31, Disable);
            DisableControlAction(0, 30, Disable);

            DisableControlAction(0, 45, Disable);
            DisableControlAction(0, 22, Disable);
            DisableControlAction(0, 44, Disable);
            DisableControlAction(0, 37, Disable);
            DisableControlAction(0, 23, Disable);
        }
    }
}
