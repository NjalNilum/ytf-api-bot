namespace Common
{
    public static class Directories
    {
        /// <summary>
        /// Root folder of Bundle.
        /// C:\App-YTF-Service
        /// </summary>
        public static string BundleRootFolder => @"C:\App-YTF-Service";

        /// <summary>
        /// ServiceWorkDir is a subfolder in BundleRootFolder.
        /// C:\App-YTF-Service\ServiceWorkDir
        /// </summary>
        public static string ServiceWorkDir => Path.Combine(BundleRootFolder, "ServiceWorkDir");

        /// <summary>
        /// LogDir is a subfolder in BundleRootFolder.
        /// C:\App-YTF-Service\Log
        /// </summary>
        public static string LogDir => Path.Combine(BundleRootFolder, "Log");
    }
}
