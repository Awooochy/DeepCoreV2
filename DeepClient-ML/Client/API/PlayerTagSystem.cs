using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VRC;

namespace DeepCore.Client.API
{
    internal class PlayerTagSystem
    {
        public static void CheckPlayer(Player player)
        {
            if (!allowedPlayers.TryGetValue(player.field_Private_APIUser_0.id, out var playerInfo))
            {
                return;
            }
            Color myColor;
            ColorUtility.TryParseHtmlString(playerInfo.Color, out myColor);
            AddTag(playerInfo.Tag, myColor, player);
        }
        public static void AddTag(string CustomTag, Color color, Player player)
        {
            PlayerNameplate nameplate2 = player.prop_VRCPlayer_0.field_Public_PlayerNameplate_0;
            Transform transform = GameObject.Instantiate<Transform>(nameplate2.gameObject.transform.Find("Contents/Quick Stats"), nameplate2.gameObject.transform.Find("Contents"));
            transform.parent = nameplate2.gameObject.transform.Find("Contents");
            transform.gameObject.SetActive(true);
            TextMeshProUGUI component = transform.Find("Trust Text").GetComponent<TextMeshProUGUI>();
            component.color = color;
            transform.Find("Trust Icon").gameObject.SetActive(false);
            transform.Find("Performance Icon").gameObject.SetActive(false);
            transform.Find("Performance Text").gameObject.SetActive(false);
            transform.Find("Friend Anchor Stats").gameObject.SetActive(false);
            transform.name = "DC Info Tag";
            transform.gameObject.transform.localPosition = new Vector3(0f, 145f, 0f);
            transform.GetComponent<ImageThreeSlice>().color = color;
            component.text = CustomTag;
        }
        private static Dictionary<string, (string Tag, string Color)> allowedPlayers = new Dictionary<string, (string, string)>
        {
        {"usr_a7d59ec0-4e6a-4f94-ad37-972602b72958", ("Ex DeepClient Dev", "#570f96")}, //LXQtie
        {"usr_78d05283-0c89-4f88-a89e-757233aebb81", ("<size=140%>Retard", "#e292fc")}, //Proza₡
        {"usr_452c013c-10e6-4467-bc2c-67a8a322fbf2", ("<size=200%>DEEPCORE V2 DEV", "#e292fc")}
        };
    }
}
