using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpOffice.Common.Commands;
using SharpOffice.Core.Commands;

namespace SharpOffice.Common.Tests
{
    [TestClass]
    public class UndoStackTest
    {
        private UndoStack _undoStack;

        private ICommand GetMockObject()
        {
            return new Mock<ICommand>().Object;
        }

        [TestMethod]
        public void CreationTest()
        {
            _undoStack = new UndoStack(10);
        }

        [TestMethod]
        public void BasicInsertTest()
        {
            CreationTest();
            _undoStack.Insert(GetMockObject());
        }

        [TestMethod]
        public void BasicUndoTest()
        {
            BasicInsertTest();
            _undoStack.Undo();
        }

        [TestMethod]
        public void BasicRedoTest()
        {
            BasicUndoTest();
            _undoStack.Redo();
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyStackException))]
        public void ThrowOnEmptyStackTest()
        {
            BasicUndoTest();
            _undoStack.Undo();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ThrowOnEmptyRedoTest()
        {
            BasicRedoTest();
            _undoStack.Redo();
        }

        [TestMethod]
        public void UndoRedoUndoTest()
        {
            BasicInsertTest();
            var ucmd = _undoStack.Undo();
            var rcmd = _undoStack.Redo();
            var u2cmd = _undoStack.Undo();
            Assert.AreEqual(ucmd, rcmd);
            Assert.AreEqual(u2cmd, ucmd);
        }

        [TestMethod]
        public void ArrayChangeTest()
        {
            CreationTest();
            for (int i = 0; i < 11; i++)
                _undoStack.Insert(GetMockObject());
            
            Assert.AreEqual(10, _undoStack.StepsLeft);

            for (int i = 0; i < 10; i++)
                _undoStack.Insert(GetMockObject());
            
            for (int i = 0; i < 10; i++)
                _undoStack.Undo();
        }
    }
}
