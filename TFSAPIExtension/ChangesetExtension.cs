using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSAPIExtension
{
    public static class ChangesetExtension
    {
        public static IEnumerable<Changeset> QueryHistory(this Changeset changeset, string path)
        {
            return changeset.VersionControlServer.QueryHistory(path, ChangesetVersionSpec.Latest, 0,
                    RecursionType.Full, null, null, null, Int32.MaxValue,
                    false, true).Cast<Changeset>();
        }

        public static IEnumerable<Changeset> QueryHistory(
            this Changeset changeset,
            string path,
            DateTime fromDatetime,
            DateTime toDatetime)
        {
            return changeset.QueryHistory(path).Where(set =>
                set.CreationDate.CompareTo(fromDatetime) >= 0 && set.CreationDate.CompareTo(toDatetime) <= 0);
        }

        public static WorkItem[] GetCodeItems(this Changeset changeset)
        {
            return changeset.WorkItems.Where(workitem => 
                workitem.Type.Name.Equals(WorkItemTypeNames.CodeItem)).ToArray();
        }
    }
}
