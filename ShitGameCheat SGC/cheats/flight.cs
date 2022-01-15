using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.IL2CPP;
using UnityEngine;
using HarmonyLib;
using UnityEngine.InputSystem;


namespace ShitGameCheat_SGC.cheats
{

    internal class flight
    {

        public static bool activated = false;

        public static void update()
        {
            if (!activated)
                return;


            

            NetworkManager.instance.myPS.rb.velocity = new Vector3(0f, 0f, 0f);
            float speed = 0.05f;
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                NetworkManager.instance.myPS.rb.transform.position = new Vector3(NetworkManager.instance.myPS.rb.transform.position.x, NetworkManager.instance.myPS.rb.transform.position.y + speed, NetworkManager.instance.myPS.rb.transform.position.z);
            }
            Vector3 playerTransformPosVec = NetworkManager.instance.myPS.rb.transform.position;
            if (Keyboard.current.wKey.wasPressedThisFrame)
            {
                NetworkManager.instance.myPS.rb.transform.position = new Vector3(playerTransformPosVec.x + Camera.main.transform.forward.x * Camera.main.transform.up.y * speed, playerTransformPosVec.y + Camera.main.transform.forward.y * speed, playerTransformPosVec.z + Camera.main.transform.forward.z * Camera.main.transform.up.y * speed);
            }
            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                NetworkManager.instance.myPS.rb.position = new Vector3(playerTransformPosVec.x - Camera.main.transform.forward.x * Camera.main.transform.up.y * speed, playerTransformPosVec.y - Camera.main.transform.forward.y * speed, playerTransformPosVec.z - Camera.main.transform.forward.z * Camera.main.transform.up.y * speed);
            }
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                NetworkManager.instance.myPS.rb.position = new Vector3(playerTransformPosVec.x + Camera.main.transform.right.x * speed, playerTransformPosVec.y, playerTransformPosVec.z + Camera.main.transform.right.z * speed);
            }
            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                NetworkManager.instance.myPS.rb.position = new Vector3(playerTransformPosVec.x - Camera.main.transform.right.x * speed, playerTransformPosVec.y, playerTransformPosVec.z - Camera.main.transform.right.z * speed);
            }


        }
    }
}
