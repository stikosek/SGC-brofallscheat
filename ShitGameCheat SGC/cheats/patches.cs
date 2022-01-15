using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using UnityEngine.InputSystem;
using HarmonyLib;

namespace ShitGameCheat_SGC.cheats
{
    [HarmonyPatch]
    public static class Patches
    {
        [HarmonyPatch(typeof(PlayerSetup), "Update")]
        [HarmonyPrefix]
        public static void Update()
        {
           
            

            speed.update();
            flight.update();
            omegajump.update();

            
        }


        [HarmonyPatch(typeof(PlayerSetup), "HitPlayer")]
        [HarmonyPrefix]
        public static bool Prefix()
        {



            return !godmode.activated;


        }


        [HarmonyPatch(typeof(SIS.DBManager), "IsPurchased")]
        [HarmonyPrefix]
        public static bool Patch(string productID, ref bool __result)
        {
            if (freepremium.activated && productID.Equals("4000"))
            {
                __result = true;
                return false;
            }

            return true;
        }






    }
}
