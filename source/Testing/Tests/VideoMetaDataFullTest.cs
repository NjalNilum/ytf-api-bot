using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleLogger;
using System;
using System.IO;
using YoutubeApi;

namespace Tests
{
    [TestClass]
    public class VideoMetaDataFullTest
    {
        public static string WorkFolder => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testVideoMetaDataWorkDir");

        [TestMethod]
        public void TestGetReadableDescription()
        {
            var videoId = "ZWHBsKm9Egk";

            var localLogger = new Logger("yt_test.log");
            localLogger.LogDebug("Test was set up.");

            var youtubeConfig = ConfigHelper.LoadFromJsonFile<YoutubeConfig>(@"YoutubeConfig.json");
            var youtubeApi = new YoutubeApi.YoutubeApi(youtubeConfig.ApiKey4Testing, WorkFolder, localLogger);
            var video = youtubeApi.GetVideoMetaData(videoId).Result;
            var readableDescription = video.GetReadableDescription();

            Assert.IsTrue(readableDescription.Contains(video.ChannelTitle));
            Assert.IsTrue(readableDescription.Contains("German"));
        }
    }
}
