using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using VRC.SDKBase;

namespace DeepCore.Client.Misc
{
    internal class PhotonUtil
    {
        public static void OpRaiseEvent(byte code, object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
        {
            Il2CppSystem.Object customObject2 = SerializationUtil.FromManagedToIL2CPP<Il2CppSystem.Object>(customObject);
            OpRaiseEvent(code, customObject2, RaiseEventOptions, sendOptions);
        }
        public static void OpRaiseEvent(byte code, Il2CppSystem.Object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
        {
            PhotonNetwork.Method_Public_Static_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0(code, customObject, RaiseEventOptions, sendOptions);
        }
        public static void SendChatBoxMessage(string message)
        {
            OpRaiseEvent(43, message, new RaiseEventOptions
            {
                field_Public_EventCaching_0 = 0,
                field_Public_ReceiverGroup_0 = 0
            }, default(SendOptions));
        }
        public static void RaiseTypingIndicator(byte Type)
        {
            OpRaiseEvent(44, Type, new RaiseEventOptions
            {
                field_Public_ReceiverGroup_0 = 0
            }, default(SendOptions));
        }
        internal static byte[] Vector3ToBytes(Vector3 vector3)
        {
            byte[] array = new byte[12];
            Buffer.BlockCopy(BitConverter.GetBytes(vector3.x), 0, array, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(vector3.y), 0, array, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(vector3.z), 0, array, 8, 4);
            return array;
        }
        internal static int ReadActor(byte[] buffer)
        {
            bool flag = buffer == null;
            int num;
            if (flag)
            {
                num = 0;
            }
            else
            {
                bool flag2 = buffer.Length < 4;
                if (flag2)
                {
                    num = 0;
                }
                else
                {
                    num = int.Parse(BitConverter.ToInt32(buffer, 0).ToString().Replace("00001", string.Empty));
                }
            }
            return num;
        }
        public static void CreatePortal(string InstanceID, Vector3 Position, float Rotation)
        {
            bool flag = InstanceID == null;
            if (!flag)
            {
                byte b = 70;
                Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
                dictionary.Add(0, 0);
                dictionary.Add(5, InstanceID);
                dictionary.Add(6, Vector3ToBytes(Position));
                dictionary.Add(7, Rotation);
                OpRaiseEvent(b, dictionary, new RaiseEventOptions(), SendOptions.SendReliable);
            }
        }

        public static void InVRMode(bool VR)
        {
            OpRaiseEvent(44, new Hashtable
            {
                {
                    "inVRMode",
                    VR
                }
            }, new RaiseEventOptions
            {
                field_Public_ReceiverGroup_0 = 0
            }, default(SendOptions));
        }
        public static void RaiseBlock(string userid, bool Block)
        {
            if (userid.StartsWith("usr_"))
            {
                OpRaiseEvent(33, new Hashtable
                {
                    { 3, Block },
                    { 0, 22 },
                    { 1, userid }
                }, new RaiseEventOptions
                {
                    field_Public_ReceiverGroup_0 = 0
                }, default(SendOptions));
                return;
            }
        }
        public static void RaisePortalCreate(string InstanceID, Vector3 Position, float Rotation)
        {
            if (InstanceID == null)
            {
                return;
            }
            byte[] dst = new byte[12];
            Buffer.BlockCopy(BitConverter.GetBytes(Position.x), 0, dst, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(Position.y), 0, dst, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(Position.z), 0, dst, 8, 4);
        }
        public static void RaiseEmojiCreate(int ID)
        {
            OpRaiseEvent(71, new Hashtable
            {
                { 0, 0 },
                { 2, ID }
            }, new RaiseEventOptions
            {
                field_Public_ReceiverGroup_0 = 0
            }, default(SendOptions));
        }

        public static void SendRPC(VRC_EventHandler.VrcEventType EventType, string Name, bool ParameterBool, VRC_EventHandler.VrcBooleanOp BoolOP, GameObject ParamObject, GameObject[] ParamObjects, string ParamString, float Float, int Int, byte[] bytes, VRC_EventHandler.VrcBroadcastType BroadcastType, float Fastforward = 0f)
        {
            VRC_EventHandler.VrcEvent vrcEvent = new VRC_EventHandler.VrcEvent
            {
                EventType = EventType,
                Name = Name,
                ParameterBool = ParameterBool,
                ParameterBoolOp = BoolOP,
                ParameterBytes = bytes,
                ParameterObject = ParamObject,
                ParameterObjects = ParamObjects,
                ParameterString = ParamString,
                ParameterFloat = Float,
                ParameterInt = Int
            };
            Networking.SceneEventHandler.TriggerEvent(vrcEvent, BroadcastType, ParamObject, Fastforward);
        }
    }
}
