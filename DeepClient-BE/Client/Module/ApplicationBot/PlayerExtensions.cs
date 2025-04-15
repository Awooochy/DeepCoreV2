using UnityEngine;
using VRC.SDKBase;

namespace DeepClient.Client.Module.ApplicationBot
{
    internal class PlayerExtensions
    {
        public static VRCPlayer LocalVRCPlayer
        {
            get
            {
                return VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0;
            }
        }
        public static GameObject InstantiatePrefab(string PrefabNAME, Vector3 position, Quaternion rotation)
        {
            return Networking.Instantiate(0, PrefabNAME, position, rotation);
        }
    }
}
