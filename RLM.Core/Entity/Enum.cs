using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity
{
    public enum SystemMode
    {
        Web,
        Console,
        BackgroundService,
        Unknow
    }

    public enum UrlPramName
    {
        ItemId,
        MemberId,
        Type,
        CategoryId,
        Name,
        StoreKey
    }

    public enum DataTypeEnum
    {
        Int,
        Long,
        String
    }

    public enum EditorTypeEnum
    {
        Text,
        TextMultiline,
        Html
    }
}
