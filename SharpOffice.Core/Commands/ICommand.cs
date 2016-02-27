using SharpOffice.Core.Data;

namespace SharpOffice.Core.Commands
{
    public interface ICommand
    {
        IData Execute(IData data);
    }
}