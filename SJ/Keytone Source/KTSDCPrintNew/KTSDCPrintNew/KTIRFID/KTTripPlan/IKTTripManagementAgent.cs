using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.RFIDGlobal;
using KTone.Core.KTIRFID;

namespace KTone.Core.KTIRFID
{
    public interface IKTTripManagementAgent
    {
        IKTTripManager GetTripManager(int dataOwnerId);
    }
}
