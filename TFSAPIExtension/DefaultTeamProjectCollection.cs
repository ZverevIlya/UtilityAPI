using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.TeamFoundation.Client;
using System.Configuration;

namespace TFSAPIExtension
{
    public class DefaultTeamProjectCollection
    {
        private const string TFS_COM = "http://tfs.com:8080/tfs/defaultcollection";

        static DefaultTeamProjectCollection()
        {
            DefaultUri = new Uri(TFS_COM);
        }

        /// <summary>
        /// http://tfs.com:8080/tfs/defaultcollection
        /// </summary>
        public static Uri DefaultUri
        {
            get;
            set;
        }

        /// <summary>
        /// Connect to http://tfs.com:8080/tfs/defaultcollection
        /// </summary>
        /// <returns></returns>
        public static TfsTeamProjectCollection GetDefaultTeamProjectCollection()
        {
            return GetTeamProjectCollection(DefaultUri);
        }

        /// <summary>
        /// Connect to specify TFS team project collection
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static TfsTeamProjectCollection GetTeamProjectCollection(Uri url)
        {
            TfsTeamProjectCollection tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(url);
            tpc.EnsureAuthenticated();
            return tpc;
        }

        /// <summary>
        /// Connect to specify TFS team project collection via Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static TfsTeamProjectCollection GetTeamProjectCollection(string url)
        {
            return GetTeamProjectCollection(new Uri(url));
        }

        /// <summary>
        /// Connect to http://tfs.com:8080/tfs/defaultcollection by specify credential.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static TfsTeamProjectCollection GetTeamProjectCollection(ICredentials credentials)
        {
            return GetTeamProjectCollection(DefaultUri, credentials);
        }

        public static TfsTeamProjectCollection GetTeamProjectCollection(string userName, string password)
        {
            ICredentials credential = new NetworkCredential(userName, password);
            return GetTeamProjectCollection(credential);
        }

        /// <summary>
        /// Connect to specify TFS team project collection by specify credential.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static TfsTeamProjectCollection GetTeamProjectCollection(Uri url, ICredentials credentials)
        {
            TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(url, credentials);
            tpc.EnsureAuthenticated();
            return tpc;
        }

        /// <summary>
        /// Connect to specify TFS team project collection via Url by specify credential.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static TfsTeamProjectCollection GetTeamProjectCollection(string url, ICredentials credentials)
        {
            return GetTeamProjectCollection(new Uri(url), credentials);
        }

        public static TfsTeamProjectCollection GetTeamProjectCollection(string url, string userName, string password)
        {
            ICredentials credential = new NetworkCredential(userName, password);
            return GetTeamProjectCollection(url, credential);
        }
    }
}

