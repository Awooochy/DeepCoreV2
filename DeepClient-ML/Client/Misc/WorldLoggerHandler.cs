using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using VRC.Core;

namespace DeepCore.Client.Misc
{
    public class WorldLoggerHandler
    {
        public static void Log()
        {
            string filePath = "DeepClient/WorldLogger.json";
            List<Instance> instances = new List<Instance>();

            if (File.Exists(filePath))
            {
                string existingJson = File.ReadAllText(filePath);
                if (!string.IsNullOrWhiteSpace(existingJson))
                {
                    instances = JsonConvert.DeserializeObject<List<Instance>>(existingJson) ?? new List<Instance>();
                }
            }

            instances.Add(new Instance
            {
                InstanceID = APIUser.CurrentUser.location,
                InstanceType = RoomManager.prop_ApiWorldInstance_1.type.ToString(),
                InstanceName = RoomManager.prop_ApiWorldInstance_1.name,
                InstanceOwner = "idk",
                WorldName = RoomManager.field_Internal_Static_ApiWorld_0.name
            });

            string json = JsonConvert.SerializeObject(instances, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        internal static void Clear()
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            File.Delete(filePath);
            DeepConsole.Log("WLogger", "World Log file cleared.");
        }
        internal static List<World_Object> Fetch()
        {
            return FetchFile();
        }
        private static List<World_Object> FetchFile()
        {
            List<World_Object> list = null;
            if (File.Exists(filePath))
            {
                list = JsonConvert.DeserializeObject<List<World_Object>>(File.ReadAllText(filePath));
            }
            return list ?? new List<World_Object>();
        }
        private const string fileName = "/WorldLogger.json";
        private static readonly string filePath = ConfManager.getResourcePathFull() + "/WorldLogger.json";
        private static readonly int maxEntries = 96;
    }
}
public class Instance
{
    public string InstanceID { get; set; }
    public string InstanceType { get; set; }
    public string InstanceName { get; set; }
    public string InstanceOwner { get; set; }
    public string WorldName { get; set; }
}
