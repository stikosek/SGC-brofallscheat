using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx;
using BepInEx.IL2CPP;
using UnityEngine;
using HarmonyLib;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

namespace ShitGameCheat_SGC.gui
{


    public class GuiObject : MonoBehaviour
    {
        //GameObject shit
        public GuiObject(IntPtr ptr) : base(ptr) { }
        private Plugin loader;

        // WINDOW RECTS
        public Rect PlayerRect = new Rect(20, 70, 200, 360);
        public Rect PlayerRectAct = new Rect(230, 70, 200, 320);
        public Rect welscreenrect = new Rect(Screen.width / 2 - 160, Screen.height / 2 - 100, 320, 200);
        public Vector2 scrollPosition = Vector2.zero;

        // Utility vairables.
        float AddMoneyAmount = 1000000f;
        float AddXpAmount = 69;
        float AddCrownsAmount = 69;
        bool showplayeractions = false;
        bool welscreen = true;
        public static bool showmenu = true;


        public static void CreateInstance(Plugin loader)
        {
            //Create GameObject
            GuiObject obj = loader.AddComponent<GuiObject>();

            obj.loader = loader;

            //Prevent Unity from deleting when a new Scene loads.
            DontDestroyOnLoad(obj.gameObject);
            obj.hideFlags |= HideFlags.HideAndDontSave;
            
        }

        public void OnGUI()
        {

            


            if (showmenu)
            {
                PlayerRect = GUI.Window(0, PlayerRect, (GUI.WindowFunction)DrawPlayer, "ShitGameCheat (SGC)");
                if (showplayeractions)
                {
                    PlayerRectAct = GUI.Window(1, PlayerRectAct, (GUI.WindowFunction)DrawPlayerAct, "Player actions");
                }
            }
            
            if (welscreen)
            {
                welscreenrect = GUI.Window(2, welscreenrect, (GUI.WindowFunction)DrawWelcome, "Welcome to the SGC modmenu!");
            }

            if (Keyboard.current.rightShiftKey.wasReleasedThisFrame)
            {
                gui.GuiObject.showmenu = !gui.GuiObject.showmenu;
            }
        }

        public void DrawPlayerAct(int windowID)
        {
            int count = 0;
            scrollPosition = GUI.BeginScrollView(new Rect(0, 20, 200, 290), scrollPosition, new Rect(0, 0, 190, 700));
            foreach (PlayerSetup playersetup in NetworkManager.instance.allPlayer)
            {
                GUI.Label(new Rect(5, 0 + count*20, 190, 20), playersetup.clothManager.playerName);
                if( GUI.Button(new Rect(140, 0 + count * 20, 50, 20), "Kill"))
                {
                    playersetup.KillPlayer(KillType.TNT, new Vector3(0, 6900, 0), null);
                }
                if (GUI.Button(new Rect(110, 0 + count * 20, 30, 20), "Tp"))
                {
                    NetworkManager.instance.myPS.rb.transform.position = playersetup.rb.transform.position;
                }

                count++;
            }
            GUI.EndScrollView();
            GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
        }

        public void DrawWelcome(int windowID)
        {
            GUI.Label(new Rect(5, 20, 310, 20), "Hello! Welcome to the SGC modmenu. This menu");
            GUI.Label(new Rect(5, 40, 310, 20), " was created in the competetion with me");
            GUI.Label(new Rect(5, 60, 310, 20), "(stikosek) and JNNJ! I hope this cheat is good");
            GUI.Label(new Rect(5, 80, 310, 20), "The mod menu is always open. deal with it");

            if(GUI.Button(new Rect(5, 180, 310, 20), "Press to close this menu."))
            {
                welscreen = false;
            }

            GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));

        }


        public void DrawPlayer(int windowID)
        {
            //utils.guiutils.DrawGui.DrawWindowBackground(Color.green, Color.gray, PlayerRect, 5, "");
            cheats.speed.activated = GUI.Toggle(new Rect(5, 20, 190, 20), cheats.speed.activated, "Speed");
            cheats.flight.activated = GUI.Toggle(new Rect(5, 40, 190, 20), cheats.flight.activated, "Flight");
            cheats.godmode.activated = GUI.Toggle(new Rect(5, 60, 190, 20), cheats.godmode.activated, "GodMode");
            cheats.omegajump.activated = GUI.Toggle(new Rect(5, 80, 190, 20), cheats.omegajump.activated, "OmegaJump");
            cheats.freepremium.activated = GUI.Toggle(new Rect(5, 100, 190, 20), cheats.freepremium.activated, "Free premium");
            if (GUI.Button(new Rect(5, 120, 190, 20), "Instant Finish"))
            {
                NetworkManager.instance.myPS.Finish();
            }
            if (GUI.Button(new Rect(5, 140, 190, 20), "Show/hide player actions"))
            {
                showplayeractions = !showplayeractions;
            }


            // INFO

            GUI.Label(new Rect(5, 165, 190, 20), "Game/lobby info.");
            if(NetworkManager.instance != null)
            {
                GUI.Label(new Rect(5, 185, 190, 20), "All players: " + NetworkManager.instance.allPlayer.Count.ToString());
                int num = 0;
                foreach (PlayerSetup playersetup in NetworkManager.instance.allPlayer)
                {
                    if (playersetup.isBot == true)
                    {
                        num++;
                    }
                }

                GUI.Label(new Rect(5, 205, 190, 20), "From that bots: " + num.ToString());

            }
            else
            {
                GUI.Label(new Rect(5, 185, 190, 20), "You need to be ingame");

                GUI.Label(new Rect(5, 205, 190, 20), "for this to show.");
            }


            // XP

            if (GUI.Button(new Rect(5, 220, 190, 20), "Give " + AddXpAmount.ToString() + " levels."))
            {
                MoneyPanelScript.AddLevel((int)AddXpAmount, 0);
            }

            AddXpAmount = GUI.HorizontalSlider(new Rect(5, 240, 320, 10), AddXpAmount, 1f, 1000f);

            // CROWNS

            if (GUI.Button(new Rect(5, 260, 190, 20), "Give " + AddCrownsAmount.ToString() + " crowns."))
            {
                MoneyPanelScript.AddCrowns((int)AddMoneyAmount, 0, true);
            }

            AddCrownsAmount = GUI.HorizontalSlider(new Rect(5, 280, 320, 10),AddCrownsAmount, 1f, 1000f);

            // MONEY

            if (GUI.Button(new Rect(5, 300, 190, 20),"Give " + AddMoneyAmount.ToString() + " coins."))
            {
                MoneyPanelScript.AddMoney((int)AddMoneyAmount, 0, true);
            }

            AddMoneyAmount = GUI.HorizontalSlider(new Rect(5, 320, 320, 10), AddMoneyAmount, 1f, 1000f);

            GUI.Label(new Rect(5, 340, 200, 20), "By stikosek#0761");

            GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));

        }

    }
}
