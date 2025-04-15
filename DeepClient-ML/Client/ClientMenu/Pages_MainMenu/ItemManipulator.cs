using System;
using System.Linq;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
        internal class ItemManipulator
        {
            private static bool checkIfBlacklisted(VRC_Pickup item)
            {
                return blacklistedPickups.Contains(item.name);
            }
        internal static readonly System.Collections.Generic.List<string> blacklistedPickups = new System.Collections.Generic.List<string>
        {
            "OscDebugConsole",
            "MirrorPickup",
            "PhotoCamera",
            "ViewFinder",
            "AvatarDebugConsole"
        };
        private static void setActiveAll()
            {
                foreach (VRC_Pickup vrc_Pickup in ItemManipulator._pickups)
                {
                    if (!checkIfBlacklisted(vrc_Pickup))
                    {
                        setActive(vrc_Pickup);
                    }
                }
            }
            private static void setInactive(VRC_Pickup item)
            {
                item.gameObject.SetActive(false);
            }
            private static void setInactiveAll()
            {
                foreach (VRC_Pickup vrc_Pickup in ItemManipulator._pickups)
                {
                    if (!ItemManipulator.checkIfBlacklisted(vrc_Pickup))
                    {
                        ItemManipulator.setInactive(vrc_Pickup);
                    }
                }
            }
            internal static void ManipulatorHacksMenu(UiManager uiManager)
            {
                ItemManipulator._uiManager = uiManager;
                ItemManipulator._itemGrabberMenu = ItemManipulator._uiManager.QMMenu.AddCategoryPage("Manipulator", "Manipulator");
                ItemManipulator._itemGrabberMenu.AddCategory("Manipulator Options");
                ItemManipulator._itemGrabber = ItemManipulator._itemGrabberMenu.GetCategory("Manipulator Options");
                ItemManipulator._itemGrabber.AddButton("Select Force Object ON (Local)", "", delegate
                {
                    ItemManipulator.Select("Active");
                }, null);
                ItemManipulator._itemGrabber.AddButton("Select Force Object OFF (Local)", "", delegate
                {
                    ItemManipulator.Select("Inactive");
                }, null);
                ItemManipulator._itemGrabber.AddButton("Select Bring Pickup", "", delegate
                {
                    ItemManipulator.Select("BringItem");
                }, null);
                ItemManipulator._itemGrabber.AddButton("Select Udon", "", delegate
                {
                    ItemManipulator.Select("UdonSelect");
                }, null);
                ItemManipulator._itemGrabber.AddButton("Freeze Item (max: " + 4.ToString() + ")", "Freeze Item", delegate
                {
                    ItemManipulator.Select("Freeze");
                }, null);
                ItemManipulator._itemGrabberMenu.AddCategory("Item Selector List");
                ItemManipulator._itemGrabberSelector = ItemManipulator._itemGrabberMenu.GetCategory("Item Selector List");
                ItemManipulator._itemGrabberOptionsMenu = ItemManipulator._itemGrabberSelector.AddCategoryPage("Selector", "Selector");
                ItemManipulator._itemGrabberOptionsMenu2 = ItemManipulator._itemGrabberSelector.AddCategoryPage("Udon Event Selector", "Udon Event Selector");
                ItemManipulator._itemGrabberOptionsMenu2.OnOpen += ItemManipulator.SelectUdon;
                ItemManipulator._itemGrabberOptionsMenu3 = ItemManipulator._itemGrabberSelector.AddCategoryPage("Unfreeze Selector", "Unfreeze Selector");
                ItemManipulator._itemGrabberOptionsMenu3.OnOpen += ItemManipulator.SelectUnfeeze;
                ItemManipulator._itemGrabberMenu.AddCategory("Manipulator Settings");
                ItemManipulator._itemSettingsOptions = ItemManipulator._itemGrabberMenu.GetCategory("Manipulator Settings");
                ItemManipulator._itemSettingsOptions.AddButton("Force Refresh", "Force Refresh", new Action(ItemManipulator.refreshPickups), null);
                ItemManipulator._itemSettingsOptions.AddToggle("Set Global Udon Event", "Set Global Udon Event", new Action<bool>(UdonNetworkTargetToggle), true);
            }
            internal static void UdonNetworkTargetToggle(bool value)
            {
                udonNetworkTarget = ((NetworkEventTarget)(value ? 0 : 1));
            }
            internal static void refreshPickups()
            {
                ItemManipulator._pickups = Resources.FindObjectsOfTypeAll<VRC_Pickup>();
                ItemManipulator.udonBehaviourEntries.Clear();
                ItemManipulator.PrepareUdonBehaviours();
            }
            private static void SelectUnfeeze()
            {
                if (ItemManipulator.calledTwice2)
                {
                    ItemManipulator.calledTwice2 = false;
                    return;
                }
                ItemManipulator.calledTwice2 = true;
                if (ItemManipulator.tempPage3 != 0)
                {
                    ItemManipulator._itemGrabberOptions3.Active = false;
                }
                int num = ++ItemManipulator.tempPage3;
                ItemManipulator._itemGrabberOptionsMenu3.AddCategory("Unfreeze Selector (" + num.ToString() + ")");
                ItemManipulator._itemGrabberOptions3 = ItemManipulator._itemGrabberOptionsMenu3.GetCategory("Unfreeze Selector (" + num.ToString() + ")");
                if (ItemManipulator._frozenItems == null)
                {
                    return;
                }
                using (System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>>> enumerator = (from i in ItemManipulator._frozenItems
                                                                                                                                                             orderby i.Key.gameObject.name
                                                                                                                                                             select i).GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        System.Collections.Generic.KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>> frozenItem = enumerator.Current;
                        ItemManipulator._itemGrabberOptions3.AddButton(frozenItem.Key.gameObject.name, "", delegate
                        {
                        }, null);
                    }
                }
            }
            public static void PrepareUdonBehaviours()
            {
                foreach (UdonBehaviour udonBehaviour in Resources.FindObjectsOfTypeAll<UdonBehaviour>())
                {
                    try
                    {
                        string objectName = udonBehaviour.gameObject.name;
                    }
                    catch (Exception ex)
                    {
                        DeepConsole.Log("MR",$"Error processing UdonBehaviour: {ex.Message}\n{ex.StackTrace}");
                        continue;
                    }
                    var validEventKeys = new System.Collections.Generic.List<string>();
                    int validKeyCount = 0;
                    foreach (var eventEntry in udonBehaviour._eventTable)
                    {
                        if (!eventEntry.Key.StartsWith("_"))
                        {
                            validEventKeys.Add(eventEntry.Key);
                            validKeyCount++;
                        }
                    }
                    if (validKeyCount > 0)
                    {
                        ItemManipulator.udonBehaviourEntries.Add(udonBehaviour, validEventKeys);
                    }
                }
            }
            internal static void disableSelection()
            {
                if (ItemManipulator._itemGrabberOptions != null)
                {
                    ItemManipulator._itemGrabberOptions.Active = false;
                }
                if (ItemManipulator._itemGrabberOptions2 != null)
                {
                    ItemManipulator._itemGrabberOptions2.Active = false;
                }
                ItemManipulator.udonSelected = null;
            }
            private static void SelectUdon()
            {
                if (ItemManipulator.calledTwice)
                {
                    ItemManipulator.calledTwice = false;
                    return;
                }
                ItemManipulator.calledTwice = true;
                if (ItemManipulator.tempPage2 != 0)
                {
                    ItemManipulator._itemGrabberOptions2.Active = false;
                }
                int num = ++ItemManipulator.tempPage2;
                ItemManipulator._itemGrabberOptionsMenu2.AddCategory("Udon Event Selector (" + num.ToString() + ")");
                ItemManipulator._itemGrabberOptions2 = ItemManipulator._itemGrabberOptionsMenu2.GetCategory("Udon Event Selector (" + num.ToString() + ")");
                if (ItemManipulator.udonSelected == null)
                {
                    return;
                }
                using (System.Collections.Generic.IEnumerator<string> enumerator = (from i in ItemManipulator.udonBehaviourEntries[ItemManipulator.udonSelected]
                                                                                    orderby i
                                                              select i).GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        string eventName = enumerator.Current;
                        ItemManipulator._itemGrabberOptions2.AddButton(eventName, "", delegate
                        {
                            ItemManipulator.sendUdonEvent(ItemManipulator.udonSelected, eventName);
                        }, null);
                }
                }
            }

            // Token: 0x0600013E RID: 318 RVA: 0x000079C8 File Offset: 0x00005BC8
            private static void Select(string type)
            {
                if (ItemManipulator.tempPage != 0)
                {
                    ItemManipulator._itemGrabberOptions.Active = false;
                }
                int num = ++ItemManipulator.tempPage;
                ItemManipulator._itemGrabberOptionsMenu.AddCategory("Selector (" + num.ToString() + ")");
                ItemManipulator._itemGrabberOptions = ItemManipulator._itemGrabberOptionsMenu.GetCategory("Selector (" + num.ToString() + ")");
                if (!(type == "Active"))
                {
                    if (!(type == "Inactive"))
                    {
                        if (type == "UdonSelect")
                        {
                            goto IL_20F;
                        }
                        if (!(type == "Freeze"))
                        {
                            goto IL_343;
                        }
                        goto IL_2AA;
                    }
                }
                else
                {
                    ItemManipulator._itemGrabberOptions.AddButton("Force Object ON All", "", new Action(ItemManipulator.setActiveAll), null);
                    using (System.Collections.Generic.IEnumerator<VRC_Pickup> enumerator = (from i in ItemManipulator._pickups
                                                                                            orderby i.gameObject.name
                                                                                            select i).GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            VRC_Pickup pickup = enumerator.Current;
                            if (!ItemManipulator.checkIfBlacklisted(pickup))
                            {
                                ItemManipulator._itemGrabberOptions.AddButton(pickup.name, "", delegate
                                {
                                    ItemManipulator.setActive(pickup);
                                }, null);
                            }
                        }
                        return;
                    }
                }
                ItemManipulator._itemGrabberOptions.AddButton("Force Object OFF All", "", new Action(ItemManipulator.setInactiveAll), null);
                using (System.Collections.Generic.IEnumerator<VRC_Pickup> enumerator2 = (from i in ItemManipulator._pickups
                                                                                         orderby i.gameObject.name
                                                                                         select i).GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        VRC_Pickup pickup = enumerator2.Current;
                        if (!ItemManipulator.checkIfBlacklisted(pickup))
                        {
                            ItemManipulator._itemGrabberOptions.AddButton(pickup.name, "", delegate
                            {
                                ItemManipulator.setInactive(pickup);
                            }, null);
                        }
                    }
                    return;
                }
            IL_20F:
                using (System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<UdonBehaviour, System.Collections.Generic.List<string>>> enumerator3 = (from i in ItemManipulator.udonBehaviourEntries
                                                                                                                                                                              orderby i.Key.gameObject.name
                                                                                                                                                                              select i).GetEnumerator())
                {
                    while (enumerator3.MoveNext())
                    {
                        System.Collections.Generic.KeyValuePair<UdonBehaviour, System.Collections.Generic.List<string>> udonBehaviourEntry = enumerator3.Current;
                        try
                        {
                            ItemManipulator._itemGrabberOptions.AddButton(udonBehaviourEntry.Key.gameObject.name, "", delegate
                            {
                                ItemManipulator.udonSelected = udonBehaviourEntry.Key;
                            }, null);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    return;
                }
            IL_2AA:
                using (System.Collections.Generic.IEnumerator<VRC_Pickup> enumerator4 = (from i in ItemManipulator._pickups
                                                                                         orderby i.gameObject.name
                                                                                         select i).GetEnumerator())
                {
                    while (enumerator4.MoveNext())
                    {
                        VRC_Pickup pickup = enumerator4.Current;
                        if (!ItemManipulator.checkIfBlacklisted(pickup))
                        {
                            ItemManipulator._itemGrabberOptions.AddButton(pickup.name, "", delegate
                            {
                                ItemManipulator.Freeze(pickup);
                            }, null);
                        }
                    }
                    return;
                }
            IL_343:
                ItemManipulator._itemGrabberOptions.AddButton("Bring All Pickup", "", delegate
                {
                    ItemManipulator.BringPickups(null);
                }, null);
                using (System.Collections.Generic.IEnumerator<VRC_Pickup> enumerator5 = (from i in ItemManipulator._pickups
                                                                                         orderby i.gameObject.name
                                                                                         select i).GetEnumerator())
                {
                    while (enumerator5.MoveNext())
                    {
                        VRC_Pickup pickup = enumerator5.Current;
                        if (!ItemManipulator.checkIfBlacklisted(pickup))
                        {
                            ItemManipulator._itemGrabberOptions.AddButton(pickup.name, "", delegate
                            {
                                ItemManipulator.BringPickups(pickup);
                            }, null);
                        }
                    }
                }
            }
            internal static void freezeItemUpdate()
            {
                if (ItemManipulator._frozenItems.Count == 0)
                {
                    return;
                }
                try
                {
                    foreach (System.Collections.Generic.KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>> keyValuePair in from item in ItemManipulator._frozenItems
                    where Networking.GetOwner(item.Key.gameObject).playerId != VRCPlayer.field_Internal_Static_VRCPlayer_0._player.field_Private_VRCPlayerApi_0.playerId                                                                                                   select item)
                    {
                        DeepConsole.Log("Yes",string.Concat(new string[]
                        {
                        "User: ",
                        Networking.GetOwner(keyValuePair.Key.gameObject).displayName,
                        " tried to steal (",
                        keyValuePair.Key.gameObject.name,
                        ")"
                        }));
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0._player.field_Private_VRCPlayerApi_0, keyValuePair.Key.gameObject);
                        Transform transform = keyValuePair.Key.transform;
                        transform.position = keyValuePair.Value.Item1;
                        transform.rotation = keyValuePair.Value.Item2;
                    }
                }
                catch (Exception ex)
                {
                    ItemManipulator._frozenItems.Clear();
                    DeepConsole.Log("Yes", "Frozen Item list cleared!");
                }
            }
            private static void setActive(VRC_Pickup item)
            {
                item.DisallowTheft = false;
                item.allowManipulationWhenEquipped = true;
                item.pickupable = true;
                item.gameObject.SetActive(true);
            }
            private static void sendUdonEvent(UdonBehaviour item, string eventName)
            {
                try
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, item.gameObject);
                    item.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(ItemManipulator.udonNetworkTarget, eventName);
                }
                catch (Exception ex)
                {
                    string str = "Failed sending custom Udon event: (";
                    string str2 = "). Reason: ";
                    Exception ex2 = ex;
                    DeepConsole.Log("Yes", str + eventName + str2 + ((ex2 != null) ? ex2.ToString() : null));
                }
            }
            private static void BringPickups(VRC_Pickup item)
            {
                if (item == null)
                {
                    foreach (VRC_Pickup item2 in ItemManipulator._pickups)
                    {
                        ItemManipulator.PickupItem(item2);
                    }
                    return;
                }
                ItemManipulator.PickupItem(item);
            }
            private static void Freeze(VRC_Pickup item)
            {
                if (ItemManipulator._frozenItems.Any((System.Collections.Generic.KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>> i) => i.Key.gameObject.name == item.gameObject.name))
                {
                    return;
                }
                while (ItemManipulator._frozenItems.Count >= 4)
                {
                    ItemManipulator._frozenItems.Remove(ItemManipulator._frozenItems.First<System.Collections.Generic.KeyValuePair<VRC_Pickup, Tuple<Vector3, Quaternion>>>().Key);
                }
                ItemManipulator._frozenItems.Add(item, new Tuple<Vector3, Quaternion>(item.transform.position, item.transform.rotation));
                DeepConsole.Log("Ues","Freezing Item: " + item.gameObject.name);
            }
            private static void PickupItem(VRC_Pickup item)
            {
                try
                {
                    ItemManipulator.setActive(item);
                    if (Networking.GetOwner(item.gameObject).playerId != VRCPlayer.field_Internal_Static_VRCPlayer_0._playerNet._vrcPlayer.field_Private_VRCPlayerApi_0.playerId)
                    {
                        item.GetComponent<VRC_Pickup>().currentlyHeldBy = null;
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0._player.field_Private_VRCPlayerApi_0, item.gameObject);
                    }
                }
                catch (Exception arg)
                {
                    DeepConsole.Log("YEs",string.Format("Failed to grab item {0}! {1}", item.name, arg));
                }
            }
            private static UiManager _uiManager;
            private static ReCategoryPage _itemGrabberMenu;
            private static ReCategoryPage _itemGrabberOptionsMenu;
            private static ReCategoryPage _itemGrabberOptionsMenu2;
            private static ReCategoryPage _itemGrabberOptionsMenu3;
            private static ReMenuCategory _itemGrabberSelector;
            private static ReMenuCategory _itemGrabber;
            private static ReMenuCategory _itemGrabberOptions;
            private static ReMenuCategory _itemGrabberOptions2;
            private static ReMenuCategory _itemGrabberOptions3;
            private static ReMenuCategory _itemSettingsOptions;
            private static System.Collections.Generic.Dictionary<UdonBehaviour, System.Collections.Generic.List<string>> udonBehaviourEntries = new System.Collections.Generic.Dictionary<UdonBehaviour, System.Collections.Generic.List<string>>();
            private static System.Collections.Generic.Dictionary<VRC_Pickup, Tuple<Vector3, Quaternion>> _frozenItems = new System.Collections.Generic.Dictionary<VRC_Pickup, Tuple<Vector3, Quaternion>>();
            private static UdonBehaviour[] _udonObjects;
            private static UdonBehaviour udonSelected;
            private static VRC_Pickup[] _pickups;
            internal static NetworkEventTarget udonNetworkTarget = 0;
            private static int tempPage;
            private static int tempPage2;
            private static int tempPage3;
            private const int maxFreezeCount = 4;
            private static bool calledTwice;
            private static bool calledTwice2;
        }
    }