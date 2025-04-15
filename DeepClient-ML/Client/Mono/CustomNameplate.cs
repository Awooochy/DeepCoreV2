using System;
using MelonLoader;
using TMPro;
using UnityEngine;
using VRC;
using DeepCore.Client.Misc;
using System.Collections.Generic;
using UnityEngine.UI;

namespace DeepCore.Client.Mono
{
    public class CustomNameplate : MonoBehaviour, IDisposable
    {
        public Player Player { get; set; }
        private static Dictionary<CustomNameplate, string> nameplates = new Dictionary<CustomNameplate, string>();
        private TextMeshProUGUI _statsText;
        public bool OverRender = true;
        public bool Enabled = true;
        public string Role = string.Empty;
        private float StartTime;
        private float looptime;
        public string DateCreated;
        private string FormattedDate;
        private string platform;
        public bool IsDcOwner;
        public bool isdcuser;
        private string ranktext = "Visitor";
        public CustomNameplate(IntPtr ptr) : base(ptr)
        {
        }
        public void Dispose()
        {
            if (this._statsText != null)
            {
                this._statsText.text = null;
                this._statsText.OnDisable();
                GameObject.Destroy(this._statsText.gameObject);
                this._statsText = null;
            }
            base.enabled = false;
            nameplates.Remove(this);
        }
        private void Start()
        {
            try
            {
                PlayerNameplate field_Public_PlayerNameplate_ = this.Player._vrcplayer.field_Public_PlayerNameplate_0;
                if (field_Public_PlayerNameplate_ != null)
                {
                    Transform transform = field_Public_PlayerNameplate_.field_Public_GameObject_5.transform;
                    if (transform.Find("Trust Text").GetComponent<TextMeshProUGUI>() != null)
                    {
                        Transform transform2 = GameObject.Instantiate<Transform>(transform, transform.parent);
                        transform2.localPosition = new Vector3(0f, -75f, 0f);
                        transform2.gameObject.SetActive(true);
                        this._statsText = transform2.Find("Trust Text").GetComponent<TextMeshProUGUI>();
                        if (this._statsText != null)
                        {
                            this._statsText.color = Color.white;
                            this._statsText.isOverlay = (this.OverRender && this.Enabled);
                        }
                        Transform transform3 = transform2.Find("Trust Icon");
                        if (transform3 != null)
                        {
                            transform3.gameObject.SetActive(false);
                        }
                        Transform transform4 = transform2.Find("Performance Icon");
                        if (transform4 != null)
                        {
                            transform4.gameObject.SetActive(false);
                        }
                        Transform transform5 = transform2.Find("Performance Text");
                        if (transform5 != null)
                        {
                            transform5.gameObject.SetActive(false);
                        }
                        Transform transform6 = transform2.Find("Friend Anchor Stats");
                        if (transform6 != null)
                        {
                            transform6.gameObject.SetActive(true);
                        }
                    }
                }
                this.StartTime = Time.time;
                this.looptime = Time.time;
                ColorShit();
            }
            catch (Exception ex)
            {
                DeepConsole.Log("CNP",ex.Message);
            }
            switch (PlayerUtil.GetPlayerRank(this.Player._vrcplayer))
            {
                case 1:
                    this.ranktext = "<color=#6495ED>New User</color>";
                    break;
                case 2:
                    this.ranktext = "<color=#90EE90>User</color>";
                    break;
                case 3:
                    this.ranktext = "<color=#ffca5d>Known</color>";
                    break;
                case 4:
                    this.ranktext = "<color=#d472ff>Trusted</color>";
                    break;
                case 5:
                    this.ranktext = "<color=#ff7575>Troll</color>";
                    break;
                case 6:
                    this.ranktext = "<color=#fffd81>Friend</color>";
                    break;
            }
            if (this.Player.field_Private_VRCPlayerApi_0.IsUserInVR())
            {
                if (this.Player.field_Private_APIUser_0.last_platform.ToLower() != "standalonewindows")
                {
                    this.platform = "Q";
                }
                else
                {
                    this.platform = "VR";
                }
            }
            else if (this.Player.field_Private_APIUser_0.last_platform.ToLower() != "standalonewindows")
            {
                this.platform = "Android";
            }
            else
            {
                this.platform = "D";
            }
            if (this.Player._vrcplayer._player.field_Private_APIUser_0.id == "")
            {
                this.IsDcOwner = true;
            }
            nameplates.Add(this, this.Player.field_Private_APIUser_0.id);
        }
        private void Update()
        {
            if (this._statsText == null)
            {
                return;
            }
            if (Time.time >= this.looptime + 3f)
            {
                int num = (int)(Time.time - this.StartTime);
                int num2 = num / 3600;
                int num3 = num % 3600 / 60;
                int num4 = num % 60;
                string text;
                if (num2 > 0)
                {
                    float num5 = (float)num / 3600f;
                    text = string.Format("{0:F1}h", num5);
                }
                else if (num3 > 0)
                {
                    text = string.Format("{0}m", num3);
                }
                else
                {
                    text = string.Format("{0}s", num4);
                }
                string text2 = this.platform;
                this._statsText.text = string.Concat(new string[]
                {
                    "[",
                    "<color=#00ffd5>",
                    text,
                    "</color>]:[",
                    text2,
                    "]:[",
                    this.ranktext,
                    "]:[F:",
                    PlayerUtil.ColourFPS(this.Player),
                    "]:[P:",
                    PlayerUtil.GetPingColord(this.Player),
                    "]:[",
                    PlayerUtil.IsPlayerMicDisabled(this.Player),
                    "]",
                    this.FormattedDate
                });
                this.looptime = Time.time;
            }
        }
        public static CustomNameplate FindById(string Id)
        {
            foreach (KeyValuePair<CustomNameplate, string> keyValuePair in nameplates)
            {
                if (keyValuePair.Value == Id)
                {
                    return keyValuePair.Key;
                }
            }
            return null;
        }
        public static void PlayerLeft(string Id)
        {
            foreach (KeyValuePair<CustomNameplate, string> keyValuePair in nameplates)
            {
                if (keyValuePair.Value == Id)
                {
                    nameplates.Remove(keyValuePair.Key);
                    break;
                }
            }
        }
        private void ColorShit()
        {
            PlayerNameplate field_Public_PlayerNameplate_ = this.Player._vrcplayer.field_Public_PlayerNameplate_0;
            Transform transform = field_Public_PlayerNameplate_.field_Public_GameObject_0.transform;
            if (transform.Find("Main/Background"))
            {
                transform.Find("Main/Background").GetComponent<ImageThreeSlice>().gameObject.SetActive(false);
            }
            if (transform.Find("Main/Glow"))
            {
                transform.Find("Main/Glow").GetComponent<ImageThreeSlice>().color = this.Player.field_Private_APIUser_0.GetPlayerColor();
            }
            if (transform.Find("Main/Pulse"))
            {
                transform.Find("Main/Pulse").GetComponent<ImageThreeSlice>().color = this.Player.field_Private_APIUser_0.GetPlayerColor();
            }
            if (transform.Find("Text Container/Name"))
            {
                transform.Find("Text Container/Name").GetComponent<NameplateTextMeshProUGUI>().color = this.Player.field_Private_APIUser_0.GetPlayerColor();
                transform.Find("Text Container/Name").GetComponent<NameplateTextMeshProUGUI>().m_Color = this.Player.field_Private_APIUser_0.GetPlayerColor();
            }
            if (transform.Find("Icon/Background"))
            {
                transform.Find("Icon/Background").GetComponent<Image>().color = this.Player.field_Private_APIUser_0.GetPlayerColor();
            }
            if (transform.Find("Icon/Glow"))
            {
                transform.Find("Icon/Glow").GetComponent<ImageThreeSlice>().color = this.Player.field_Private_APIUser_0.GetPlayerColor();
            }
            if (transform.Find("Icon/Pulse"))
            {
                transform.Find("Icon/Pulse").GetComponent<ImageThreeSlice>().color = this.Player.field_Private_APIUser_0.GetPlayerColor();
            }
        }
    }
}
