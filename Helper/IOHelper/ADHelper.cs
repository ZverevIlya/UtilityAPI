using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;

namespace Helper.IOHelper
{
    public class ADHelper
    {
        public static string GetUserEmail(string displayName)
        {
            string email = null;

            DirectorySearcher searcher = new DirectorySearcher();
            searcher.PropertiesToLoad.Add("mail");
            searcher.Filter = string.Format("(&(displayName={0})(objectCategory=person)((objectClass=user)))", displayName);
            SearchResultCollection result = searcher.FindAll();

            foreach (SearchResult item in result)
            {
                ResultPropertyValueCollection values = item.Properties["mail"];
                foreach (var propertyValue in values)
                {
                    return email = propertyValue.ToString();
                }
            }

            return email;
        }
    }
}
