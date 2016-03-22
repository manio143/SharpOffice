namespace SharpOffice.Core.Commands
{
    public interface ICommandExecutor
    {
        void ApplyCommand(ICommand cmd);
    }
}