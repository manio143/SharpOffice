using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using SharpOffice.Core;

namespace SharpOffice.Runtime.Eto
{
    public class MainWindow : Form
    {
        public MainWindow(IApplication application)
        {
            Title = application.Name + " - SharpOffice.Runtime.Eto (" + GetType().Assembly.GetName().Version + ")";
            ClientSize = new Size(460, 200);
        }
    }
}