using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO = System.IO;

namespace Helper.IOHelper
{
    public sealed class Path
    {
        public static string GetProgramFilesPathX86()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        }

        public static string GetProgramFilesPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        }

        public static string GetSystemDriver()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        }
    }
}
