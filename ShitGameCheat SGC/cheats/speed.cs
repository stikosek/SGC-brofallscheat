using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace ShitGameCheat_SGC.cheats
{

    internal class speed
    {

        public static bool activated = false;
        int old = 0;

        public static void update()
        {
            if (!activated)
            {
                NetworkManager.instance.myPS.speed = 7;
                return;
            }
               

            



            NetworkManager.instance.myPS.speed = 20;
           


        }
    }
}
