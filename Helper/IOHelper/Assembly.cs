using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Reflection = System.Reflection;

namespace Helper.IOHelper
{
    public static class Assembly
    {
        /// <summary>
        /// Get calling assembly file version information.
        /// </summary>
        /// <returns></returns>
        public static FileVersionInfo GetFileVersionInfo()
        {
            return FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetCallingAssembly().Location);
        }

        /// <summary>
        /// Get the assembly "Helper" file version information.
        /// </summary>
        /// <returns></returns>
        public static FileVersionInfo GetAssemblyFileVersionInfo()
        {
            return FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
