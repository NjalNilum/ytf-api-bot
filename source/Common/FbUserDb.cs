
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common
{
    public class FbUserDb
    {
        private FbUsers m_FbUsers;

        /// <summary>
        /// Returns the amount of users in the database.
        /// </summary>
        public int CountOfUsers => this.m_FbUsers.ListOfUsers.Count; // None of the internally accessed properties can be null.

        public FbUserDb()
        {
            this.m_FbUsers = new FbUsers();
        }

        public FbUserDb(string pathToFile)
        {
            this.m_FbUsers = new FbUsers();
            DeserializeFromFile(pathToFile);
        }

        /// <summary>
        /// Encapsulates the find method.
        /// See docu to find.
        /// </summary>
        public User? Find(Predicate<User> match)
        {
            return this.m_FbUsers.ListOfUsers.Find(match);
        }

        /// <summary>
        /// Returns user with id userId, if exists, otherwise null.
        /// </summary>
        public User? Find(string userId)
        {
            return this.m_FbUsers.ListOfUsers.Find(item => item.Id == userId);
        }

        /// <summary>
        /// Check if user exists.
        /// </summary>
        /// <param name="user">User to be checked</param>
        /// <returns>True, if user exists, otherwise false.</returns>
        public bool Exists(User user)
        {
            return this.m_FbUsers.ListOfUsers.Exists(item => item.Id == user.Id);
        }

        /// <summary>
        /// Check if user exists.
        /// </summary>
        /// <param name="userId">Id of user to be checked</param>
        /// <returns>True, if user exists, otherwise false.</returns>
        public bool Exists(string userId)
        {
            return this.m_FbUsers.ListOfUsers.Exists(item => item.Id == userId);
        }

        /// <summary>
        /// Updates the database with the transferred user. User is newly created if it does not exist,
        /// otherwise name, groups and email address are updated.
        /// </summary>
        /// <param name="user">User to be added/updated</param>
        public void Update(User user)
        {
            var existingUser = this.m_FbUsers.ListOfUsers.Find(item => item.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.Name = user.Name;  
                user.Groups.ForEach(item => UpdateGroup(existingUser.Groups, item));
            }
            else
            {
                this.m_FbUsers.ListOfUsers.Add(user);
            }
        }

        /// <summary>
        /// Updates a list of groups with the passed group. If the group exists, the group name is updated,
        /// otherwise the group is inserted as a new element in the list.
        /// </summary>
        /// <param name="groups">List of groups</param>
        /// <param name="group">Group to be added/updated</param>
        public static void UpdateGroup(List<Group> groups, Group group)
        {
            var existingGroup = groups.Find(item => item.GroupId == group.GroupId);

            if (existingGroup != null)
            {
                existingGroup.GroupName = group.GroupName;
            }
            else
            {
                groups.Add(group);
            }
        }

       

        /// <summary>
        /// Returns a random user.
        /// </summary>
        public User GetRandomUser()
        {
            return this.m_FbUsers.ListOfUsers[Random.Shared.Next(CountOfUsers)];
        }

        /// <summary>
        /// Writes the database into a file.
        /// The entire database is written to the file. There is no differential matching.
        /// An already existing file is overwritten.
        /// </summary>
        /// <param name="fullPath">Full path to write the file in.</param>
        public void SerializeDbToFile(string fullPath)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            File.WriteAllText(fullPath, JsonSerializer.Serialize(this.m_FbUsers, options));
        }

        /// <summary>
        /// Imports the database from file and write into UserDb.
        /// </summary>
        /// <param name="fullPath">Full path to database file</param>
        /// <exception cref="NullReferenceException">Thrown if deserialization failed.</exception>
        public void DeserializeFromFile(string fullPath)
        {
            var resultList = JsonSerializer.Deserialize<FbUsers>(File.ReadAllText(fullPath));
            this.m_FbUsers = resultList ?? throw new NullReferenceException("Deserialization failed.");
        }
    }
}
