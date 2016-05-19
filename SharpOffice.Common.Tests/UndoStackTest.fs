namespace SharpOffice.Common.Tests

open Moq
open NUnit.Framework
open SharpOffice.Common.Commands
open SharpOffice.Core.Commands

[<TestFixture>]
module UndoStackTest =
    
    let mutable _undoStack = new UndoStack()
    
    let GetMockObject () =
        let mock = new Mock<ICommand>()
        mock.Object

    let Insert () =
        let obj = GetMockObject ()
        _undoStack.Insert (obj)
        obj

    let Undo () = _undoStack.Undo()

    let Redo () = _undoStack.Redo()

    [<SetUp>]
    let Clear () = _undoStack <- new UndoStack()

    [<Test>]
    let BasicInsertTest () =
        ignore(Insert ())

    [<Test>]
    let BasicUndoTest () =
        let obj = Insert()
        let undo = Undo()
        Assert.AreEqual(obj, undo)

    [<Test>]
    let BasicRedoTest () =
        ignore(Insert())
        let undo = Undo()
        let redo = Redo()
        Assert.AreEqual(undo, redo)

    [<Test>]
    let ThrowOnEmptyStackTest () =
        ignore(Assert.Throws<EmptyStackException>(fun () -> ignore(Undo())))

    [<Test>]
    let ThrowOnEmptyRedoTest () =
        ignore(Assert.Throws<EmptyStackException>(fun () -> ignore(Redo())))

    [<Test>]
    let UndoRedoUndoTest () =
        ignore(Insert())
        let undo1 = Undo()
        let redo = Redo()
        let undo2 = Undo()
        Assert.AreEqual(undo1, redo)
        Assert.AreEqual(redo, undo2)