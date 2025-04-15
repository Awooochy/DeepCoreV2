using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Funnies
{
    internal class FagFinder
    {
        public static string[] FagThings = { "i'm trans", "im trans", "i am trans", "i an trans", "gender\u02f8 trans", "i'm male", "im male", "i am male"};
        public static string[] Pronoms = { "he⁄him", "he⁄them", "he⁄they", "them⁄him"};
        public static void FindTheFag(Player player)
        {
            string tag = "";
            if (player.field_Private_APIUser_0.bio.Contains($"{FagThings}"))
            {
                tag += "Faggot - ";
            }
            foreach (string pronoun in Pronoms)
            {
                if (player.field_Private_APIUser_0.bio.Contains(pronoun))
                {
                    tag += $"{pronoun}";
                }
            }
            API.PlayerTagSystem.AddTag(tag,Color.red,player);
        }
    }
}
