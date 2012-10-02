using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Entity
{
    public enum ConfigFieldDatabaseQueue
    {
        MaxQueueSize
    }

    public enum ResponseFormat
    {
        NONE,
        TEXT,
        XML,
        RSS,
        CSV,
        BINARY
    }

    public enum Language
    {
        VN,
        EN
    }
}
