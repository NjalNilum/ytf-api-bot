﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FacebookAutomation
{
    public class Group
    {
        [JsonPropertyName("groupName")]
        public string GroupName { get; set; }

        [JsonPropertyName("groupId")]
        public string GroupId { get; set; }
    }

    public class User
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("pw")]
        public string Pw { get; set; }
    }

    public class FacebookConfig
    {
        [JsonPropertyName("users")]
        public List<User> Users { get; set; }

        [JsonPropertyName("testGroups")]
        public List<Group> TestGroups { get; set; }

        [JsonPropertyName("taskGroups_01")]
        public List<Group> TaskGroups_01 { get; set; }

        [JsonPropertyName("taskGroups_Gayman")]
        public List<Group> TaskGroups_Gayman { get; set; }

        [JsonPropertyName("taskGroups_SchwuchtelGruppen")]
        public List<Group> TaskGroups_SchwuchtelGruppen { get; set; }

        [JsonPropertyName("taskGroups_DSBM")]
        public List<Group> TaskGroups_DSBM { get; set; }

        [JsonPropertyName("taskGroups_smallVinylGroup")]
        public List<Group> TaskGroups_smallVinylGroup { get; set; }

        [JsonPropertyName("skyrimGroups")]
        public List<Group> SkyrimGroups { get; set; }
    }

}
