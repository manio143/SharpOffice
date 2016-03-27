using Moq;
using NUnit.Framework;
using SharpOffice.Common.Commands;
using SharpOffice.Core.Commands;

namespace SharpOffice.Common.Tests
{
    [TestFixture]
    public class UndoStackTest
    {
        private UndoStack _undoStack;

        private ICommand GetMockObject()
        {
            return new Mock<ICommand>().Object;
        }

        [Test]
        public void CreationTest()
        {
            _undoStack = new UndoStack();
        }

        [Test]
        public void BasicInsertTest()
        {
            CreationTest();
            _undoStack.Insert(GetMockObject());
        }

        [Test]
        public void BasicUndoTest()
        {
            BasicInsertTest();
            _undoStack.Undo();
        }

        [Test]
        public void BasicRedoTest()
        {
            BasicUndoTest();
            _undoStack.Redo();
        }

        [Test]
        public void ThrowOnEmptyStackTest()
        {
            BasicUndoTest();
            Assert.Throws<EmptyStackException>(delegate { _undoStack.Undo(); });
        }

        [Test]
        public void ThrowOnEmptyRedoTest()
        {
            BasicRedoTest();
            Assert.Throws<EmptyStackException>(delegate { _undoStack.Redo(); });
        }

        [Test]
        public void UndoRedoUndoTest()
        {
            BasicInsertTest();
            var ucmd = _undoStack.Undo();
            var rcmd = _undoStack.Redo();
            var u2cmd = _undoStack.Undo();
            Assert.AreEqual(ucmd, rcmd);
            Assert.AreEqual(u2cmd, ucmd);
        }
    }
}
