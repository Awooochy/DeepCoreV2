using System;
using UnityEngine;
using VRC.Localization;
using Il2CppSystem.Collections.Generic;
using TMPro;
using VRC.UI.Elements.Controls;
using VRC.UI.Client;

namespace DeepCore.Client.Misc
{
    public static class PopupHelper
    {
        public static void AlertPopup(string tittle, string content, int time)
        {
            VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_Single_0(LocalizableStringExtensions.Localize(tittle, null, null, null), LocalizableStringExtensions.Localize(content, null, null, null), time);
        }
        public static void OpenVideoInMM(string Tittle, string url, bool s)
        {
            VRCUIManager.prop_VRCUIManager_0.Method_Public_Virtual_Final_New_Void_String_String_Boolean_0(Tittle, url, s);
        }
        public static void StandardPopup(string TTle, string Ctent, string btext, System.Action value)
        {
            if (VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0 != null)
            {
                LocalizableString Tittle = LocalizableStringExtensions.Localize(TTle, null, null, null);
                LocalizableString Content = LocalizableStringExtensions.Localize(Ctent, null, null, null);
                LocalizableString buttontext = LocalizableStringExtensions.Localize(btext, null, null, null);
                System.Action combinedAction = () =>
                {
                    VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_0();
                    value?.Invoke();
                };
                VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_Action_1_VRCUiPopup_0(Tittle,Content,buttontext,combinedAction);
            }
            else
            {
                DeepConsole.Log("VRCUiPopup", "VRCUiPopupManager is not initialized!");
            }
        }
        public static void StandardPopupV2(string T,string C,string B,System.Action AC)
        {
            LocalizableString Tittle = LocalizableStringExtensions.Localize(T, null, null, null);
            LocalizableString Content = LocalizableStringExtensions.Localize(C, null, null, null);
            LocalizableString ButtonText = LocalizableStringExtensions.Localize(B, null, null, null);
            System.Action combinedAction = () =>
            {
                VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_0();
                AC?.Invoke();
            };
            VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_Action_1_VRCUiPopup_0(Tittle, Content, ButtonText,combinedAction);
        }
        public static void PopupCall(string T, string S, string BT, bool IsNumpad, Action<string> onUserInputAction = null)
        {
            LocalizableString Tittle = LocalizableStringExtensions.Localize(T, null, null, null);
            LocalizableString Sub = LocalizableStringExtensions.Localize(S, null, null, null);
            LocalizableString ntg = LocalizableStringExtensions.Localize("", null, null, null);
            LocalizableString BText = LocalizableStringExtensions.Localize(BT, null, null, null);
            Action<string, List<KeyCode>, TextMeshProUGUIEx> action = (userInput, keyCodes, textComponent) =>
            {
                if (onUserInputAction != null)
                {
                    onUserInputAction(userInput);
                }
                else
                {
                    Console.WriteLine($"User Input: {userInput}");
                }
            };
            Il2CppSystem.Action<string, List<KeyCode>, TextMeshProUGUIEx> logInputAction = action;
            Action action1 = () =>
            {
            };
            Il2CppSystem.Action onCloseAction = action1;
            VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_InputType_Boolean_LocalizableString_Action_3_String_List_1_KeyCode_TextMeshProUGUIEx_Action_LocalizableString_Boolean_Action_1_VRCUiPopup_Boolean_Int32_0(
            Tittle, ntg, TMP_InputField.InputType.Standard, IsNumpad, BText, logInputAction, onCloseAction, Sub, true, null, false, 0
            );
        }
        public static void ConfirmNotification(string title, string Content, string okButtonName, Action onSuccess)
        {
            LocalizableString localizableString = LocalizableStringExtensions.Localize(title, null, null, null);
            LocalizableString localizableString2 = LocalizableStringExtensions.Localize(Content, null, null, null);
            LocalizableString localizableString3 = LocalizableStringExtensions.Localize(okButtonName, null, null, null);
            LocalizableString localizableString4 = LocalizableStringExtensions.Localize(okButtonName, null, null, null);
            Action value = () =>
            {
                onSuccess();
                VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_0();
            };
            Action value1 = () =>
            {
                VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_0();
            };
            VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_LocalizableString_Action_Action_1_VRCUiPopup_0(localizableString, localizableString2, localizableString3, value, localizableString4, value1, null);
        }
        public static void testthings()
        {
            LocalizableString Tittle = LocalizableStringExtensions.Localize("a", null, null, null);
            LocalizableString Sub = LocalizableStringExtensions.Localize("n", null, null, null);
            Action<VRCUiPopup> action1 = (VRCUiPopup popup) =>
            {
                Console.WriteLine("Popup action triggered.");
            };
            Il2CppSystem.Action<VRCUiPopup> action = action1;
            VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_LocalizableString_Action_Action_1_VRCUiPopup_PDM_0(Tittle, Sub, Sub,null,null, null);
        }
    }
}
