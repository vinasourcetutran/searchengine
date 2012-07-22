using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Bot.Entity.Schedule
{
    

    public enum ScheduleAction
    {
        NONE=0,
        DELETE,
        CREATE,
        UPDATE,
        REFRESH
    }
    public enum ScheduleStatus
    {
        NONE,
        NEW,
        QUEUE,
        PROCESSING,
        CANCELED,
        DONE,
        ERROR
    }

}
