using System;
using System.Collections.Generic;
using System.IO;

using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests
{
    [TestClass]
    public class UserDbTest
    {
        public string WorkFolder => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testUserDbWorkDir");

        public string DbFile => "80KTest.db";

        /// <summary>
        /// Setup Test.
        /// Cleas test folder or creats it.
        /// </summary>
        public void SetupTest()
        {
            if (Directory.Exists(WorkFolder))
            {
                // Clean dir
                var di = new DirectoryInfo(WorkFolder);
                foreach (var file in di.EnumerateFiles())
                {
                    file.Delete();
                }

                foreach (var directory in di.EnumerateDirectories())
                {
                    Directory.Delete(directory.FullName, true);
                }
            }
            else
            {
                Directory.CreateDirectory(WorkFolder);
            }
        }

        /// <summary>
        /// TestMethod that checks functionality for simple methods like, Exist, Find, GetRandomUser.
        /// </summary>
        [TestMethod]
        public void SimpleMethodsTest()
        {
            SetupTest();
            var myTestDb = new FbUserDb(DbFile);

            // this user exists in any case. The testfile includes 80K user with incremental userIds starting at 1.
            var randomUserId = Random.Shared.Next(1, myTestDb.CountOfUsers).ToString();

            // Test Exist
            Assert.IsTrue(myTestDb.Exists(randomUserId));
            // Test find
            var user = myTestDb.Find(item => item.Id == randomUserId);
            // Test second Exist
            Assert.IsTrue(myTestDb.Exists(user));
            // Test second find
            var user1 = myTestDb.Find(randomUserId);
            Assert.AreEqual(user1.Id, user.Id);
            // Test get random user
            var randomUser = myTestDb.GetRandomUser();
            Assert.IsTrue(myTestDb.Exists(randomUser));
        }

        /// <summary>
        /// This test was only used to create a large database file.
        /// </summary>
        [TestMethod]
        public void SerializeAndDeserializeTest()
        {
            SetupTest();
            FbUserDb myDB = new FbUserDb();
            for (int i = 1; i <= 10; i++)
            {
                var groups = new List<Group>();
                var countOfGroups = Random.Shared.Next(10);
                for (int j = 1; j < countOfGroups; j++)
                {
                    var groupId = Random.Shared.Next(20).ToString();
                    var newGroup = new Group() { GroupId = groupId, GroupName = groupId };
                    FbUserDb.UpdateGroup(groups, newGroup);
                }

                var newUser = new User
                              {
                                  Name = $"{i:D7}",
                                  Groups = groups,
                                  Id = i.ToString(),
                                  Email = string.Empty
                              };

                myDB.Update(newUser);
            }

            var tmpDb = "Temp_Db.deleteMe";
            myDB.SerializeDbToFile(Path.Combine(WorkFolder, tmpDb));

            var theNewFabDataBase = new FbUserDb();
            Assert.AreEqual(theNewFabDataBase.CountOfUsers, 0);
            theNewFabDataBase.DeserializeFromFile(Path.Combine(WorkFolder, tmpDb));
            Assert.AreEqual(theNewFabDataBase.CountOfUsers, 10);
            Assert.IsTrue(theNewFabDataBase.Find(item => item.Id == "3")?.Id == "3");
            File.Delete(Path.Combine(WorkFolder, tmpDb));
        }

        /// <summary>
        /// This test was only used to create a large database file.
        /// </summary>
        // [TestMethod]
        public void CreateBigDb()
        {
            SetupTest();
            FbUserDb myDB = new FbUserDb();
            for (int i = 1; i <= 80000; i++)
            {
                var groups = new List<Group>();
                var countOfGroups = Random.Shared.Next(10);
                for (int j = 1; j < countOfGroups; j++)
                {
                    var groupId = Random.Shared.Next(20).ToString();
                    var newGroup = new Group() { GroupId = groupId, GroupName = groupId };
                    FbUserDb.UpdateGroup(groups, newGroup);
                }
                
                var newUser = new User
                              {
                                  Name = $"{i:D7}",
                                  Groups = groups,
                                  Id = i.ToString(),
                                  Email = string.Empty
                              };

                myDB.Update(newUser);
            }
            myDB.SerializeDbToFile(Path.Combine(WorkFolder, DbFile));
        }
    }
}
