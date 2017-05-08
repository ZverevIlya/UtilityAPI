using System;
using System.IO;

namespace Helper.IOHelper
{
    [Obsolete]
    public sealed class PathHelper
    {
        #region AppData

        public static string GetCurrentDirectoryFilePath(string filename)
        {
            return Path.Combine(Environment.CurrentDirectory, filename);
        }

        public static string GetLocalApplicationDataPath(string folder)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), folder);
        }

        public static string GetLocalApplicationDataFullPath(string folder, string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), folder, filename);
        }

        public static string GetApplicationDataPath(string folder)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), folder);
        }

        public static string GetApplicationDataFullPath(string folder, string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), folder, filename);
        }

        public static void SaveToLocalApplicationData(string folder, string filename, string contents)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), folder);
            SaveTo(path, filename, contents);
        }

        public static void SaveToApplication(string folder, string filename, string contents)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), folder);
            SaveTo(path, filename, contents);
        }

        private static void SaveTo(string path, string filename, string contents)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(Path.Combine(path, filename), contents);
        }

        #endregion

        /// <summary>
        /// By default, '-' is split char.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetDateFileName(DateTime date)
        {
            return GetDateFileName(date, "-");
        }

        public static string GetDateFileName(DateTime date, string splitChar)
        {
            return date.ToShortDateString().Replace("/", splitChar);
        }
    }
}
