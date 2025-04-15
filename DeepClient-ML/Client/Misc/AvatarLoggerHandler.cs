using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using VRC.Core;

namespace DeepCore.Client.Misc
{
    internal class AvatarLoggerHandler
    {
        internal static void Log(ApiAvatar avtr)
        {
            Handle(new Avatar_Object(avtr));
        }
        internal static void Log(Avatar_Object avtr)
        {
            Handle(avtr);
        }
        internal static List<Avatar_Object> Fetch()
        {
            return FetchFile();
        }
        internal static void Clear()
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            File.Delete(filePath);
            DeepConsole.Log("AvaLogger","Avatar Logger file cleared!");
        }
        private static void Handle(Avatar_Object avtr)
        {
            try
            {
                List<Avatar_Object> list = FetchFile();
                list.RemoveAll((Avatar_Object avatarObject) => avatarObject.id == avtr.id);
                list.Add(avtr);
                if (list.Count > maxEntries)
                {
                    list.RemoveRange(0, list.Count - maxEntries);
                }
                File.WriteAllText(filePath, JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                DeepConsole.Log("AvaLogger","Failed fetching/writing to avatar log file. "+ex.Message);
            }
        }
        private static List<Avatar_Object> FetchFile()
        {
            List<Avatar_Object> list = null;
            if (File.Exists(filePath))
            {
                list = JsonConvert.DeserializeObject<List<Avatar_Object>>(File.ReadAllText(filePath));
            }
            return list ?? new List<Avatar_Object>();
        }
        private const string fileName = "/AvatarLogger.json";
        private static readonly int maxEntries = ConfManager.maxAvatarLogToFile.Value;
        private static readonly string filePath = ConfManager.getResourcePathFull() + "/AvatarLogger.json";
    }
}