using ReMod.Core.VRChat;
using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Movement
{
    internal class SeatOnHead
    {
        public static Vector3 normalGravity = Physics.gravity;
        public static bool IsEnable = false;
        public static Player TargetPlayer;
        public static void State(bool s)
        {
            if (s)
            {
                TargetPlayer = PlayerExtensions.GetVRCPlayer()._player;
                IsEnable = true;
            }
            else
            {
                IsEnable = false;
                TargetPlayer = null;
            }
        }
        public static void Update()
        {
            try
            {
                if (IsEnable)
                {
                    if (TargetPlayer != null)
                    {
                        var playerhead = TargetPlayer.prop_VRCPlayer_0.field_Internal_Animator_0.GetBoneTransform(HumanBodyBones.Head);

                        if (playerhead != null) Player.prop_Player_0.transform.position = playerhead.position;

                        Physics.gravity = Vector3.zero;
                    }
                }
                else
                {
                    Physics.gravity = normalGravity;
                }
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    IsEnable = false;
                }
            }
            catch { }
        }
    }
}
