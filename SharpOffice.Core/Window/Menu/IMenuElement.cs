using System;
using System.ComponentModel;

namespace SharpOffice.Core.Window
{
    public interface IMenuElement : INotifyPropertyChanged
    {
        string Label { get; }
        bool Enabled { get; }

        /// <summary>
        /// Null if not checkable, true/false if checkable.
        /// </summary>
        bool? Checked { get; }

        Menu SubMenu { get; }

        void Command(object sender, EventArgs args);
    }
}