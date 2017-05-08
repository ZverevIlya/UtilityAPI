namespace TFSAPIExtension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;
    using Microsoft.TeamFoundation.VersionControl.Client;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ShelvesetExtension
    {
        public static WorkItem[] LinkedBugs(this Shelveset shelveset)
        {
            return shelveset.LinkedWorkItems(WorkItemTypeNames.Bug);
        }

        public static WorkItem[] LinkedWorkItems(this Shelveset shelveset, string workItemTypeName)
        {
            return shelveset.WorkItemInfo.Where(x => x.WorkItem.Type.Name == workItemTypeName)
                .Select(x => x.WorkItem).ToArray();
        }
    }
}
