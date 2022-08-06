using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TattooStudio.data;
using static CitizenFX.Core.Native.API;
using static TattooStudio.Tattoos;

namespace TattooStudio
{
    public class Main : BaseScript
    {
        #region Variables
        private bool firstTick = true;
        public static dynamic ESX = null;
        public static Control MenuToggleKey { get { return MenuController.MenuToggleKey; } private set { MenuController.MenuToggleKey = value; } } // M by Default
        public static Menu Menu { get; private set; }
        public static Tattoos TattooMenu { get; private set; }
        private List<Vector3> blips = new List<Vector3>() {
            new Vector3(1322.645F,-1651.976F,52.275F),
            new Vector3(-1153.676F,-1425.68F,4.954F),
            new Vector3(322.139F,180.467F,103.587F),
            new Vector3(-3170.071F,1075.059F,20.829F),
            new Vector3(1864.633F,3747.738F,33.032F),
            new Vector3(-293.713F,6200.04F,31.487F)
        };
        public static string CommitData = null;

        #endregion


        public Main()
        {
            MenuToggleKey = (Control) (-2);
            RegisterNuiCallbackType("postTattooData");

            EventHandlers["esx:playerLoaded"] += new Action<dynamic>(OnLoad);
            EventHandlers["esx:onPlayerSpawn"] += new Action<dynamic>(OnLoad);
            EventHandlers["tattoo:apply"] += new Action<bool>(OnApply);
            EventHandlers["__cfx_nui:postTattooData"] += new Action<IDictionary<string, object>, CallbackDelegate>(ApplyData);


            TriggerEvent("esx:getSharedObject", new object[]
            {
                new Action<dynamic>(esx =>
                {
                    ESX = esx;
                })
            });
        }

        private void OnApply(bool success)
        {
            if(CommitData == null)
            {
                SendNuiMessage(Newtonsoft.Json.JsonConvert.SerializeObject(new NUICommand("clear")));
            } else
            {
                SendNuiMessage(Newtonsoft.Json.JsonConvert.SerializeObject(new NUICommand("save", CommitData)));
            }
        }

        private async void OnLoad(dynamic obj)
        {
            await Delay(1000);
            LoadTattoosFromStorage();
            Tick += OnTick;
        }

        public static void LoadTattoosFromStorage()
        {
            SendNuiMessage(Newtonsoft.Json.JsonConvert.SerializeObject(new NUICommand("load")));
        }

        private async void ApplyData(IDictionary<string, object> nuidata, CallbackDelegate cb)
        {
            // Retrieve JSONObj and cast to string
            if (!nuidata.TryGetValue("data", out object tattooJSONObj))
            {
                cb(new{});

                return;
            }
            var tattooJSON = (tattooJSONObj as string) ?? "";

            // Wait until Playermodel is correctly loaded
            while (true)
            {
                if (Game.PlayerPed.Model.Equals(PedHash.FreemodeMale01) == true || Game.PlayerPed.Model.Equals(PedHash.FreemodeFemale01) == true)
                {
                    break;
                }
                await Delay(0);
            }

            if (tattooJSON.Length == 0)
            {
                ESX.TriggerServerCallback("esx_skin:getPlayerSkin", new Action<dynamic>((skin) => {
                    MultiplayerPedData data = new MultiplayerPedData();
                    if (skin.sex == 0)
                    {
                        data.IsMale = true;
                    }else
                    {
                        data.IsMale = false;
                    }
                    currentCharacter = data;
                    CreateListsIfNull();
                    ApplySavedTattoos();
                }));
            }
            else
            {
                currentCharacter = Newtonsoft.Json.JsonConvert.DeserializeObject<MultiplayerPedData>(tattooJSON);
                CreateListsIfNull();
                ApplySavedTattoos();
            }
            cb(new { });
        }

        private async Task OnTick()
        {
            if (firstTick)
            {
                firstTick = false;

                while (Game.Player.Name == "**Invalid**" || Game.Player.Name == "** Invalid **")
                {
                    await Delay(0);
                }

                foreach(Vector3 vector in blips)
                {
                    Blip myBlip = World.CreateBlip(vector);
                    myBlip.Sprite = BlipSprite.TattooParlor;
                    myBlip.Color = BlipColor.Red;
                    myBlip.Scale = 0.8F;
                    SetBlipAsShortRange(myBlip.Handle, true);
                    BeginTextCommandSetBlipName("STRING");
                    AddTextComponentString("Tattoostudio");
                    EndTextCommandSetBlipName(myBlip.Handle);
                }


                string obj = LoadResourceFile(GetCurrentResourceName(), "overlays.json");
                TattoosData.tattoos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tattoo>>(obj);

                // Add Main Menu to Pool
                Menu = new Menu("Tattoostudio");

                MenuController.AddMenu(Menu);
                MenuController.MainMenu = Menu;
                MenuController.MenuAlignment = MenuController.MenuAlignmentOption.Left;


                TattooMenu = new Tattoos();
                Menu menu2 = TattooMenu.GetMenu();
                MenuItem button2 = new MenuItem("Tattoos", "Lass dir ein Tattoo stechen.")
                {
                    Label = "→→→"
                };
                AddMenu(Menu, menu2, button2);

                MenuItem delete = new MenuItem("~r~Entferne alle Tattoos", "Laser dir alles Tattoos weg: 5000€");
                Menu.AddMenuItem(delete);

                Menu.OnItemSelect += async (sender, item, index) =>
                {
                    if (item == button2)
                    {
                        DisableBackButton = true;
                        DontCloseMenus = true;
                        FreezeEntityPosition(Game.PlayerPed.Handle, true);
                    }
                    else if(item == delete)
                    {
                        CommitData = null;
                        TriggerServerEvent("tattoo:setData", true);
                    }
                };

            }
            else
            {
                while (ESX == null)
                {
                    await Delay(1000);
                }
                foreach (Vector3 vector in blips)
                {
                    if (World.GetDistance(Game.PlayerPed.Position, vector) < 20F)
                    {
                        DrawMarker(27, vector.X, vector.Y, vector.Z - 0.9F, 0F, 0F, 0F, 0F, 0F, 0F, 3F, 3F, 1F, 255, 0, 0, 200, false, false, 2, false, null, null, false);
                    }   
                    if(World.GetDistance(Game.PlayerPed.Position, vector) < 1.5F)
                    {
                        AddTextEntry("tattooHelp", "Drücke ~INPUT_CONTEXT~ um auf das Tattoostudio zuzugreifen.");
                        DisplayHelpTextThisFrame("tattooHelp", false);
                        if (Game.IsControlJustPressed(1, Control.Context))
                        {
                            Menu.OpenMenu();
                        }
                    }
                }
            }
            
            
        }


        private void AddMenu(Menu parentMenu, Menu submenu, MenuItem menuButton)
        {
            parentMenu.AddMenuItem(menuButton);
            MenuController.AddSubmenu(parentMenu, submenu);
            MenuController.BindMenuItem(parentMenu, submenu, menuButton);
            submenu.RefreshIndex();
        }

        private void UpdatePresence()
        {
            SetDiscordAppId("636185216238616578");
            SetDiscordRichPresenceAsset("logo_big");
            SetDiscordRichPresenceAssetText("http://immortal-roleplay.one");
        }
    }
}
