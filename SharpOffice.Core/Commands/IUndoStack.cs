namespace SharpOffice.Core.Commands
{
    public interface IUndoStack
    {
        void Insert(ICommand cmd);
        
        ICommand Undo();
        ICommand Redo();

        ICommand Peek();

        void ApplyChanges();

        int StepsLeft { get; }
        int RedoStepsLeft { get; }
    }
}