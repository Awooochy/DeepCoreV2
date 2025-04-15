using System;
using System.IO;
using UnityEngine;

namespace DeepClient.Client.Misc
{
    public class Resources
    {
        public static Sprite LoadSprite(string sprite)
        {
            return (resourcePath + "/" + sprite).LoadSpriteFromDisk();
        }
        private static string resourcePath = Path.Combine(Environment.CurrentDirectory, "DeepClient/");
    }
}