using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Common.Database
{
    public class FbUserDb
    {
        private readonly MongoClient mongoClient;
        private readonly string internalDataBaseName;
        private readonly IMongoCollection<Group> groups;
        private readonly IMongoCollection<User> users;

        /// <summary>
        /// Returns the amount of users in the database.
        /// </summary>
        public long CountOfUsers => users.CountDocuments(FilterDefinition<User>.Empty);

        public FbUserDb(string nameOfDb = "facebook-users")
        {
            mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            internalDataBaseName = nameOfDb;
            var internalDataBase = mongoClient.GetDatabase(internalDataBaseName);
            groups = internalDataBase.GetCollection<Group>("groups");
            users = internalDataBase.GetCollection<User>("users");
        }

        /// <summary>
        /// Drops/Delets the database from database server.
        /// </summary>
        public void DropDatabase()
        {
            mongoClient.DropDatabase(internalDataBaseName);
        }
        /// <summary>
        /// Returns user with id userId, if exists, otherwise null.
        /// </summary>
        public User? Find(string userId)
        {
            return users.Find(Builders<User>.Filter.Eq(nameof(User.Id), userId)).FirstOrDefault();
        }

        /// <summary>
        /// Updates the database with the transferred user. User is newly created if it does not exist,
        /// otherwise name, groups and email address will be overwritten.
        /// </summary>
        /// <param name="user">User to be added/updated</param>
        public void CreateOrOverwrite(User user)
        {
            var theFilter = Builders<User>.Filter.Eq(nameof(User.Id), user.Id);
            var updateDef = Builders<User>.Update
                                          .Set(nameof(User.Id), user.Id)
                                          .Set(nameof(User.Name), user.Name)
                                          .Set(nameof(User.Email), user.Email)
                                          .Set(nameof(User.Groups), user.Groups);
            var options = new UpdateOptions
            {
                IsUpsert = true
            };
            var result = users.UpdateOne(theFilter, updateDef, options);
        }

        /// <summary>
        /// Updates the database with the transferred user. User is newly created if it does not exist,
        /// otherwise name, groups and email address will be overwritten.
        /// </summary>
        /// <param name="user">User to be added/updated</param>
        private void UpdateWithoutGroups(User user)
        {
            var theFilter = Builders<User>.Filter.Eq(nameof(User.Id), user.Id);
            var updateDef = Builders<User>.Update
                                          .Set(nameof(User.Name), user.Name)
                                          .Set(nameof(User.Email), user.Email);
            var options = new UpdateOptions
            {
                IsUpsert = true
            };
            var result = users.UpdateOne(theFilter, updateDef, options);
        }

        /// <summary>
        /// Updates the group database with the transferred group. If the group exists, the group name is updated otherwise the group is inserted
        /// as a new element.
        /// </summary>
        /// <param name="group"></param>
        public void CreateOrOverwrite(Group group)
        {
            var theFilter = Builders<Group>.Filter.Eq(nameof(Group.GroupId), group.GroupId);
            var updateDef = Builders<Group>.Update.Set(nameof(Group.GroupName), group.GroupName);
            var options = new UpdateOptions
            {
                IsUpsert = true
            };
            var result = groups.UpdateOne(theFilter, updateDef, options);
        }

        /// <summary>
        ///  Super update method.
        /// 1 Creates user if it doesn't exist.
        /// 2 Updates the users group list only if theGroup is a new group.
        /// </summary>
        public void Update(string userId, string userName, Group theGroup, string email = "")
        {
            // Update groups care for ots own
            CreateOrOverwrite(theGroup);

            var theUser = Find(userId);
            if (theUser == null)
            {
                // Create and insert new user if it doesn't exist.
                theUser = new User
                {
                    Id = userId,
                    Name = userName,
                    Email = email,
                    Groups = new List<string> { theGroup.GroupId }
                };

                CreateOrOverwrite(theUser);
            }
            else
            {
                theUser.Name = userName;
                theUser.Email = email;
                UpdateWithoutGroups(theUser);
            }

            if (!theUser.Groups.Contains(theGroup.GroupId))
            {
                theUser.Groups.Add(theGroup.GroupId);
                var theFilter = Builders<User>.Filter.Eq(nameof(User.Id), userId);
                var updateDef = Builders<User>.Update.Set(nameof(User.Groups), theUser.Groups);
                var options = new UpdateOptions
                {
                    IsUpsert = true
                };
                var result = users.UpdateOne(theFilter, updateDef, options);
            }
        }

        /// <summary>
        /// Returns a a number of random users in db. Return null, if something went wrong.
        /// </summary>
        public List<User>? GetRandomUser(long numberOfRandomUsers)
        {
            return users.AsQueryable().Sample(numberOfRandomUsers).ToList();
        }
    }
}
