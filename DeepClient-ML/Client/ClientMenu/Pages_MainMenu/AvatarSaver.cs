using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using DeepCore.Client.Misc;
using MelonLoader;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
    internal class AvatarSaver
    {
        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);
        [DllImport("user32.dll")]
        private static extern bool EmptyClipboard();
        [DllImport("user32.dll")]
        private static extern IntPtr SetClipboardData(uint uFormat, IntPtr data);
        [DllImport("user32.dll")]
        private static extern bool CloseClipboard();
        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalAlloc(uint flags, int size);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalLock(IntPtr hMem);
        [DllImport("kernel32.dll")]
        private static extern bool GlobalUnlock(IntPtr hMem);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalFree(IntPtr hMem);

        public static ReMenuPage menuPage;
        public static readonly string SAVE_PATH = Path.Combine(Environment.CurrentDirectory, "DeepClient", "SavedAvatars.txt");
        public static readonly string VRCA_PATH = Path.Combine(Environment.CurrentDirectory, "DeepClient", "SavedVRCAs");
        public static List<SavedAvatar> savedAvatars = new List<SavedAvatar>();
        public static bool autoSave;
        public static uint GMEM_MOVEABLE = 2U;
        public static uint CF_UNICODETEXT = 13U;
        public static void AvSaverFunctionsMenu(UiManager UIManager)
        {
            ReMenuCategory category = UIManager.QMMenu.GetCategoryPage("Utils Functions").GetCategory("Functions");
            menuPage = category.AddMenuPage("Avatar Saver", "Save and manage avatars", null, "#ffffff");
            menuPage.AddButton("Save Current Avatar", "Save your current avatar", new Action(SaveCurrentAvatar), null, "#ffffff");
            menuPage.AddButton("View Saved Avatars", "Show saved avatars", new Action(BuildAndOpenAvatarList), null, "#ffffff");
        }
        public static void BuildAndOpenAvatarList()
        {
            try
            {
                ReMenuPage reMenuPage = menuPage.AddMenuPage("Saved Avatars", "List of saved avatars", null, "#ffffff");
                using (List<SavedAvatar>.Enumerator enumerator = savedAvatars.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        SavedAvatar avatar = enumerator.Current;
                        reMenuPage.AddMenuPage(avatar.AvatarName, "By: " + avatar.AuthorName + "\nSaved: " + avatar.SaveDate, delegate (ReMenuPage page)
                        {
                            CreateAvatarButtons(page, avatar);
                        }, null, "#ffffff");
                    }
                }
                reMenuPage.Open();
            }
            catch (Exception arg)
            {
                MelonLogger.Error(string.Format("[AvatarSaver] Menu build error: {0}", arg));
            }
        }
        public static void CreateAvatarButtons(ReMenuPage page, AvatarSaver.SavedAvatar avatar)
        {
            page.AddButton("Switch To Avatar", "Use this avatar", delegate
            {
                SwitchToAvatar(avatar);
            }, null, "#ffffff");
        }
        public static void SaveCurrentAvatar()
        {
            try
            {
                VRCAvatarManager vrcavatarManager = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCAvatarManager_0;
                ApiAvatar apiAvatar = vrcavatarManager.field_Private_ApiAvatar_0;
                SaveAvatarInfo(apiAvatar, true);
            }
            catch (System.TypeLoadException ex)
            {
                DeepConsole.LogConsole("AvaSaver", $"TypeLoadException: {ex.Message}");
            }
            catch (System.NullReferenceException ex)
            {
                DeepConsole.LogConsole("AvaSaver", $"NullReferenceException: {ex.Message}");
            }
            catch (Exception ex)
            {
                DeepConsole.LogConsole("AvaSaver", $"Unexpected error: {ex}");
            }
        }
        public static bool SaveAvatarInfo(ApiAvatar avatar, bool notify)
        {
            bool result;
            try
            {
                SavedAvatar savedAvatar = savedAvatars.FirstOrDefault((SavedAvatar a) => a.AvatarId == avatar.id);
                bool flag = savedAvatar != null;
                if (flag)
                {
                    savedAvatar.UpdateFromApiAvatar(avatar);
                    SaveToFile();
                    if (notify)
                    {
                        DeepConsole.Log("AvaSaver", $"Updated: {avatar.name}");
                    }
                    result = false;
                }
                else
                {
                    SavedAvatar item = new SavedAvatar
                    {
                        AvatarId = avatar.id,
                        AvatarName = avatar.name,
                        AuthorName = avatar.authorName,
                        SaveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        AssetUrl = avatar.assetUrl,
                        ThumbnailUrl = avatar.thumbnailImageUrl,
                        IsPublic = (avatar.releaseStatus == "public"),
                        HasVRCA = false
                    };
                    savedAvatars.Add(item);
                    SaveToFile();
                    if (notify)
                    {
                        DeepConsole.Log("AvaSaver", $"Added: {avatar.name}");
                    }
                    result = true;
                }
            }
            catch (Exception arg)
            {
                DeepConsole.LogConsole("AvaSaver",(string.Format("Save avatar error: {0}", arg)));
                result = false;
            }
            return result;
        }
        public static void SaveToFile()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(SAVE_PATH));
                File.WriteAllLines(SAVE_PATH, from a in savedAvatars
                                              select a.ToString());
            }
            catch (Exception arg)
            {
                DeepConsole.LogConsole("AvaSaver", (string.Format("Save to file error: {0}", arg)));
            }
        }
        public static void SwitchToAvatar(SavedAvatar avatar)
        {
            try
            {
                ApiAvatar apiAvatar = new ApiAvatar { id = avatar.AvatarId };

                Action<ApiContainer> value = (ApiContainer container) =>
                {
                    try
                    {
                        ApiAvatar apiAvatar2 = container.Model as ApiAvatar;
                        if (apiAvatar2 == null)
                        {
                            DeepConsole.LogConsole("AvaSaver", "Downloaded avatar is null.");
                            return;
                        }
                        VRCPlayer localVRCPlayer = PlayerUtil.GetLocalVRCPlayer();
                        VRCAvatarManager vrcAvatarManager = localVRCPlayer?.field_Private_VRCAvatarManager_0;
                        if (vrcAvatarManager == null)
                        {
                            DeepConsole.LogConsole("AvaSaver", "Avatar Manager is null.");
                            return;
                        }
                        MethodInfo method = typeof(VRCAvatarManager).GetMethod("SetAvatar", BindingFlags.Instance | BindingFlags.Public);
                        if (method == null)
                        {
                            DeepConsole.LogConsole("AvaSaver", "Unable to switch avatar. Method not found.");
                            return;
                        }
                        method.Invoke(vrcAvatarManager, new object[] { apiAvatar2 });
                        VRCPlayerApi localPlayer = Networking.LocalPlayer;
                        if (localPlayer != null && localPlayer.IsValid())
                        {
                            SetAvatarScale(localPlayer, 1f);
                        }
                        DeepConsole.Log("AvaSaver", $"Changed to {avatar.AvatarName}");
                        }
                        catch (Exception ex)
                        {
                        DeepConsole.LogConsole("AvaSaver", $"Failed to switch avatar: {ex.Message}");
                        }
                };
                apiAvatar.Get(value, null, null);
            }
            catch (Exception ex)
            {
                DeepConsole.LogConsole("AvaSaver", $"Failed to initiate avatar switch: {ex.Message}");
            }
        }
        public static void SetAvatarScale(VRCPlayerApi player, float scale)
        {
            try
            {
                GameObject gameObject = player.gameObject;
                bool flag = gameObject != null;
                if (flag)
                {
                    gameObject.transform.localScale = new Vector3(scale, scale, scale);
                    MelonLogger.Msg(string.Format("[AvatarSaver] Set avatar scale to {0}", scale));
                }
                else
                {
                    MelonLogger.Warning("[AvatarSaver] Player GameObject is null.");
                }
            }
            catch (Exception arg)
            {
                MelonLogger.Error(string.Format("[AvatarSaver] SetAvatarScale error: {0}", arg));
            }
        }
        public class SavedAvatar
        {
            public string AvatarId { get; set; }
            public string AvatarName { get; set; }
            public string AuthorName { get; set; }
            public string SaveDate { get; set; }
            public string AssetUrl { get; set; }
            public string ThumbnailUrl { get; set; }
            public bool IsPublic { get; set; }
            public bool HasVRCA { get; set; }
            public override string ToString()
            {
                return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", new object[]
                {
                    this.AvatarId,
                    this.AvatarName,
                    this.AuthorName,
                    this.SaveDate,
                    this.AssetUrl,
                    this.ThumbnailUrl,
                    this.IsPublic,
                    this.HasVRCA
                });
            }
            public static SavedAvatar FromString(string line)
            {
                string[] array = line.Split(new char[]
                {
                    '|'
                });
                bool flag = array.Length < 8;
                if (flag)
                {
                    throw new FormatException("Invalid saved avatar format.");
                }
                return new SavedAvatar
                {
                    AvatarId = array[0],
                    AvatarName = array[1],
                    AuthorName = array[2],
                    SaveDate = array[3],
                    AssetUrl = array[4],
                    ThumbnailUrl = array[5],
                    IsPublic = bool.Parse(array[6]),
                    HasVRCA = bool.Parse(array[7])
                };
            }
            public void UpdateFromApiAvatar(ApiAvatar avatar)
            {
                this.AvatarName = avatar.name;
                this.AuthorName = avatar.authorName;
                this.SaveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.AssetUrl = avatar.assetUrl;
                this.ThumbnailUrl = avatar.thumbnailImageUrl;
                this.IsPublic = (avatar.releaseStatus == "public");
            }
        }
    }
}