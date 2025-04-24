using System.Collections.Generic;
using UnityEngine;
using VRC.SDKBase;
using RootMotion.FinalIK;

namespace DeepCore.Client.Misc
{
    internal static class AvatarExtensions
    {
        internal static List<GameObject> AllClones = new List<GameObject>();

        internal static GameObject CloneAvatar(this VRC.Player player, bool makePickupable = true)
        {
            if (player == null || player._vrcplayer?.field_Private_VRCAvatarManager_0?.prop_GameObject_0 == null)
                return null;

            // Create a wrapper object for the pickup
            GameObject wrapper = new GameObject($"[PickupWrapper] {player.field_Private_APIUser_0.displayName}'s Avatar");
            wrapper.transform.position = player.transform.position + Vector3.up * 0.5f;
            wrapper.transform.rotation = player.transform.rotation;

            // Create the avatar clone as child of wrapper
            GameObject avatarClone = GameObject.Instantiate(
                player._vrcplayer.field_Private_VRCAvatarManager_0.prop_GameObject_0,
                wrapper.transform,
                false
            );

            // Disable animator and IK to freeze in T-pose
            var animator = avatarClone.GetComponent<Animator>();
            if (animator != null && animator.isHuman)
            {
                animator.enabled = false;
                var head = animator.GetBoneTransform(HumanBodyBones.Head);
                if (head != null) head.localScale = Vector3.one;
            }

            var vrik = avatarClone.GetComponent<VRIK>();
            if (vrik != null) vrik.enabled = false;

            // Make all renderers visible
            foreach (var renderer in avatarClone.GetComponentsInChildren<Renderer>(true))
            {
                renderer.gameObject.layer = 0; // Default layer
            }

            // Add pickup components to wrapper
            if (makePickupable)
            {
                MakePickupable(wrapper, avatarClone);
            }

            // Track and preserve the clone
            AllClones.Add(wrapper);
            GameObject.DontDestroyOnLoad(wrapper);
            
            return wrapper;
        }

        private static void MakePickupable(GameObject wrapper, GameObject avatarClone)
        {
            if (wrapper == null || avatarClone == null) return;

            // Add rigidbody to wrapper
            var rb = wrapper.AddComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

            // Add collider to wrapper (simple capsule)
            var collider = wrapper.AddComponent<CapsuleCollider>();
            collider.height = 2f;
            collider.radius = 0.3f;
            collider.center = Vector3.up * 1f;

            // Add VRC_Pickup component to wrapper
            var pickup = wrapper.AddComponent<VRC_Pickup>();
    
            // Configure pickup settings
            pickup.pickupable = true;
            pickup.allowManipulationWhenEquipped = true;
            pickup.AutoHold = VRC_Pickup.AutoHoldMode.No;
            pickup.orientation = VRC_Pickup.PickupOrientation.Grip;
            pickup.InteractionText = "Avatar Clone";
            pickup.UseText = "Hold Avatar";
    
            // Create and assign grip transform
            GameObject grip = new GameObject("Grip");
            grip.transform.SetParent(wrapper.transform);
            grip.transform.localPosition = Vector3.zero;
            grip.transform.localRotation = Quaternion.identity;
            pickup.ExactGun = grip.transform;
            pickup.ExactGrip = grip.transform;

            // Set ownership using PlayerUtil
            var localVRCPlayer = PlayerUtil.GetLocalVRCPlayer();
            if (localVRCPlayer != null)
            {
                var localPlayerApi = localVRCPlayer.field_Private_VRCPlayerApi_0;
                if (localPlayerApi != null)
                {
                    Networking.SetOwner(localPlayerApi, wrapper);
                }
            }

            // Add script to handle physics after spawn
            wrapper.AddComponent<PickupInitializer>();
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

        // Helper class for initialization
        private class PickupInitializer : MonoBehaviour
        {
            private float spawnTime;
            private Rigidbody rb;

            void Start()
            {
                spawnTime = Time.time;
                rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true; // Start kinematic to prevent falling through floor
                }
            }

            void Update()
            {
                // After 1 second, enable physics
                if (Time.time > spawnTime + 1f && rb != null && rb.isKinematic)
                {
                    rb.isKinematic = false;
                    // Give a slight upward force to ensure it's above ground
                    rb.AddForce(Vector3.up * 2f, ForceMode.VelocityChange);
                    Destroy(this); // Remove this component after initialization
                }
            }
        }
    }
}