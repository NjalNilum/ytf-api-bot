using System.Collections.Generic;
using Common;
using Common.Database;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests
{
    [TestClass]
    public class UserDbTest
    {
        public User CreateUser(string id)
        {
            return new User
            {
                Id = $"i_bims_id_{id}",
                Name = $"My name is {id}",
                Email = string.Empty,
                Groups = new List<string> { "group01", "group02" }
            };
        }

        public List<Group> MakeGroups(User theUser)
        {
            var result = new List<Group>();
            theUser.Groups.ForEach(group => result.Add(new Group { GroupId = group, GroupName = $"Names is {group}" }));
            return result;
        }

        /// <summary>
        /// Creates DB with 10 user and checks for count of documents and if users exist and not exist.
        /// </summary>
        [TestMethod]
        public void SimpleDbUpdateTest()
        {
            var myDB = new FbUserDb("the-test-db");

            for (int i = 0; i < 10; i++)
            {
                var user = CreateUser($"{i:D3}");
                myDB.CreateOrOverwrite(user);
                MakeGroups(user).ForEach(item => myDB.CreateOrOverwrite(item));
            }

            var countOfUsers = myDB.CountOfUsers;
            Assert.AreEqual(countOfUsers, 10);

            var aUser = myDB.Find("i_bims_id_007");
            Assert.IsNotNull(aUser);
            aUser = myDB.Find("huan-son-006");
            Assert.IsNull(aUser);

            myDB.DropDatabase();
        }

        /// <summary>
        /// Checks the update method. 
        /// </summary>
        [TestMethod]
        public void TestUpdateMethod()
        {
            var myDB = new FbUserDb("the-test-db");

            myDB.Update("111", "My name is ringading", new Group { GroupId = "001", GroupName = "the super group" });
            myDB.Update("112", "My name is other", new Group { GroupId = "003", GroupName = "the not group" });

            var user = myDB.Find("111");
            Assert.AreEqual(user.Groups.Count, 1);
            Assert.AreEqual(user.Name, "My name is ringading");

            myDB.Update("111", "My new name is better", new Group { GroupId = "999", GroupName = "new groups are 999" }, "name@nowhere.none");

            user = myDB.Find("111");
            Assert.AreEqual(user.Groups.Count, 2);
            Assert.AreEqual(user.Name, "My new name is better");

            myDB.DropDatabase();
        }

        [TestMethod]
        public void StressTest()
        {
            var myDB = new FbUserDb("the-test-db");

            var size = 3000; // Enlarge if want to play with it.
            for (int i = 0; i < size; i++)
            {
                var user = CreateUser($"{i:D6}");
                myDB.CreateOrOverwrite(user);
                MakeGroups(user).ForEach(item => myDB.CreateOrOverwrite(item));
            }

            var countOfUsers = myDB.CountOfUsers;
            Assert.AreEqual(countOfUsers, size);

            var aUser = myDB.Find("i_bims_id_000007");
            Assert.IsNotNull(aUser);
            aUser.Email = "my@my.my";
            myDB.CreateOrOverwrite(aUser);
            var sameUser = myDB.Find(aUser.Id);

            aUser = myDB.Find("huan-son-006");
            Assert.IsNull(aUser);

            myDB.DropDatabase();
        }

        [TestMethod]
        public void TestGetRandomUser()
        {
            var myDB = new FbUserDb("the-test-db");

            var size = 100; // Enlarge if want to play with it.
            for (int i = 0; i < size; i++)
            {
                var user = CreateUser($"{i:D6}");
                myDB.CreateOrOverwrite(user);
                MakeGroups(user).ForEach(item => myDB.CreateOrOverwrite(item));
            }

            var rndUser = myDB.GetRandomUser(1);
            var users = myDB.GetRandomUser(15);

            Assert.IsNotNull(rndUser);
            Assert.AreEqual(users.Count, 15);
            myDB.DropDatabase();
        }
    }
}
