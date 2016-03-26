using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SharpOffice.Core.Commands;

namespace SharpOffice.Common.Commands
{
    /// <summary>
    /// A simple stack implementation for inserting ICommand objects, with Undo and Redo
    /// </summary>
    public class UndoStack : IUndoStack
    {
        private readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
        private readonly Stack<ICommand> _redoStack = new Stack<ICommand>();

        /// <summary>
        /// Add a new command to the stack.
        /// </summary>
        /// <param name="cmd"></param>
        public void Insert(ICommand cmd)
        {
            if(cmd == null)
                throw new ArgumentNullException("cmd");

            _undoStack.Push(cmd);
            _redoStack.Clear();
        }

        /// <summary>
        /// Retrieve the last command from the stack.
        /// </summary>
        /// <returns></returns>
        public ICommand Undo()
        {
            ThrowIfEmpty(_undoStack);

            var cmd = _undoStack.Pop();
            _redoStack.Push(cmd);

            return cmd;
        }

        private void ThrowIfEmpty(Stack<ICommand> stack)
        {
            if (stack.Count == 0)
                throw new EmptyStackException(String.Format("{0} is empty.", stack == _undoStack ? "UndoStack" : "RedoStack"));
        }
        
        /// <summary>
        /// Retrieve the last command from the stack without removing it.
        /// </summary>
        /// <returns></returns>
        public ICommand Peek()
        {
            ThrowIfEmpty(_undoStack);
            return _undoStack.Peek();
        }

        /// <summary>
        /// Pushes last Undone command back to the UndoStack.
        /// </summary>
        /// <returns></returns>
        public ICommand Redo()
        {
            ThrowIfEmpty(_redoStack);
            
            var cmd = _redoStack.Pop();
            _undoStack.Push(cmd);

            return cmd;
        }

        public int StepsLeft { get { return _undoStack.Count; } }

        public int RedoStepsLeft { get { return _redoStack.Count; } }

        public void Clear()
        {
            _undoStack.Clear();
            _redoStack.Clear();
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
