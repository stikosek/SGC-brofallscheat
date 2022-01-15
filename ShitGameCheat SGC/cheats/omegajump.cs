using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace ShitGameCheat_SGC.cheats
{
    internal class omegajump
    {

        public static bool activated = false;
        public static int height = 220;
        public static  void update()
        {
            if (!activated)
            {
                NetworkManager.instance.myPS.jumpHeight = NetworkManager.instance.myPS.jumpHeight;
                return;
            }
            NetworkManager.instance.myPS.jumpHeight = height;



        }

    }
}
