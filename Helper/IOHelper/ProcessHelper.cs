using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Helper.IOHelper
{
    public sealed class ProcessHelper
    {
        public static void OpenByIE(string url)
        {
            string ie_path = @"C:\Program Files (x86)\Internet Explorer\iexplore.exe";
            if (!File.Exists(ie_path))
            {
                ie_path = @"C:\Program Files\Internet Explorer\iexplore.exe";
            }

            Run(ie_path, url);
        }

        [Obsolete]
        public static void OpenExplorer(string path)
        {
            string explorer = @"C:\Windows\explorer.exe";
            Run(explorer, path);
        }

        public static void OpenByExplorer(string path)
        {
            string explorer = @"C:\Windows\explorer.exe";
            Run(explorer, path);
        }

        public static Process Run(string filename, string args, bool useShellExecute = false, bool redirectStandardOutput = true)
        {
            ProcessStartInfo pinfo = new ProcessStartInfo(filename);
            pinfo.Arguments = args;
            pinfo.UseShellExecute = useShellExecute;

            ///You must set UseShellExecute to false if you want to set RedirectStandardOutput to true.
            /// Otherwise, reading from the StandardOutput stream throws an exception. 
            if (useShellExecute)
            {
                redirectStandardOutput = false;
            }
            pinfo.RedirectStandardOutput = redirectStandardOutput;

            return Process.Start(pinfo);
        }

        public static void KillAllProcess(string processName)
        {
            Process[] process = Process.GetProcessesByName(processName);

            foreach (var proc in process)
            {
                proc.Kill();
            }
        }

        public static Process GetProcessById(int pid)
        {
            Process p;

            try
            {
                p = Process.GetProcessById(pid);
            }
            catch (ArgumentException)
            {
                // not found process id, do nothing.
                p = null;
            }

            return p;
        }

        public static void KillProcess(int pid)
        {
            Process p = GetProcessById(pid);
            if (p != null)
            {
                p.Kill();
            }
        }
    }
}
