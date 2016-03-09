using System;
using System.Runtime.Serialization;
using SharpOffice.Core.Commands;

namespace SharpOffice.Common
{
    /// <summary>
    /// A simple stack implementation for inserting ICommand objects, with Undo and Redo
    /// </summary>
    public class UndoStack
    {
        private readonly ICommand[][] _stack;
        private readonly ICommand[] _redoStack;
        private readonly int _undoSteps;
        private int _currentArray;
        private int _currentIndex;
        private int _currentRedoIndex;
        private int _stepsLeft;

        public UndoStack(int undoSteps)
        {
            _undoSteps = undoSteps;
            _stack = new ICommand[2][];
            _stack[0] = new ICommand[_undoSteps];
            _stack[1] = new ICommand[_undoSteps];
            _redoStack = new ICommand[_undoSteps];
        }

        /// <summary>
        /// Add a new command to the stack.
        /// </summary>
        /// <param name="cmd"></param>
        public void Insert(ICommand cmd)
        {
            NextIndex();
            _stepsLeft++;
            _stack[_currentArray][_currentIndex] = cmd;
            _currentRedoIndex = 0;
        }

        private void NextIndex()
        {
            if (++_currentIndex < _undoSteps) return;

            NextArray();
            _currentIndex = 0;
        }

        private void NextArray()
        {
            if (_currentArray == 1)
                _stack[0] = _stack[1];
            else
            {
                _stack[1] = new ICommand[_undoSteps];
                _currentArray = 1;
            }
        }

        /// <summary>
        /// Retrieve the last command from the stack.
        /// </summary>
        /// <returns></returns>
        public ICommand Undo()
        {
            ThrowIfEmpty();
            var cmd = _stack[_currentArray][_currentIndex];
            InsertRedo(cmd);
            PreviousIndex();
            _stepsLeft--;
            return cmd;
        }

        private void ThrowIfEmpty()
        {
            if (_stepsLeft == 0)
                throw new EmptyStackException();
        }

        private void InsertRedo(ICommand cmd)
        {
            _redoStack[_currentRedoIndex++] = cmd;
        }

        private void PreviousIndex()
        {
            if (--_currentIndex >= 0) return;
            
            PreviousArray();
            _currentIndex = _undoSteps - 1;
        }

        private void PreviousArray()
        {
            if (_currentArray == 0)
                throw new EmptyStackException();
            _currentArray--;
        }

        /// <summary>
        /// Retrieve the last command from the stack without removing it.
        /// </summary>
        /// <returns></returns>
        public ICommand Peek()
        {
            return _stack[_currentArray][_currentIndex];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICommand Redo()
        {
            if (--_currentRedoIndex < 0)
                throw new InvalidOperationException("Nothing to redo.");

            var cmd = _redoStack[_currentRedoIndex];
            NextIndex();
            _stepsLeft++;

            return cmd;
        }

        public int StepsLeft { get { return _stepsLeft; } }

        public int RedoStepsLeft { get { return _currentRedoIndex;} }
    }

    [Serializable]
    public class EmptyStackException : Exception
    {
        public EmptyStackException()
        {
        }

        public EmptyStackException(string message)
            : base(message)
        {
        }

        public EmptyStackException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected EmptyStackException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
