using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Text.RegularExpressions;

namespace TFSAPIExtension
{
    public static class FieldExtension
    {
        public const string HTML_TAG_PATTERN = "<.*?>";

        /// <summary>
        /// If field type is html, strip all html tags.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string StripHtmlValue(this Field field)
        {
            if (field.FieldDefinition.FieldType.ToString() == "Html")
            {
                var str = field.Value.ToString();
                return Regex.Replace(str, HTML_TAG_PATTERN, string.Empty);
            }
            else
            {
                return field.Value.ToString();
            }
        }
    }
}
