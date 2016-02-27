using System;
using System.Runtime.Serialization;
using SharpOffice.Core.Commands;

namespace SharpOffice.Common
{
    public class UndoStack
    {
        private ICommand[][] _stack;
        private int _undoSteps;
        private int _currentArray;
        private int _currentIndex;
        private int _stepsLeft;

        public UndoStack(int undoSteps)
        {
            _undoSteps = undoSteps;
            _stack = new ICommand[2][];
            _stack[0] = new ICommand[_undoSteps];
            _stack[1] = new ICommand[_undoSteps];
        }

        public void Push(ICommand cmd)
        {
            if (++_currentIndex >= _undoSteps)
            {
                if (_currentArray == 1)
                {
                    _stack[0] = _stack[1];
                    _stack[1] = new ICommand[_undoSteps];
                }
                else
                {
                    _currentArray = 1;
                }
                _currentIndex = 0;
            }
            _stepsLeft++;
            _stack[_currentArray][_currentIndex] = cmd;
        }

        public ICommand Pop()
        {
            var cmd = _stack[_currentArray][_currentIndex];
            _stack[_currentArray][_currentIndex] = null;
            if (--_currentIndex < 0)
            {
                if (_currentArray == 0)
                    throw new EmptyStackException();
                _currentArray--;
                _currentIndex = _undoSteps - 1;
            }
            _stepsLeft--;
            return cmd;
        }

        public ICommand Peek()
        {
            return _stack[_currentArray][_currentIndex];
        }

        public int StepsLeft { get { return _stepsLeft; } }
    }

    [Serializable]
    public class EmptyStackException : Exception
    {
        public EmptyStackException()
        {
        }

        public EmptyStackException(string message) : base(message)
        {
        }

        public EmptyStackException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EmptyStackException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
