using Common;
using SimpleLogger;
using YoutubeApi;

Logger myLogger = new Logger("YoutubeLog.log", Directories.LogDir);
myLogger.LogInfo("I bims, der YoutubeService");

try
{
    var serviceWorkDir = Directories.ServiceWorkDir;
    var youtubeConfig = ConfigHelper.LoadFromJsonFile<YoutubeConfig>(@"YoutubeConfig.json");

    myLogger.LogInfo("Start Youtube Worker");
    var youtubeApi = new YoutubeApi.YoutubeApi(youtubeConfig.ApiKey4Testing, serviceWorkDir, myLogger);
    var myYoutubeManager = new YtManager(youtubeApi, serviceWorkDir);
    _ = myYoutubeManager.StartYoutubeWorker(youtubeConfig.Channels, 10, MyCallback);

    while (Console.ReadKey().Key != ConsoleKey.E)
    {
        Console.WriteLine();
        Console.WriteLine("Hit e to exit");
    }

    myYoutubeManager.StopYoutubeWorker();
    Console.WriteLine("YoutubeWorker stopped");
    Thread.Sleep(TimeSpan.FromSeconds(10));
}
catch (Exception ex)
{
    myLogger.LogError(ex.Message);
}

void MyCallback(string arg1, string message)
{
    //   _ = telegramManager.SendDebugMessageAsync(arg1 + ": " + message);
}

