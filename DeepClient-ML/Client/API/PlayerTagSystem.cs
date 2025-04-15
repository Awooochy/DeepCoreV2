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
        {"usr_a7d59ec0-4e6a-4f94-ad37-972602b72958", ("Silly Beans :3", "#570f96")}, //LXQtie
        {"usr_fc152d76-e45a-448e-b90a-860da7ea8b3e", ("Débiteur halal.", "#00ff44")}, //faucheuse -
        {"usr_7dd8c2f2-6884-44c6-b9b2-490770b64a49", ("<size=120%>HORNY FURRY<size=90%>LTG Member", "#ff0000")}, //ＶＩＫＳＴＯＲＮᴷᴳ
        {"usr_53df9514-8d47-4c5c-ae32-04735c392e8b", ("SnØwfall", "#ff0000")}, //puffaufromage
        {"usr_29187716-6c4d-4215-9342-925b99fe4374", ("<size=220%>Pédo", "#00ff51")}, //Nakat0_中戸
        {"usr_8775051c-9e24-4710-8d60-db0aaef67534", ("- BadUpdate Enjoyer -", "#00ff51")}, //TheFrenchStoner
        {"usr_78d05283-0c89-4f88-a89e-757233aebb81", ("<size=140%>Fuckable", "#e292fc")} //Proza₡
        };
    }
}
