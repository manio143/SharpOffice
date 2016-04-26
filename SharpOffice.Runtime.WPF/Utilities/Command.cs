using System;
using System.Windows.Input;

namespace SharpOffice.Window.Runtime.WPF.Utilities
{
    public class Command : ICommand
    {
        private readonly EventHandler _handler;
        private readonly object _owner;

        public Command(EventHandler handler, object owner)
        {
            if(handler == null)
                throw new ArgumentNullException("handler");
            _handler = handler;
            _owner = owner;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event System.EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _handler(_owner, EventArgs.Empty);
        }
    }
}