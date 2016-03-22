using System;
using System.Runtime.Serialization;
using SharpOffice.Core.Commands;

namespace SharpOffice.Common.Commands
{
    /// <summary>
    /// A simple stack implementation for inserting ICommand objects, with Undo and Redo
    /// </summary>
    public class UndoStack : IUndoStack
    {
        private readonly ICommand[][] _stack;
        private readonly ICommand[] _redoStack;
        private readonly int _undoSteps;
        private readonly ICommandExecutor _commandExecutor;
        private int _currentArray;
        private int _currentIndex;
        private int _currentRedoIndex;
        private int _stepsLeft;

        public UndoStack(int undoSteps)
        {
            _undoSteps = undoSteps;
            _stack = new ICommand[2][];
            _stack[0] = new ICommand[_undoSteps];
            _redoStack = new ICommand[_undoSteps];
        }

        public UndoStack(int undoSteps, ICommandExecutor commandExecutor)
            : this(undoSteps)
        {
            _commandExecutor = commandExecutor;
        }

        /// <summary>
        /// Add a new command to the stack.
        /// </summary>
        /// <param name="cmd"></param>
        public void Insert(ICommand cmd)
        {
            NextIndex();
            IncreaseStepsLeft();
            _stack[_currentArray][_currentIndex] = cmd;
            _currentRedoIndex = 0;
        }

        private void IncreaseStepsLeft()
        {
            _stepsLeft += _stepsLeft < _undoSteps ? 1 : 0;
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
            {
                ApplyChanges(0, 0, _undoSteps, ignoreNoCommandExecutor: true);
                _stack[0] = _stack[1];
            }
            else
                _currentArray = 1;
            _stack[1] = new ICommand[_undoSteps];
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

        public int RedoStepsLeft { get { return _currentRedoIndex; } }

        /// <summary>
        /// Send commands to ICommandExecutor.
        /// </summary>
        public void ApplyChanges()
        {
            ApplyChanges(_currentIndex, _currentArray, _stepsLeft, reverse: true);
        }

        private void ApplyChanges(int startIndex, int startArray, int count, bool reverse = false,
            bool ignoreNoCommandExecutor = false)
        {
            if (_commandExecutor == null)
            {
                if (ignoreNoCommandExecutor)
                    return;
                throw new InvalidOperationException("This UndoStack hasn't been initialized with an ICommandExecutor.",
                    new NullReferenceException());
            }

            Func<int, int, int> change = (value, modifier) => reverse ? value + modifier : value - modifier;

            var currentIndex = startIndex;
            var currentArray = startArray;
            for (int i = 0; i < count; i++)
            {
                _commandExecutor.ApplyCommand(_stack[currentArray][currentIndex]);
                currentIndex = currentIndex == _undoSteps ? 0 : change(currentIndex, 1);
                currentArray = currentIndex == 0 ? change(currentArray, 1) : currentArray;
            }
        }
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
