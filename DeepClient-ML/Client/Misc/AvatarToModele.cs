using System.Collections.Generic;
using UnityEngine;
using VRC.SDKBase;
using RootMotion.FinalIK;

namespace DeepCore.Client.Misc
{
    internal static class AvatarExtensions // Changed to static class
    {
        internal static List<GameObject> AllClones = new List<GameObject>();

        internal static GameObject CloneAvatar(this VRC.Player player, bool makePickupable = true)  // Renamed to avoid conflict with potential existing methods
        {
            if (player == null || player._vrcplayer?.field_Private_VRCAvatarManager_0?.prop_GameObject_0 == null)
                return null;

            // Create the clone
            GameObject clone = GameObject.Instantiate(
                player._vrcplayer.field_Private_VRCAvatarManager_0.prop_GameObject_0, 
                player.transform.position, 
                player.transform.rotation
            );
            
            clone.name = $"[Clone] {player.field_Private_APIUser_0.displayName}'s Avatar";

            // Disable animator and IK to freeze in T-pose
            var animator = clone.GetComponent<Animator>();
            if (animator != null && animator.isHuman)
            {
                animator.enabled = false;
                // Reset head scale if needed
                var head = animator.GetBoneTransform(HumanBodyBones.Head);
                if (head != null) head.localScale = Vector3.one;
            }

            var vrik = clone.GetComponent<VRIK>();
            if (vrik != null) vrik.enabled = false;

            // Make all renderers visible
            foreach (var renderer in clone.GetComponentsInChildren<Renderer>(true))
            {
                renderer.gameObject.layer = 0; // Default layer
            }

            // Add pickup component if requested
            if (makePickupable)
            {
                MakePickupable(clone);
            }

            // Track and preserve the clone
            AllClones.Add(clone);
            GameObject.DontDestroyOnLoad(clone);
            
            return clone;
        }

        private static void MakePickupable(GameObject obj)
        {
            if (obj == null) return;

            // Add rigidbody for physics
            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null) rb = obj.AddComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;

            // Add colliders if none exist
            if (obj.GetComponentInChildren<Collider>(true) == null)
            {
                foreach (var renderer in obj.GetComponentsInChildren<Renderer>(true))
                {
                    var collider = renderer.gameObject.AddComponent<MeshCollider>();
                    collider.convex = true;
                }
            }

            // Add VRC_Pickup component
            var pickup = obj.GetComponent<VRC_Pickup>();
            if (pickup == null) pickup = obj.AddComponent<VRC_Pickup>();
            
            // Configure pickup settings
            pickup.pickupable = true;
            pickup.allowManipulationWhenEquipped = true;
            pickup.AutoHold = VRC_Pickup.AutoHoldMode.No;
            pickup.orientation = VRC_Pickup.PickupOrientation.Any;
            pickup.InteractionText = "Avatar Clone";
            pickup.UseText = "Hold Avatar";
        }

        public static void CleanupAllClones()
        {
            foreach (var clone in AllClones)
            {
                if (clone != null) 
                    GameObject.Destroy(clone);
            }
            AllClones.Clear();
        }
    }
}