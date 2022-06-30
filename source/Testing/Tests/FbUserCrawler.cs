using Common;
using FacebookAutomation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleLogger;
using System;
using System.IO;
using System.Linq;
using YoutubeApi;
using FbAutomation = FacebookAutomation.FacebookAutomation;

namespace Tests
{
    [TestClass]
    public class FbUserCrawler
    {
        public string WorkFolder => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testFacebookWorkDir");

        [TestMethod]
        public void CrawlThroughSmallGroup()
        {
            var logger = new Logger("TestFacebookLogFile.log");
            var facebook = new FbAutomation(WorkFolder, logger);
            var facebookConfig = ConfigHelper.LoadFromJsonFile<FacebookConfig>(@"FacebookConfig.json");

            facebook.Login(facebookConfig.Users[0].Email, facebookConfig.Users[0].Pw);
            
            facebook.CrawlMembersOfFbGroup(facebookConfig.SkyrimGroups[0]);

            facebook.Dispose();
        }

       
    }
}
