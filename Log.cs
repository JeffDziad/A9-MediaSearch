using System;
using System.IO;
using NLog.Web;

namespace A8_MediaSearch
{
    static class Log
    {
        private static string path = Directory.GetCurrentDirectory() + "\\nlog.config";
        private static readonly NLog.Logger Logger = NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

        public static void log(string msg, Exception ex)
        {
            Logger.Warn(msg);
            Logger.Debug(ex.StackTrace);
        }

        public static void logX(string msg)
        {
            Logger.Warn(msg);
        }
    }
}