
using SimpleLogger;
using Common;
using TelegramApi;

Logger myLogger = new Logger("TelegramLog.log", Directories.LogDir);
myLogger.LogInfo("I bims, der TelegramService");
myLogger.LogInfo($"ServiceWorkDir: {Directories.ServiceWorkDir}");

try
{
    var serviceWorkDir = Directories.ServiceWorkDir;
    var telegramConfig = ConfigHelper.LoadFromJsonFile<TelegramConfig>(@"TelegramConfig.json");

    myLogger.LogInfo("Start Telegram Worker");
    var telegramManager = new TelegramManager(telegramConfig, VideoMetaDataFull.VideoFileSearchPattern, serviceWorkDir);
    //_ = telegramManager.StartSomeBotToHaufenChat();
    _ = telegramManager.StartBlackMetaloidToBmChat();
    _ = telegramManager.StartGermanBlackMetaloidToGbmChat();

    while (Console.ReadKey().Key != ConsoleKey.E)
    {
        Console.WriteLine();
        Console.WriteLine("Hit e to exit");
    }

    telegramManager.StopAllWorker();
    Console.WriteLine("TelegramService stopped");
    Thread.Sleep(TimeSpan.FromSeconds(10));
}
catch (Exception ex)
{
    myLogger.LogError(ex.Message);
}