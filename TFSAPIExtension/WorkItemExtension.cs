using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSAPIExtension
{
    public static class WorkItemExtension
    {
        public static bool IsVModelType(this WorkItem workitem)
        {
            bool isVType = false;
            switch (workitem.Type.Name)
            {
                case VKeyTypes.RS:
                case VKeyTypes.SFS:
                case VKeyTypes.SSFS:
                case VKeyTypes.DS:
                case VKeyTypes.STS:
                case VKeyTypes.TestCase:
                    isVType = true;
                    break;
                default:
                    break;
            }

            return isVType;
        }

        [Obsolete]
        public static Dictionary<int, WorkItem> History(this WorkItem workitem)
        {
            Dictionary<int, WorkItem> items = new Dictionary<int, WorkItem>();

            for (int i = 1; i <= workitem.Rev; i++)
            {
                items.Add(i, workitem.Store.GetWorkItem(workitem.Id, i));
            }

            return items;
        }

        /// <summary>
        /// Get all revision of this work item.
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns></returns>
        public static IEnumerable<WorkItem> GetRevisions(this WorkItem workItem)
        {
            /// workitem revision index start from 0, revision start from 1
            return from rev in workItem.Revisions.Cast<Revision>()
                   select workItem.Store.GetWorkItem(workItem.Id, rev.Index + 1);
        }

        /// <summary>
        /// Get specify revision work item.
        /// </summary>
        /// <param name="workItem"></param>
        /// <param name="revision"></param>
        /// <returns></returns>
        public static WorkItem GetRevision(this WorkItem workItem, int revision)
        {
            return workItem.Store.GetWorkItem(workItem.Id, revision);
        }

        #region WORK ITEM LINKS
        /// <summary>
        /// The strictest of link end type is "Related"
        /// </summary>
        /// <param name="workitem"></param>
        /// <returns></returns>
        public static RelatedLink[] StrictestRelatedLinks(this WorkItem workitem)
        {
            return RelatedLinks(workitem)
                .Where(link => (link.LinkTypeEnd != null)
                    && (link.LinkTypeEnd.Name == WorkItemLinkTypeEnds.Related))
                .ToArray();
        }

        public static WorkItem[] StrictestRelated(this WorkItem workitem)
        {
            return (from item in StrictestRelatedLinks(workitem)
                    select workitem.Store.GetWorkItem(item.RelatedWorkItemId)).ToArray();
        }

        /// <summary>
        /// All link type is related
        /// </summary>
        /// <param name="workitem"></param>
        /// <returns></returns>
        public static RelatedLink[] RelatedLinks(this WorkItem workitem)
        {
            return workitem.Links.Cast<Link>()
                .Select<Link, RelatedLink>(link => link as RelatedLink)
                .Where<RelatedLink>(link => link != null)
                .ToArray();
        }

        public static WorkItem[] Related(this WorkItem workitem)
        {
            return (from item in RelatedLinks(workitem)
                    select workitem.Store.GetWorkItem(item.RelatedWorkItemId)).ToArray();
        }

        public static bool HasTracedFrom(this WorkItem workitem)
        {
            return TracedFromLinks(workitem).Count() != 0;
        }

        public static RelatedLink[] TracedFromLinks(this WorkItem workitem)
        {
            return RelatedLinks(workitem)
                .Where(link => (link.LinkTypeEnd != null)
                    && (link.LinkTypeEnd.Name == WorkItemLinkTypeEnds.TracedFrom))
                .ToArray();
        }

        public static WorkItem[] TracedFrom(this WorkItem workitem)
        {
            return TracedFromLinks(workitem)
                .Select(x => workitem.Store.GetWorkItem(x.RelatedWorkItemId))
                .Where(x => x.IsVModelType())
                .ToArray();
        }

        public static bool HasTracedTo(this WorkItem workitem)
        {
            return TracedToLinks(workitem).Count() != 0;
        }

        public static RelatedLink[] TracedToLinks(this WorkItem workitem)
        {
            return RelatedLinks(workitem)
                .Where(link => (link.LinkTypeEnd != null)
                    && (link.LinkTypeEnd.Name == WorkItemLinkTypeEnds.TracedTo))
                .ToArray();
        }

        public static WorkItem[] TracedTo(this WorkItem workitem)
        {
            return TracedToLinks(workitem)
                .Select(x => workitem.Store.GetWorkItem(x.RelatedWorkItemId))
                .Where(x => x.IsVModelType()).ToArray();
        }

        public static RelatedLink[] TestedByLinks(this WorkItem workitem)
        {
            return RelatedLinks(workitem)
                .Where(link => (link.LinkTypeEnd != null)
                    && (link.LinkTypeEnd.Name == WorkItemLinkTypeEnds.TestedBy))
                .ToArray();
        }

        public static RelatedLink[] TestsLinks(this WorkItem workitem)
        {
            return RelatedLinks(workitem)
                .Where(link => link.LinkTypeEnd != null && link.LinkTypeEnd.Name == "Tests").ToArray();
        }

        public static WorkItem[] TestedBy(this WorkItem workitem)
        {
            return TestedByLinks(workitem)
                .Select(link => workitem.Store.GetWorkItem(link.RelatedWorkItemId))
                .Where(x => x.IsVModelType())
                .ToArray();
        }

        public static WorkItem[] Tests(this WorkItem workitem)
        {
            return TestsLinks(workitem)
                .Select(link => workitem.Store.GetWorkItem(link.RelatedWorkItemId))
                .Where(x => x.IsVModelType()).ToArray();
        }

        #endregion

        #region Add Links

        private static void AddLink(this WorkItem workitem, WorkItem other, WorkItemLinkTypeEnd linkType)
        {
            workitem.Links.Add(new RelatedLink(linkType, other.Id));
        }

        public static void AddRelatedLink(this WorkItem workitem, WorkItem other)
        {
            WorkItemLinkTypeEnd typeEnd = workitem.Store.WorkItemLinkTypes.LinkTypeEnds[WorkItemLinkTypeEnds.Related];
            AddLink(workitem, other, typeEnd);
        }

        public static void AddTracedToLink(this WorkItem workitem, WorkItem tracedTo)
        {
            WorkItemLinkTypeEnd typeEnd = workitem.Store.WorkItemLinkTypes.LinkTypeEnds[WorkItemLinkTypeEnds.TracedTo];
            AddLink(workitem, tracedTo, typeEnd);
        }

        public static void AddTracedFromLink(this WorkItem workitem, WorkItem tracedFrom)
        {
            WorkItemLinkTypeEnd typeEnd = workitem.Store.WorkItemLinkTypes.LinkTypeEnds[WorkItemLinkTypeEnds.TracedFrom];
            AddLink(workitem, tracedFrom, typeEnd);
        }

        public static void AddTestedByLink(this WorkItem workitem, WorkItem testCase)
        {
            WorkItemLinkTypeEnd typeEnd = workitem.Store.WorkItemLinkTypes.LinkTypeEnds[WorkItemLinkTypeEnds.TestedBy];
            AddLink(workitem, testCase, typeEnd);
        }

        public static void AddTestsLink(this WorkItem workitem, WorkItem testCase)
        {
            WorkItemLinkTypeEnd typeEnd = workitem.Store.WorkItemLinkTypes.LinkTypeEnds[WorkItemLinkTypeEnds.Tests];
            AddLink(workitem, testCase, typeEnd);
        }

        #endregion

        #region External Links

        private static void AddExternalLink(this WorkItem workitem, string registeredType, string uri)
        {
            RegisteredLinkType linkType = workitem.Store.RegisteredLinkTypes[registeredType];
            workitem.Links.Add(new ExternalLink(linkType, uri));
        }

        public static void AddHyperlink(this WorkItem workitem, string uri)
        {
            AddExternalLink(workitem, RegisteredLinkTypes.WorkitemHyperlink, uri);
        }

        public static void AddChangesetLink(this WorkItem workitem, string uri)
        {
            AddExternalLink(workitem, RegisteredLinkTypes.FixedinChangeset, uri);
        }

        public static bool HasExternalLink(this WorkItem workitem, string uri)
        {
            if (workitem.ExternalLinkCount == 0)
            {
                return false;
            }

            foreach (var link in workitem.Links)
            {
                ExternalLink ex = link as ExternalLink;
                if (ex == null)
                {
                    continue;
                }

                if (ex.LinkedArtifactUri.Equals(uri, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Remove Links
        public static void RemoveRelatedLink(this WorkItem workitem, RelatedLink link)
        {
            workitem.Links.Remove(link);
        }
        #endregion

        public static bool HasWorkItem(this IEnumerable<WorkItem> workitems, WorkItem workitem)
        {
            return (from item in workitems
                    where item.Id == workitem.Id
                    select item).ToArray().Length != 0;
        }


        public static bool HasWorkItem(this IEnumerable<WorkItem> workitems, int id)
        {
            return (from item in workitems
                    where item.Id == id
                    select item).ToArray().Length != 0;
        }

        public static string ClosedBy(this WorkItem workitem)
        {
            if (workitem.State != "Closed")
            {
                return string.Empty;
            }

            return workitem.Fields[WorkItemFieldNames.ClosedBy].Value.ToString();
        }
    }
}
