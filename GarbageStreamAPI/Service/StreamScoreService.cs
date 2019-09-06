using GarbageStreamAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarbageStreamAPI.Service
{
    public class StreamScoreService
    {
        public int Score(string stream)
        {
            if (string.IsNullOrEmpty(stream))
                throw new StreamScoreException(State.EXPECTING_GROUP_OR_GARBAGE, 0, '\0');

            Stack<State> stack = new Stack<State>();
            stack.Push(State.EXPECTING_GROUP_OR_GARBAGE);

            int score = 0;

            for (int i = 0; i < stream.Length; i++)
            {
                char c = stream[i];

                if (stack.Count == 0)
                    throw new StreamScoreException(null, i, c);

                State state = stack.Peek();

                switch (state)
                {
                    case State.EXPECTING_GROUP_OR_GARBAGE:
                        if (c == '{')
                        {
                            stack.Pop();
                            stack.Push(State.IN_GROUP);
                        }
                        else if (c == '<')
                        {
                            stack.Pop();
                            stack.Push(State.IN_GARBAGE);
                        }
                        else
                            throw new StreamScoreException(state, i, c);
                        break;

                    case State.IN_GARBAGE:
                        if (c == '!')
                            stack.Push(State.IGNORE_NEXT_CHARACTER);
                        else if (c == '>')
                        {
                            stack.Pop();
                            stack.Push(State.EXITED_GROUP_OR_GARBAGE);
                        }
                        break;

                    case State.IN_GROUP:
                        if (c == '<')
                            stack.Push(State.IN_GARBAGE);
                        else if (c == '{')
                            stack.Push(State.IN_GROUP);
                        else if (c == '}')
                        {
                            score += stack.Count(s => s == State.IN_GROUP);
                            stack.Pop();
                            stack.Push(State.EXITED_GROUP_OR_GARBAGE);
                        }
                        else
                            throw new StreamScoreException(state, i, c);
                        break;

                    case State.IGNORE_NEXT_CHARACTER:
                        stack.Pop();
                        break;

                    case State.EXITED_GROUP_OR_GARBAGE:
                        stack.Pop();

                        if (c == ',')
                        {
                            stack.Push(State.EXPECTING_GROUP_OR_GARBAGE);
                        }
                        else if (c == '}' && stack.Count > 0 && stack.Peek() == State.IN_GROUP)
                        {
                            score += stack.Count(s => s == State.IN_GROUP);
                            stack.Pop();
                            stack.Push(State.EXITED_GROUP_OR_GARBAGE);
                        }
                        else
                            throw new StreamScoreException(state, i, c);
                        break;
                }
            }
            
            if(stack.Count > 1 || stack.Peek() != State.EXITED_GROUP_OR_GARBAGE)
                throw new StreamScoreException(stack.Peek(), stream.Length + 1, '\0');

            return score;
        }
    }
}
