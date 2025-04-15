using System;
using System.Collections.Generic;
using System.IO;
using DeepCore.Client.Misc;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace DeepCore.Client.Module.ApplicationBot
{
    internal class Bot
    {
        public const float _moveSpeed = 0.1f;
        public static bool _sitOn = false;
        public static VRCPlayer _target;
        public static Vector3 _originalGravity;
        public static Vector3 _playerLastPos;
        public static byte[] _e7Data;
        public static string _event12Target = "";
        public static Player _event12TargetPlayer = null;
        public static Player _followTargetPlayer = null;
        public static bool _blockEvent12FromSending = false;
        public static Player _orbitTarget;
        public static Action _lastActionOnMainThread;
        public static bool _spinbot = false;
        public static int _spinbotSpeed = 20;
        public static string _PrefabName = "";
        public static float OrbitSpeed = 5f;
        public static float alpha = 0f;
        public static float a = 1f;
        public static float b = 1f;
        public static float Range = 1f;
        public static float Height = 0f;
        public VRCPlayer currentPlayer;
        public Player selectedPlayer;
        private static GameObject _camera
        {
            get
            {
                return ObjectExtensions.GetPlayerCamera;
            }
        }
        private static void MovePlayer(Vector3 pos)
        {
            bool flag = PlayerExtensions.LocalVRCPlayer != null;
            if (flag)
            {
                PlayerExtensions.LocalVRCPlayer.transform.position += pos;
            }
        }
        public static void MakeBot(int Profile, bool gpu, bool auth = false)
        {
            string text = "BotLaunch.exe";
            if (auth)
            {
                text = File.ReadAllText(text);
            }
            string arguments = gpu
                ? string.Format("--profile={0} --no-vr -DCDaddyUwU -Number{1}", Profile, Profile)
                : string.Format("--profile={0} --no-vr -batchmode -DCDaddyUwU -Number{1} -batchmode -noUpm -nographics -disable-gpu-skinning -no-stereo-rendering", Profile, Profile);
            BotAPI.LaunchBot(new string[] { text, arguments });
        }
        public static void ReceiveCommand(string Command)
        {
            int num = Command.IndexOf(" ");
            string CMD = Command.Substring(0, num);
            string Parameters = Command.Substring(num + 1);
            HandleActionOnMainThread(delegate
            {
                Commands[CMD](Parameters);
            });
        }
        private static void HandleActionOnMainThread(Action action)
        {
            _lastActionOnMainThread = action;
        }
        public static void OnUpdate()
        {
            bool isBot = Entry.IsBot;
            if (isBot)
            {
                bool flag = _lastActionOnMainThread != null;
                if (flag)
                {
                    _lastActionOnMainThread();
                    _lastActionOnMainThread = null;
                }
                HandleBotFunctions();
                bool flag2 = _followTargetPlayer != null;
                if (flag2)
                {
                    PlayerUtil.LocalPlayer().transform.position = _followTargetPlayer.transform.position + new Vector3(1f, 0f, 0f);
                }
            }
        }
        public static void HandleBotFunctions()
        {
            bool flag = _orbitTarget != null && PlayerExtensions.LocalVRCPlayer != null;
            if (flag)
            {
                Physics.gravity = new Vector3(0f, 0f, 0f);
                alpha += Time.deltaTime * OrbitSpeed;
                PlayerExtensions.LocalPlayer.transform.position = new Vector3(_orbitTarget.transform.position.x + a * (float)Math.Cos((double)alpha), _orbitTarget.transform.position.y,_orbitTarget.transform.position.z + b * (float)Math.Sin((double)alpha));
            }
            bool flag2 = _spinbot && PlayerExtensions.LocalVRCPlayer != null;
            if (flag2)
            {
                PlayerExtensions.LocalVRCPlayer.transform.Rotate(new Vector3(0f, (float)_spinbotSpeed, 0f));
            }
            bool flag3 = _PrefabName != "";
            if (flag3)
            {
                SpawnPrefab(_PrefabName);
            }
            bool sitOn = _sitOn;
            if (sitOn)
            {
                TeleportToIUser(_target);
            }
        }
        public static void SpawnPrefab(string Name)
        {
            GameObject gameObject = PlayerExtensions.InstantiatePrefab(Name, PlayerExtensions.LocalVRCPlayer.transform.position, new Quaternion(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue));
            gameObject.SetActive(false);
        }
        public static void OnUiManagerInit()
        {
            bool isBot = Entry.IsBot;
            if (isBot)
            {
                SocketConnection.Client();
            }
        }
        public static void StandardSetup(string userId)
        {
            VRCPlayer vrcplayer = PlayerExtensions.GetPlayerByUserID(userId)._vrcplayer;
            bool flag = vrcplayer == null;
            if (!flag)
            {
                _target = vrcplayer;
                SetGravity();
                _sitOn = true;
                TeleportToIUser(vrcplayer);
            }
        }
        public static void SetGravity()
        {
            bool flag = Physics.gravity == Vector3.zero;
            if (!flag)
            {
                _originalGravity = Physics.gravity;
                Physics.gravity = Vector3.zero;
            }
        }
        public static void RemoveSetGravity()
        {
            bool flag = _originalGravity == Vector3.zero;
            if (!flag)
            {
                Physics.gravity = _originalGravity;
            }
        }
        public static void TeleportToIUser(VRCPlayer user)
        {
            try
            {
                Vector3 vector = default(Vector3);
                vector = user.field_Internal_Animator_0.GetBoneTransform((HumanBodyBones)10).position + new Vector3(0f, 0.1f, 0f);
                Transform transform = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform;
                bool flag = _playerLastPos != default(Vector3);
                if (flag)
                {
                    bool flag2 = _playerLastPos != transform.position;
                    if (flag2)
                    {
                        transform.position = vector;
                    }
                }
                _playerLastPos = vector;
            }
            catch
            {
                _target = null;
                RemoveSetGravity();
            }
        }

        private static Dictionary<string, Action<string>> Commands = new Dictionary<string, Action<string>>
        {
            {
                "JoinWorld",
                delegate(string WorldID)
                {
                    DeepConsole.Log("Bot","Joining World " + WorldID);
                    bool flag = RoomManager.field_Internal_Static_ApiWorld_0 != null;
                    if (flag)
                    {
                        Networking.GoToRoom(WorldID);
                    }
                }
            },
            {
                "SetAvatar",
                delegate(string AvatarID)
                {
                    VrcExtensions.ChangeAvatar(AvatarID);
                }
            },
            {
                "SendChatMsg",
                delegate(string msg)
                {
                    PhotonEx.SendChatBoxMessage($"{msg.ToString()}");
                }
            }
        };
    }
}
