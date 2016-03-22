using SharpOffice.Core.Commands;
using SharpOffice.Core.Data;

namespace SharpOffice.Common.Commands
{
    public class ChainCommand : ICommand
    {
        private ICommand[] _chainedCommands;

        public ChainCommand(params ICommand[] commands)
        {
            _chainedCommands = commands;
        }

        public IData Execute(IData data)
        {
            var currentData = data;
            foreach (ICommand cmd in _chainedCommands)
                currentData = cmd.Execute(data);
            return currentData;
        }

        public IData Reverse(IData data)
        {
            var currentData = data;
            for (int i = _chainedCommands.Length - 1; i >= 0; i--)
                currentData = _chainedCommands[i].Reverse(currentData);
            return currentData;
        }
    }
}