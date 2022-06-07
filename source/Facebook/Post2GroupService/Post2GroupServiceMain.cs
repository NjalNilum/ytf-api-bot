using SimpleLogger;
using Common;
using FacebookAutomation;

Logger myLogger = new Logger("FacebookLog.log", Directories.LogDir);
myLogger.LogInfo("I bims, der Post2GroupService");

try
{
    var serviceWorkDir = Directories.ServiceWorkDir;
    var facebookConfig = ConfigHelper.LoadFromJsonFile<FacebookConfig>(@"FacebookConfig.json");

    myLogger.LogInfo("Start Post2GroupServiceWorker");
    var fbManager = new FbManager(serviceWorkDir, facebookConfig, MyCallback);
    _ = fbManager.StartFbWorker01();
    //_ = fbManager.StartGaymanWorker();

    while (Console.ReadKey().Key != ConsoleKey.E)
    {
        Console.WriteLine();
        Console.WriteLine("Hit e to exit");
    }

    fbManager.StopAllWorker();
    Console.WriteLine("Post2GroupService stopped");
    Thread.Sleep(TimeSpan.FromSeconds(10));
}
catch (Exception ex)
{
    myLogger.LogError(ex.Message);
}



void MyCallback(string arg1, string message)
{
    // _ = telegramManager.SendDebugMessageAsync(arg1 + ": " + message);
}