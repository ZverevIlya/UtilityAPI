namespace TFSAPIExtension
{
    using System;
    using Microsoft.TeamFoundation.Build.Client;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class BuildExtension
    {
        public static IBuildAgent ReservedAgent(this IBuildDetail detail)
        {
            IBuildAgent current = null;
            foreach (IBuildAgent agent in detail.BuildController.Agents)
            {
                if (Uri.Equals(agent.ReservedForBuild, detail.Uri))
                {
                    current = agent;
                }
            }
            return current;
        }
    }
}
