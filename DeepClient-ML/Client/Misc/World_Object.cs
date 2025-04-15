using VRC.Core;

namespace DeepCore.Client.Misc
{
    internal class World_Object
    {
        public string worldName { get; set; }
        public string worldID { get; set; }
        public string worldImageURL { get; set; }
        public string instanceID { get; set; }
        public string instanceName { get; set; }
        public string instanceType { get; set; }
        public string instanceOwner { get; set; }
        internal World_Object()
        {
        }
        internal World_Object(ApiWorld wrld, ApiWorldInstance worldInstance)
        {
            this.worldName = wrld.name;
            this.worldID = wrld.id;
            this.worldImageURL = wrld.imageUrl;
            this.instanceID = worldInstance.instanceId;
            this.instanceName = worldInstance.name;
            this.instanceType = worldInstance.type.ToString();
            this.instanceOwner = (worldInstance.ownerId ?? "");
        }
    }
}
