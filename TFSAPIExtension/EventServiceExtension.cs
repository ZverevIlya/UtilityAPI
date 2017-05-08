using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Framework.Client;

namespace TFSAPIExtension
{
    public static class EventServiceExtension
    {
        public static Subscription[] GetSubscriptions(this IEventService es, string userName, string eventType)
        {
            return (es.GetEventSubscriptions(userName).Where<Subscription>(ss => ss.EventType == eventType)).ToArray();            
        }
    }
}
