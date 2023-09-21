namespace Common
{

    public class Group
    {
        public string GroupName { get; set; }

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
        public string Name { get; set; }

        /// <summary>
        /// Email if available
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// FB id of user
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Groups actual user is a member of
        /// </summary>
        public List<string> Groups { get; set; }

        public User()
        {
            Name = string.Empty;
            Email = string.Empty;
            Id = string.Empty; 
            Groups = new List<string>();
        }
    }
}
