using System;

namespace SharpOffice.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = false)]
    public class WindowAttribute : Attribute
    {
        public WindowType WindowType { get; private set; }

        public WindowAttribute(WindowType windowType)
        {
            WindowType = windowType;
        }
    }

    public enum WindowType
    {
        MainWindow,
        Window,
        Dialog
    }
}