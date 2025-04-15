using System.Linq;
using VRC.Core;

namespace DeepCore.Client.Misc
{
    internal class Avatar_Object
    {
        public string id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public string authorName { get; set; }
        public string authorId { get; set; }
        public string assetUrl { get; set; }
        public string description { get; set; }
        public string thumbnailImageUrl { get; set; }
        public int version { get; set; }
        public int supportedPlatforms { get; set; }
        public string releaseStatus { get; set; }
        public bool isblackListed { get; set; }
        internal Avatar_Object()
        {
        }
        internal Avatar_Object(ApiAvatar avtr)
        {
            this.id = avtr.id;
            this.name = avtr.name;
            this.imageUrl = avtr.imageUrl;
            this.authorName = avtr.authorName;
            this.authorId = avtr.authorId;
            this.assetUrl = avtr.assetUrl;
            this.description = avtr.description;
            this.thumbnailImageUrl = avtr.thumbnailImageUrl;
            this.version = avtr.version;
            this.supportedPlatforms = savePlatform(avtr.supportedPlatforms);
            this.releaseStatus = avtr.releaseStatus;
        }
        internal Avatar_Object(Avatar_Object avtr)
        {
            this.id = avtr.id;
            this.name = avtr.name;
            this.imageUrl = avtr.imageUrl;
            this.authorName = avtr.authorName;
            this.authorId = avtr.authorId;
            this.assetUrl = avtr.assetUrl;
            this.description = avtr.description;
            this.thumbnailImageUrl = avtr.thumbnailImageUrl;
            this.version = avtr.version;
            this.supportedPlatforms = avtr.supportedPlatforms;
            this.releaseStatus = avtr.releaseStatus;
            this.isblackListed = avtr.isblackListed;
        }
        internal ApiAvatar toApiAvatar()
        {
            return new ApiAvatar
            {
                id = this.id,
                name = this.name,
                authorName = this.authorName,
                authorId = this.authorId,
                assetUrl = this.assetUrl,
                description = this.description,
                thumbnailImageUrl = this.thumbnailImageUrl,
                version = this.version,
                supportedPlatforms = Avatar_Object.getPlatform(this.supportedPlatforms),
                releaseStatus = this.releaseStatus
            };
        }
        private static ApiModel.SupportedPlatforms getPlatform(int num)
        {
            switch (num)
            {
                case 0:
                    return (ApiModel.SupportedPlatforms)3;
                case 1:
                    return (ApiModel.SupportedPlatforms)2;
                case 2:
                    return (ApiModel.SupportedPlatforms)1;
                default:
                    return (ApiModel.SupportedPlatforms)0;
            }
        }
        private static int savePlatform(ApiModel.SupportedPlatforms supportedPlatform)
        {
            switch (supportedPlatform)
            {
                case (ApiModel.SupportedPlatforms)1:
                    return 2;
                case (ApiModel.SupportedPlatforms)2:
                    return 1;
                case (ApiModel.SupportedPlatforms)3:
                    return 0;
                default:
                    return 3;
            }
        }
        private static bool checkIfBlacklisted(System.Collections.Generic.List<string> tags)
        {
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            foreach (string item in tags)
            {
                list.Add(item);
            }
            return list.Any((string x) => Avatar_Object.blackListed.Any((string y) => y == x));
        }
        private static readonly System.Collections.Generic.List<string> blackListed = new System.Collections.Generic.List<string>
        {
            "admin_featured_quest",
            "admin_quest_fallback_extended",
            "admin_quest_fallback_basic",
            "author_quest_fallback"
        };
    }
}