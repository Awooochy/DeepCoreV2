/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepCore.Client.API;
using RootMotion.FinalIK;
using UnityEngine;

namespace DeepCore.Client.Misc
{
    internal class AvatarToModele
    {
        internal static List<GameObject> AllClones = new List<GameObject>();

        internal static GameObject Clone(this global::VRC.Player player, bool Mul = true)
        {
            GameObject Clone = GameObject.Instantiate<GameObject>(player._vrcplayer.field_Private_VRCAvatarManager_0.prop_GameObject_0, null, true);
            Animator component = Clone.GetComponent<Animator>();
            bool flag = component != null && component.isHuman;
            if (flag)
            {
                component.GetBoneTransform(HumanBodyBones.Head).localScale = Vector3.one;
            }
            Clone.name = "Cloned Avatar";
            component.enabled = false;
            Clone.GetComponent<VRIK>().enabled = false;
            Clone.transform.position = player.transform.position;
            Clone.transform.rotation = player.transform.rotation;
            List<SkinnedMeshRenderer> transforms = AviUtils.FindAllComponentsInGameObject<SkinnedMeshRenderer>(Clone, true, false, true);
            foreach (SkinnedMeshRenderer rend in transforms)
            {
                rend.gameObject.layer = 0;
            }
            if (Mul)
            {
                AllClones.Add(Clone);
            }
            GameObject.DontDestroyOnLoad(Clone);
            return Clone;
        }
    }
}
*/