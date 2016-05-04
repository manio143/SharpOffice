using System;
using System.ComponentModel;

namespace SharpOffice.Core.Window
{
    public abstract class SingleMenuItem : IMenuElement
    {
        protected SingleMenuItem(string label)
        {
            _label = label;
        }

        private readonly string _label;
        public string Label
        {
            get { return _label; }
        }

        public bool Enabled { get { return true; } }
        public bool? Checked { get { return null; } }
        public Menu SubMenu { get { return null; } }

        public abstract void Command(object sender, EventArgs args);

        public event PropertyChangedEventHandler PropertyChanged;
    }
}