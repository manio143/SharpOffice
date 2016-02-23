using System;
using System.IO;
using SharpOffice.Window;
using Xwt;
using Xwt.GtkBackend;

namespace SharpOffice
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Platform.IsWindows)
                Application.Initialize(ToolkitType.Wpf);
            else if (Platform.IsMac)
                Application.Initialize(ToolkitType.Cocoa);
            else
                Application.Initialize(ToolkitType.Gtk);

            //Assembly load / Choose Type
            var definition = new WindowDefinition();
            var config = new WindowConfiguration(); //load config

            using (var window = WindowGenerator.Generate(definition, config))
            {
                window.Show();
                Application.Run();
            }
        }
    }
}
