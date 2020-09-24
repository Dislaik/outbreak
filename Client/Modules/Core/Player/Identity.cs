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
        private Menu MainMenu = null;
        private Menu SkinMenu = null;
        private string PlayerFirstName = null;
        private string PlayerLastName = null;
        private string PlayerSex = "Male";
        private int PlayerAge = 18;
        private bool LockKey = true;
        private bool EntryTextIsOpen = true;
        private int Skin_ = 0;
        private int Face_ = 0;
        private int Hair_ = 0;
        private int HairColor_ = 0;
        private int Eyes_ = 0;
        private int Eyebrows_ = 0;
        private int Beard_ = 0;
        private int Cam = 1;
        private Dictionary<string, string> PlayerSkinDictionary = new Dictionary<string, string>();

        public Identity()
        {
            EventHandlers["playerSpawned"] += new Action(InitPlayerRegister);
            EventHandlers["Outbreak.Core.Player:PlayerRegister"] += new Action(MenuIdentity);
            EventHandlers["Outbreak.Core.Player:PlayerAlreadyRegistered"] += new Action<string, int, int, int, int, int, int, int>(Skin.SetPlayerModels);
        }
        private void InitPlayerRegister()
        {
            TriggerServerEvent("Outbreak.Core.Player:InitPlayerRegister");
        }
        private void MenuIdentity()
        {
            MenuSkin();
            SkinMenu.ClosedMenu();

            Skin.SetModelToPlayer("mp_m_freemode_01");
            Skin.DefaultCharacterComponents("Male");

            CreateCamera();

            Vector3 PlayerCoords = GetEntityCoords(PlayerPedId(), false);
            SetCamCoord(Cam, PlayerCoords.X - 2f, PlayerCoords.Y, PlayerCoords.Z);

            MainMenu = new Menu("Identity", "Character creation")
            {
                TitleFont = 2,
                CanExit = false
            }; MainMenu.Register(MainMenu);
            MainMenu.AddItem("First Name", "First name of your character");
            MainMenu.AddItem("Last Name", "Last name of your character");
            List<string> Age = new List<string>();
            for (int i = 18; i < 60; i++)
            {
                Age.Add(i.ToString());
            } MainMenu.AddItemList("Age:", "Age of your character", Age);
            List<string> Sex = new List<string>
            {
                "Male",
                "Female"
            }; MainMenu.AddItemList("Sex:", "Gender of your character", Sex);
            MainMenu.AddItem("Edit Character", "Edit your fucking character");
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
                else if (name == "Edit Character")
                {
                    MainMenu.ClosedMenu();
                    SkinMenu.OpenMenu();
                    SetCamCoord(Cam, PlayerCoords.X - 1f, PlayerCoords.Y, PlayerCoords.Z + 0.3f);
                }
                else if (name == "Finish")
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
                        PlayerSkinDictionary.Add("Skin", Skin_.ToString());
                        PlayerSkinDictionary.Add("Face", Face_.ToString());
                        PlayerSkinDictionary.Add("Hair", Hair_.ToString());
                        PlayerSkinDictionary.Add("HairColor", HairColor_.ToString());
                        PlayerSkinDictionary.Add("Eyes", Eyes_.ToString());
                        PlayerSkinDictionary.Add("Eyebrows", Eyebrows_.ToString());
                        PlayerSkinDictionary.Add("Beard", Beard_.ToString());
                        string PlayerSkin = Utils.String.DictionaryToString(PlayerSkinDictionary);

                        LockKey = false;
                        Skin.PlayerLoaded = true;
                        TriggerServerEvent("Outbreak.Core.Player:SetPlayerRegistered", PlayerFirstName+" "+PlayerLastName, PlayerAge, PlayerSex, PlayerSkin);
                        Screen.ShowNotification("~b~[INFO]~w~ Player register completed!");
                        MainMenu.ClosedMenu();
                        DeleteCam();
                    }
                }
            };

            MainMenu.OnItemListSelectSides += (name, index, namelist, indexlist) =>
            {
                if (index == 3)
                {
                    PlayerAge = Convert.ToInt32(namelist);
                }
                else if (index == 4)
                {
                    if (indexlist == 1)
                    {
                        if (PlayerSex != "Male")
                        {
                            Skin.SetPlayerModels("Male", Skin_, Face_, Hair_, HairColor_, Eyes_, Eyebrows_, Beard_);

                            PlayerSex = "Male";
                        }
                    }
                    else if (indexlist == 2)
                    {
                        if (PlayerSex != "Female")
                        {
                            Skin.SetPlayerModels("Female", Skin_, Face_, Hair_, HairColor_, Eyes_, Eyebrows_, Beard_);

                            PlayerSex = "Female";
                        }
                    }
                }
            };

            Tick += async () =>
            {
                MainMenu.Initiation();

                if (LockKey)
                {
                    LockKeys(true);
                }

                MainMenu.ListOfItems[1] = $"First Name: {PlayerFirstName}";
                MainMenu.ListOfItems[2] = $"Last Name: {PlayerLastName}";

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
        public void CreateCamera()
        {
            if (!DoesCamExist(Cam))
            {
                Cam = CreateCam("DEFAULT_SCRIPTED_CAMERA", true);
            }
            SetCamActive(Cam, true);
            RenderScriptCams(true, true, 500, true, true);
            SetCamRot(Cam, 0f, 0f, -90f, 2);
        }
        public void DeleteCam()
        {
            SetCamActive(Cam, false);
            RenderScriptCams(false, true, 500, true, true);
        }
        public void MenuSkin()
        {
            Vector3 PlayerCoords = GetEntityCoords(PlayerPedId(), false);

            SkinMenu = new Menu("Identity", "Character creation")
            {
                TitleFont = 2,
                CanExit = false
            }; SkinMenu.Register(SkinMenu);
            List<string> Skins = new List<string>();
            for (int i = 0; i <= 44; i++)
            {
                Skins.Add(i.ToString());
            }
            SkinMenu.AddItemList("Skin", "Change the Skin of your character", Skins);
            List<string> Faces = new List<string>();
            for (int i = 0; i <= 45; i++)
            {
                Faces.Add(i.ToString());
            } SkinMenu.AddItemList("Face", "Change the face of your character", Faces);
            List<string> Hairs = new List<string>();
            for (int i = 0; i <= 74; i++)
            {
                if (i != 23 && i != 24)
                {
                    Hairs.Add(i.ToString());
                }
            } SkinMenu.AddItemList("Hair", "Change the hair of your character", Hairs);
            List<string> HairsColor = new List<string>();
            for (int i = 0; i <= 55; i++)
            {
                HairsColor.Add(i.ToString());
            } SkinMenu.AddItemList("Hair Color", "Change the hair color of your character", HairsColor);
            List<string> Eyes = new List<string>();
            for (int i = 0; i <= 31; i++)
            {
                Eyes.Add(i.ToString());
            }
            SkinMenu.AddItemList("Eyes", "Change the eyes of your character", Eyes);
            List<string> Eyebrows = new List<string>(); 
            for (int i = 0; i <= 33; i++)
            {
                Eyebrows.Add(i.ToString());
            } SkinMenu.AddItemList("Eyebrows", "Change the eyesbrows of your character", Eyebrows);
            List<string> Beards = new List<string>();
            for (int i = 0; i <= 28; i++)
            {
                Beards.Add(i.ToString());
            }
            SkinMenu.AddItemList("Beard", "Change the beard of your character", Beards);
            SkinMenu.AddItem("Back", "Back to Identity Menu");

            SkinMenu.OnItemSelect += (name, index) =>
            {
                if (name == "Back")
                {
                    SkinMenu.ResetSelect();
                    SkinMenu.ClosedMenu();
                    MainMenu.OpenMenu();
                    SetCamCoord(Cam, PlayerCoords.X - 2f, PlayerCoords.Y, PlayerCoords.Z);
                }
            };

            SkinMenu.OnItemListSelectSides += (name, index, namelist, indexlist) =>
            {
                if(name == "Skin")
                {
                    Skin_ = Convert.ToInt32(namelist);
                    SetPedHeadBlendData(PlayerPedId(), Face_, 0, 0, Convert.ToInt32(namelist), 0, 0, 0f, 0f, 0f, false);

                    SetCamCoord(Cam, PlayerCoords.X - 1f, PlayerCoords.Y, PlayerCoords.Z + 0.3f);
                }
                else if (name == "Face")
                {
                    Face_ = Convert.ToInt32(namelist);
                    SetPedHeadBlendData(PlayerPedId(), Convert.ToInt32(namelist), 0, 0, Skin_, 0, 0, 0f, 0f, 0f, false);
                    
                    SetCamCoord(Cam, PlayerCoords.X - 0.5f, PlayerCoords.Y, PlayerCoords.Z + 0.7f);
                }
                else if (name == "Hair")
                {
                    Hair_ = Convert.ToInt32(namelist);
                    SetPedComponentVariation(PlayerPedId(), 2, Convert.ToInt32(namelist), 0, 0);

                    SetCamCoord(Cam, PlayerCoords.X - 0.5f, PlayerCoords.Y, PlayerCoords.Z + 0.7f);
                }
                else if (name == "Hair Color")
                {
                    HairColor_ = Convert.ToInt32(namelist);
                    SetPedHairColor(PlayerPedId(), Convert.ToInt32(namelist), 0);
                    SetPedHeadOverlayColor(PlayerPedId(), 2, 1, Convert.ToInt32(namelist), 0);
                    SetPedHeadOverlayColor(PlayerPedId(), 1, 1, Convert.ToInt32(namelist), 0);

                    SetCamCoord(Cam, PlayerCoords.X - 0.5f, PlayerCoords.Y, PlayerCoords.Z + 0.7f);
                }
                else if (name == "Eyes")
                {
                    Eyes_ = Convert.ToInt32(namelist);
                    SetPedEyeColor(PlayerPedId(), Convert.ToInt32(namelist));

                    SetCamCoord(Cam, PlayerCoords.X - 0.4f, PlayerCoords.Y, PlayerCoords.Z + 0.7f);
                }
                else if (name == "Eyebrows")
                {
                    Eyebrows_ = Convert.ToInt32(namelist);
                    SetPedHeadOverlay(PlayerPedId(), 2, Convert.ToInt32(namelist), 1f);

                    SetCamCoord(Cam, PlayerCoords.X - 0.4f, PlayerCoords.Y, PlayerCoords.Z + 0.7f);
                }
                else if (name == "Beard")
                {
                    Beard_ = Convert.ToInt32(namelist);
                    if (Beard_ == 0)
                    {
                        SetPedHeadOverlay(PlayerPedId(), 1, Convert.ToInt32(namelist), 0f);
                    }
                    else { SetPedHeadOverlay(PlayerPedId(), 1, Convert.ToInt32(namelist), 1f); }
                    

                    SetCamCoord(Cam, PlayerCoords.X - 0.4f, PlayerCoords.Y, PlayerCoords.Z + 0.7f);
                }
            };

            Tick += async () =>
            {
                SkinMenu.Initiation();

                await Task.FromResult(0);
            };
        }
    }
}