using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Represents the type of message.
    /// </summary>
    public enum SimpleMessageType
    {
        Complete = 0,
        Abandon = 1,
        SendToDlq = 2,
        Error = 3
    }
}
