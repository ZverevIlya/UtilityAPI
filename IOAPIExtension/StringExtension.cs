using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOAPIExtension
{
    public static class StringExtension
    {
        /// <summary>
        /// Answers true if this String is either null or empty.
        /// </summary>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Answers true if this String is neither null or empty.
        /// </summary>
        public static bool HasValue(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Returns the first non-null/non-empty parameter when this String is null/empty.
        /// </summary>
        public static string IsNullOrEmptyReturn(this string s, params string[] otherPossibleResults)
        {
            if (s.HasValue())
            {
                return s;
            }

            if (otherPossibleResults == null)
            {
                return "";
            }

            foreach (var t in otherPossibleResults)
            {
                if (t.HasValue())
                {
                    return t;
                }
            }

            return "";
        }
    }
}
