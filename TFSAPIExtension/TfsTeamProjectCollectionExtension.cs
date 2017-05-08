using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSAPIExtension
{
    public static class TfsTeamProjectCollectionExtension
    {
        public static WorkItemStore WorkItemStore(this TfsTeamProjectCollection tpc)
        {
            return tpc.GetService<WorkItemStore>();
        }

        public static VersionControlServer VersionControlServer(this TfsTeamProjectCollection tpc)
        {
            return tpc.GetService<VersionControlServer>();
        }

        public static IBuildServer BuildServer(this TfsTeamProjectCollection tpc)
        {
            return tpc.GetService<IBuildServer>();
        }

        public static IGroupSecurityService GroupSecurityService(this TfsTeamProjectCollection tpc)
        {
            return tpc.GetService<IGroupSecurityService>();
        }

        public static IEventService EventService(this TfsTeamProjectCollection tpc)
        {
            return tpc.GetService<IEventService>();
        }

        public static IAuthorizationService AuthorizationService(this TfsTeamProjectCollection tpc)
        {
            return tpc.GetService<IAuthorizationService>();
        }

        public static ICommonStructureService CommonStructureService(this TfsTeamProjectCollection tpc)
        {
            return tpc.GetService<ICommonStructureService>();
        }

        public static TswaClientHyperlinkService TswaClientHyperlinkService(this TfsTeamProjectCollection tpc)
        {
            return tpc.GetService<TswaClientHyperlinkService>();
        }
    }
}
