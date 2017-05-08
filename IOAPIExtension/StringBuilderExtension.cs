using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOAPIExtension
{
    public static class StringBuilderExtension
    {
        /// <summary>
        /// Answers true if this StringBuilder length is 0.
        /// </summary>
        public static bool HasValue(this StringBuilder sb)
        {
            return sb.Length != 0;
        }
    }
}
