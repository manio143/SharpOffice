using System;
using System.ComponentModel;

namespace SharpOffice.Core.Window
{
    public class MenuSeparator : IMenuElement
    {
        public string Label { get { return "---"; }}
        public bool Enabled { get { return false; }}
        public bool? Checked { get { return null; } }
        public Menu SubMenu { get { return null; }}
        public void Command(object sender, EventArgs args)
        {
            throw new InvalidOperationException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}