using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AdWeb
{
    [HubName("hitCounter")]
    public class HitCounter : Hub
    {
        private static  int _hitCount=0;
        public void RecordHit()
        {
            _hitCount++;
            this.Clients.All.OnHotRecorderd(_hitCount);

        }
    }
}