namespace SharpOffice.Core.Commands
{
    public interface IUndoStack
    {
        void Insert(ICommand cmd);
        
        ICommand Undo();
        ICommand Redo();

        ICommand Peek();

        int StepsLeft { get; }
        int RedoStepsLeft { get; }
    }
}