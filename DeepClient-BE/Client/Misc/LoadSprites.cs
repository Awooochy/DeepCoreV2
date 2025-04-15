using System.IO;
using UnityEngine;

namespace DeepClient.Client.Misc
{
    internal static class LoadSprites
    {
        public static Sprite LoadSpriteFromDisk(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            byte[] array = File.ReadAllBytes(path);
            if (array == null || array.Length == 0)
            {
                return null;
            }
            Texture2D texture2D = new Texture2D(512, 512);
            if (!ImageConversion.LoadImage(texture2D, array))
            {
                return null;
            }
            Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0f, 0f), 100000f, 1000U, 0, Vector4.zero, false);
            return sprite;
        }
    }
}