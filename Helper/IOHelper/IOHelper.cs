using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Helper.IOHelper
{
    public sealed class IOHelper
    {
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                
            }
        }

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
            return date.ToString("yyyy-MM-dd");
        }

        public static void Delete(string path, bool recursive = false)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void EmptyDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                DeleteFiles(Directory.GetFiles(path, "*", SearchOption.AllDirectories));
                DeletDirectories(Directory.GetDirectories(path));
            }
        }

        public static void DeletDirectories(string[] dirs)
        {
            foreach (string dname in dirs)
            {
                Delete(dname, true);
            }
        }

        public static void DeleteFiles(string[] files)
        {
            foreach (string fname in files)
            {
                Delete(fname);
            }
        }

        public static void DeleteFile(string filename, bool force = true)
        {
            try
            {
                Delete(filename);
            }
            catch (UnauthorizedAccessException)
            {
                FileInfo file = new FileInfo(filename);
                file.IsReadOnly = false;
                Delete(filename);
            }
        }

        public static string GetTemporaryFileName()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
        }
    }
}
