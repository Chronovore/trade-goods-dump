using System.IO;

namespace TradeGoodsDump.Logging
{
    /// <summary>
    /// Singleton for logging file resource.
    /// </summary>
    internal class Logger
    {
        private const string ERROR_TAG = "#ERR#";
        private const string INFO_TAG = "[INF]";
        
        private static Logger _instance;
        
        private readonly StreamWriter _writer;

        public static void Close()
        {
            _instance?._writer.Close();
        }
        
        public static void Info(string message)
        {
            Log(INFO_TAG, message);
        }
        
        public static void Error(string message)
        {
            Log(ERROR_TAG, message);
        }
        
        public Logger(string directory, string fileName)
        {
            if (_instance == null)
            {
                Directory.CreateDirectory(directory);
                _writer = File.CreateText($"{directory}/{fileName}");
                _instance = this;
            }
            else
            {
                Error("Tried to create duplicate logging Instance!");
            }
        }
        
        private static void Log(string tag, string message)
        {
            if (_instance == null) return;
            
            _instance._writer.WriteLine($"{tag} {message}");
            _instance._writer.Flush();
        }
    }
}