using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattooStudio.data;
using static CitizenFX.Core.Native.API;
using static TattooStudio.Common;

namespace TattooStudio
{
    public class Tattoos
    {
        private Menu menu;
        public static bool DontCloseMenus { get { return MenuController.PreventExitingMenu; } set { MenuController.PreventExitingMenu = value; } }
        public static bool DisableBackButton { get { return MenuController.DisableBackButton; } set { MenuController.DisableBackButton = value; } }
        public static MultiplayerPedData currentCharacter = new MultiplayerPedData();


        


        public Tattoos()
        {

        }

        public void populateMenu()
        {
            List<string> headTattoosList = new List<string>();
            List<string> torsoTattoosList = new List<string>();
            List<string> leftArmTattoosList = new List<string>();
            List<string> rightArmTattoosList = new List<string>();
            List<string> leftLegTattoosList = new List<string>();
            List<string> rightLegTattoosList = new List<string>();
            List<string> badgeTattoosList = new List<string>();

            TattoosData.GenerateTattoosData();
            if (currentCharacter.IsMale)
            {
                int counter = 1;
                foreach (var tattoo in MaleTattoosCollection.HEAD)
                {
                    headTattoosList.Add($"Tattoo #{counter} (of {MaleTattoosCollection.HEAD.Count})");
                    counter++;
                }
                counter = 1;
                foreach (var tattoo in MaleTattoosCollection.TORSO)
                {
                    torsoTattoosList.Add($"Tattoo #{counter} (of {MaleTattoosCollection.TORSO.Count})");
                    counter++;
                }
                counter = 1;
                foreach (var tattoo in MaleTattoosCollection.LEFT_ARM)
                {
                    leftArmTattoosList.Add($"Tattoo #{counter} (of {MaleTattoosCollection.LEFT_ARM.Count})");
                    counter++;
                }
                counter = 1;
                foreach (var tattoo in MaleTattoosCollection.RIGHT_ARM)
                {
                    rightArmTattoosList.Add($"Tattoo #{counter} (of {MaleTattoosCollection.RIGHT_ARM.Count})");
                    counter++;
                }
                counter = 1;
                foreach (var tattoo in MaleTattoosCollection.LEFT_LEG)
                {
                    leftLegTattoosList.Add($"Tattoo #{counter} (of {MaleTattoosCollection.LEFT_LEG.Count})");
                    counter++;
                }
                counter = 1;
                foreach (var tattoo in MaleTattoosCollection.RIGHT_LEG)
                {
                    rightLegTattoosList.Add($"Tattoo #{counter} (of {MaleTattoosCollection.RIGHT_LEG.Count})");
                    counter++;
                }
                counter = 1;
                foreach (var tattoo in MaleTattoosCollection.BADGES)
                {
                    badgeTattoosList.Add($"Badge #{counter} (of {MaleTattoosCollection.BADGES.Count})");
                    counter++;
                }
            }
            else
            {
                int counter = 1;
                foreach (var tattoo in FemaleTattoosCollection.HEAD)
                {
                    headTattoosList.Add($"Tattoo #{counter} (of {FemaleTattoosCollection.HEAD.Count})");
                    counter++;
                }
                counter = 1;
                foreach (var tattoo in FemaleTattoosCollection.TORSO)
                {
                    torsoTattoosList.Add($"Tattoo #{counter} (of {FemaleTattoosCollection.TORSO.Count})");
                    counter++;
                }
                counter = 1;
                foreach (var tattoo in FemaleTattoosCollection.LEFT_ARM)
                {
                    leftArmTattoosList.Add($"Tattoo #{counter} (of {FemaleTattoosCollection.LEFT_ARM.Count})");
                    counter++;
                }
                counter = 1;
                foreach (var tattoo in FemaleTattoosCollection.RIGHT_ARM)
                {
                    rightArmTattoosList.Add($"Tattoo #{counter} (of {FemaleTattoosCollection.RIGHT_ARM.Count})");
                    counter++;
                }
                counter = 1;
                foreach (var tattoo in FemaleTattoosCollection.LEFT_LEG)
                {
                    leftLegTattoosList.Add($"Tattoo #{counter} (of {FemaleTattoosCollection.LEFT_LEG.Count})");
                    counter++;
                }
                counter = 1;
                foreach (var tattoo in FemaleTattoosCollection.RIGHT_LEG)
                {
                    rightLegTattoosList.Add($"Tattoo #{counter} (of {FemaleTattoosCollection.RIGHT_LEG.Count})");
                    counter++;
                }
                counter = 1;
                foreach (var tattoo in FemaleTattoosCollection.BADGES)
                {
                    badgeTattoosList.Add($"Badge #{counter} (of {FemaleTattoosCollection.BADGES.Count})");
                    counter++;
                }
            }


            const string tatDesc = "Rotiere durch die Tattooliste. Wenn dir eins gefällt, dann drücke Enter um es hinzuzufügen. Wenn    du es bereits hast wird es entfernt.";
            MenuListItem headTatts = new MenuListItem("Kopf Tattoos", headTattoosList, 0, tatDesc);
            MenuListItem torsoTatts = new MenuListItem("Torso Tattoos", torsoTattoosList, 0, tatDesc);
            MenuListItem leftArmTatts = new MenuListItem("Linker Arm Tattoos", leftArmTattoosList, 0, tatDesc);
            MenuListItem rightArmTatts = new MenuListItem("Rechter Arm Tattoos", rightArmTattoosList, 0, tatDesc);
            MenuListItem leftLegTatts = new MenuListItem("Linkes Bein Tattoos", leftLegTattoosList, 0, tatDesc);
            MenuListItem rightLegTatts = new MenuListItem("Rechtes Bein Tattoos", rightLegTattoosList, 0, tatDesc);
            MenuListItem badgeTatts = new MenuListItem("Overlay Sticker", badgeTattoosList, 0, tatDesc);

            menu.AddMenuItem(headTatts);
            menu.AddMenuItem(torsoTatts);
            menu.AddMenuItem(leftArmTatts);
            menu.AddMenuItem(rightArmTatts);
            menu.AddMenuItem(leftLegTatts);
            menu.AddMenuItem(rightLegTatts);
            menu.AddMenuItem(badgeTatts);
        }

        public static void CreateListsIfNull()
        {
            if (currentCharacter.PedTatttoos.HeadTattoos == null)
            {
                currentCharacter.PedTatttoos.HeadTattoos = new List<KeyValuePair<string, string>>();
            }
            if (currentCharacter.PedTatttoos.TorsoTattoos == null)
            {
                currentCharacter.PedTatttoos.TorsoTattoos = new List<KeyValuePair<string, string>>();
            }
            if (currentCharacter.PedTatttoos.LeftArmTattoos == null)
            {
                currentCharacter.PedTatttoos.LeftArmTattoos = new List<KeyValuePair<string, string>>();
            }
            if (currentCharacter.PedTatttoos.RightArmTattoos == null)
            {
                currentCharacter.PedTatttoos.RightArmTattoos = new List<KeyValuePair<string, string>>();
            }
            if (currentCharacter.PedTatttoos.LeftLegTattoos == null)
            {
                currentCharacter.PedTatttoos.LeftLegTattoos = new List<KeyValuePair<string, string>>();
            }
            if (currentCharacter.PedTatttoos.RightLegTattoos == null)
            {
                currentCharacter.PedTatttoos.RightLegTattoos = new List<KeyValuePair<string, string>>();
            }
            if (currentCharacter.PedTatttoos.BadgeTattoos == null)
            {
                currentCharacter.PedTatttoos.BadgeTattoos = new List<KeyValuePair<string, string>>();
            }
        }

        public static void ApplySavedTattoos()
        {
            // remove all decorations, and then manually re-add them all. what a retarded way of doing this R*....
            ClearPedDecorations(Game.PlayerPed.Handle);

            foreach (var tattoo in currentCharacter.PedTatttoos.HeadTattoos)
            {
                SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tattoo.Key), (uint)GetHashKey(tattoo.Value));
            }
            foreach (var tattoo in currentCharacter.PedTatttoos.TorsoTattoos)
            {
                SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tattoo.Key), (uint)GetHashKey(tattoo.Value));
            }
            foreach (var tattoo in currentCharacter.PedTatttoos.LeftArmTattoos)
            {
                SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tattoo.Key), (uint)GetHashKey(tattoo.Value));
            }
            foreach (var tattoo in currentCharacter.PedTatttoos.RightArmTattoos)
            {
                SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tattoo.Key), (uint)GetHashKey(tattoo.Value));
            }
            foreach (var tattoo in currentCharacter.PedTatttoos.LeftLegTattoos)
            {
                SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tattoo.Key), (uint)GetHashKey(tattoo.Value));
            }
            foreach (var tattoo in currentCharacter.PedTatttoos.RightLegTattoos)
            {
                SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tattoo.Key), (uint)GetHashKey(tattoo.Value));
            }
            foreach (var tattoo in currentCharacter.PedTatttoos.BadgeTattoos)
            {
                SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tattoo.Key), (uint)GetHashKey(tattoo.Value));
            }

        }

        public async void CreateMenu()
        {
            menu = new Menu("Tattoostudio", "Tattoos");

            while(TattoosData.tattoos == null)
            {
                await BaseScript.Delay(1);
            }

            populateMenu();

            MenuItem save = new MenuItem("Bezahlen", "Das wird dich 5000€ kosten");
            MenuItem abort = new MenuItem("~r~Abbrechen");
            menu.AddMenuItem(save);
            menu.AddMenuItem(abort);

            

            menu.OnIndexChange += (sender, oldItem, newItem, oldIndex, newIndex) =>
            {
                CreateListsIfNull();
                ApplySavedTattoos();
            };


            menu.OnListIndexChange += (sender, item, oldIndex, tattooIndex, menuIndex) =>
            {
                CreateListsIfNull();
                ApplySavedTattoos();
                if (menuIndex == 0) // head
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.HEAD.ElementAt(tattooIndex) : FemaleTattoosCollection.HEAD.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (!currentCharacter.PedTatttoos.HeadTattoos.Contains(tat))
                    {
                        SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tat.Key), (uint)GetHashKey(tat.Value));
                    }
                }
                else if (menuIndex == 1) // torso
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.TORSO.ElementAt(tattooIndex) : FemaleTattoosCollection.TORSO.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (!currentCharacter.PedTatttoos.TorsoTattoos.Contains(tat))
                    {
                        SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tat.Key), (uint)GetHashKey(tat.Value));
                    }
                }
                else if (menuIndex == 2) // left arm
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.LEFT_ARM.ElementAt(tattooIndex) : FemaleTattoosCollection.LEFT_ARM.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (!currentCharacter.PedTatttoos.LeftArmTattoos.Contains(tat))
                    {
                        SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tat.Key), (uint)GetHashKey(tat.Value));
                    }
                }
                else if (menuIndex == 3) // right arm
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.RIGHT_ARM.ElementAt(tattooIndex) : FemaleTattoosCollection.RIGHT_ARM.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (!currentCharacter.PedTatttoos.RightArmTattoos.Contains(tat))
                    {
                        SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tat.Key), (uint)GetHashKey(tat.Value));
                    }
                }
                else if (menuIndex == 4) // left leg
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.LEFT_LEG.ElementAt(tattooIndex) : FemaleTattoosCollection.LEFT_LEG.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (!currentCharacter.PedTatttoos.LeftLegTattoos.Contains(tat))
                    {
                        SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tat.Key), (uint)GetHashKey(tat.Value));
                    }
                }
                else if (menuIndex == 5) // right leg
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.RIGHT_LEG.ElementAt(tattooIndex) : FemaleTattoosCollection.RIGHT_LEG.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (!currentCharacter.PedTatttoos.RightLegTattoos.Contains(tat))
                    {
                        SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tat.Key), (uint)GetHashKey(tat.Value));
                    }
                }
                else if (menuIndex == 6) // badges
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.BADGES.ElementAt(tattooIndex) : FemaleTattoosCollection.BADGES.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (!currentCharacter.PedTatttoos.BadgeTattoos.Contains(tat))
                    {
                        SetPedDecoration(Game.PlayerPed.Handle, (uint)GetHashKey(tat.Key), (uint)GetHashKey(tat.Value));
                    }
                }
            };

            menu.OnListItemSelect += (sender, item, tattooIndex, menuIndex) =>
            {
                CreateListsIfNull();

                if (menuIndex == 0) // head
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.HEAD.ElementAt(tattooIndex) : FemaleTattoosCollection.HEAD.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (currentCharacter.PedTatttoos.HeadTattoos.Contains(tat))
                    {
                        Subtitle.Custom($"Tattoo #{tattooIndex + 1} wurde ~r~entfernt~s~.");
                        currentCharacter.PedTatttoos.HeadTattoos.Remove(tat);
                    }
                    else
                    {
                        Subtitle.Custom($"Tattoo #{tattooIndex + 1} wurde ~g~hinzugefügt~s~.");
                        currentCharacter.PedTatttoos.HeadTattoos.Add(tat);
                    }
                }
                else if (menuIndex == 1) // torso
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.TORSO.ElementAt(tattooIndex) : FemaleTattoosCollection.TORSO.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (currentCharacter.PedTatttoos.TorsoTattoos.Contains(tat))
                    {
                        Subtitle.Custom($"Tattoo #{tattooIndex + 1} wurde ~r~entfernt~s~.");
                        currentCharacter.PedTatttoos.TorsoTattoos.Remove(tat);
                    }
                    else
                    {
                        Subtitle.Custom($"Tattoo #{tattooIndex + 1} wurde ~g~hinzugefügt~s~.");
                        currentCharacter.PedTatttoos.TorsoTattoos.Add(tat);
                    }
                }
                else if (menuIndex == 2) // left arm
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.LEFT_ARM.ElementAt(tattooIndex) : FemaleTattoosCollection.LEFT_ARM.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (currentCharacter.PedTatttoos.LeftArmTattoos.Contains(tat))
                    {
                        Subtitle.Custom($"Tattoo #{tattooIndex + 1} wurde ~r~entfernt~s~.");
                        currentCharacter.PedTatttoos.LeftArmTattoos.Remove(tat);
                    }
                    else
                    {
                        Subtitle.Custom($"Tattoo #{tattooIndex + 1} wurde ~g~hinzugefügt~s~.");
                        currentCharacter.PedTatttoos.LeftArmTattoos.Add(tat);
                    }
                }
                else if (menuIndex == 3) // right arm
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.RIGHT_ARM.ElementAt(tattooIndex) : FemaleTattoosCollection.RIGHT_ARM.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (currentCharacter.PedTatttoos.RightArmTattoos.Contains(tat))
                    {
                        Subtitle.Custom($"Tattoo #{tattooIndex + 1} wurde ~r~entfernt~s~.");
                        currentCharacter.PedTatttoos.RightArmTattoos.Remove(tat);
                    }
                    else
                    {
                        Subtitle.Custom($"Tattoo #{tattooIndex + 1} wurde ~g~hinzugefügt~s~.");
                        currentCharacter.PedTatttoos.RightArmTattoos.Add(tat);
                    }
                }
                else if (menuIndex == 4) // left leg
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.LEFT_LEG.ElementAt(tattooIndex) : FemaleTattoosCollection.LEFT_LEG.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (currentCharacter.PedTatttoos.LeftLegTattoos.Contains(tat))
                    {
                        Subtitle.Custom($"Tattoo #{tattooIndex + 1} wurde ~r~entfernt~s~.");
                        currentCharacter.PedTatttoos.LeftLegTattoos.Remove(tat);
                    }
                    else
                    {
                        Subtitle.Custom($"Tattoo #{tattooIndex + 1} wurde ~g~hinzugefügt~s~.");
                        currentCharacter.PedTatttoos.LeftLegTattoos.Add(tat);
                    }
                }
                else if (menuIndex == 5) // right leg
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.RIGHT_LEG.ElementAt(tattooIndex) : FemaleTattoosCollection.RIGHT_LEG.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (currentCharacter.PedTatttoos.RightLegTattoos.Contains(tat))
                    {
                        Subtitle.Custom($"Tattoo #{tattooIndex + 1} wurde ~r~entfernt~s~.");
                        currentCharacter.PedTatttoos.RightLegTattoos.Remove(tat);
                    }
                    else
                    {
                        Subtitle.Custom($"Tattoo #{tattooIndex + 1} wurde ~g~hinzugefügt~s~.");
                        currentCharacter.PedTatttoos.RightLegTattoos.Add(tat);
                    }
                }
                else if (menuIndex == 6) // badges
                {
                    var Tattoo = currentCharacter.IsMale ? MaleTattoosCollection.BADGES.ElementAt(tattooIndex) : FemaleTattoosCollection.BADGES.ElementAt(tattooIndex);
                    KeyValuePair<string, string> tat = new KeyValuePair<string, string>(Tattoo.collectionName, Tattoo.name);
                    if (currentCharacter.PedTatttoos.BadgeTattoos.Contains(tat))
                    {
                        Subtitle.Custom($"Badge #{tattooIndex + 1} wurde ~r~entfernt~s~.");
                        currentCharacter.PedTatttoos.BadgeTattoos.Remove(tat);
                    }
                    else
                    {
                        Subtitle.Custom($"Badge #{tattooIndex + 1} wurde ~g~hinzugefügt~s~.");
                        currentCharacter.PedTatttoos.BadgeTattoos.Add(tat);
                    }
                }

                ApplySavedTattoos();

            };

            menu.OnItemSelect += async (sender, item, index) =>
            {
                if(item == save)
                {
                    BaseScript.TriggerServerEvent("tattoo:setData", false);
                    Main.CommitData = Newtonsoft.Json.JsonConvert.SerializeObject(currentCharacter);
                    menu.GoBack();
                    DisableBackButton = false;
                    DontCloseMenus = false;
                    FreezeEntityPosition(Game.PlayerPed.Handle, false);
                }
                else if(item == abort)
                {
                    Main.LoadTattoosFromStorage();
                    menu.GoBack();
                    DisableBackButton = false;
                    DontCloseMenus = false;
                    FreezeEntityPosition(Game.PlayerPed.Handle, false);
                }

            };

        }



        public struct PedTattoos
        {
            public List<KeyValuePair<string, string>> TorsoTattoos;
            public List<KeyValuePair<string, string>> HeadTattoos;
            public List<KeyValuePair<string, string>> LeftArmTattoos;
            public List<KeyValuePair<string, string>> RightArmTattoos;
            public List<KeyValuePair<string, string>> LeftLegTattoos;
            public List<KeyValuePair<string, string>> RightLegTattoos;
            public List<KeyValuePair<string, string>> BadgeTattoos;
        }

        public struct MultiplayerPedData
        {
            public PedTattoos PedTatttoos;
            public bool IsMale;
        }

        public Menu GetMenu()
        {
            if (menu == null)
            {
                CreateMenu();
            }
            return menu;
        }
    }

}

