using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common
{

    public class Group
    {
        [JsonPropertyName("groupName")]
        public string GroupName { get; set; }

        [JsonPropertyName("groupId")]
        public string GroupId { get; set; }

        public Group()
        {
            GroupName = string.Empty;
            GroupId = string.Empty;
        }
    }

    public class User
    {
        /// <summary>
        /// FB name of user
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Email if available
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// FB id of user
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Groups actual user is a member of
        /// </summary>
        [JsonPropertyName("groups")]
        public List<Group> Groups { get; set; }

        public User()
        {
            Name = string.Empty;
            Email = string.Empty;
            Id = string.Empty; 
            Groups = new List<Group>();
        }
    }

    public class FbUsers
    {
        /// <summary>
        /// Groups actual user is a member of
        /// </summary>
        [JsonPropertyName("listOfUsers")]
        public List<User> ListOfUsers { get; set; }

        public FbUsers()
        {
            ListOfUsers = new List<User>();
        }
    }

}
