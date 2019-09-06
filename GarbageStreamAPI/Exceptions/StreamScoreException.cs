using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarbageStreamAPI.Exceptions
{
    public class StreamScoreException: Exception
    {
        public State? State { get; private set; }
        public int Offset { get; private set; }
        public char Character { get; private set; }

        public StreamScoreException(State? state, int offset, char character)
        {
            State = state;
            Offset = offset;
            Character = character;
        }

        public override string Message
        {
            get
            {
                return
                    "Unexpected character in stream; " +
                    "state: " + (State == null ? "NULL" : State.ToString()) + ", " +
                    "offset: " + Offset + ", " +
                    "character: " + Character
                ;
            }
        }
    }
}
