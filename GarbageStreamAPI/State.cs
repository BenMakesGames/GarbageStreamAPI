using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarbageStreamAPI
{
    public enum State
    {
        EXPECTING_GROUP_OR_GARBAGE,
        IN_GROUP,
        IN_GARBAGE,
        IGNORE_NEXT_CHARACTER,
        EXITED_GROUP_OR_GARBAGE,
    }
}
