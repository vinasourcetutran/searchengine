using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity
{
    public interface IQueueable<PrimakeyType>
    {
        PrimakeyType EntityId { get; set; }
        int Status { get; set; }
        DateTime NextRun { get; set; }
        int Priority { get; set; }
        int StatusCode { get; set; }
        string StatusDescription { get; set; }
    }
}
