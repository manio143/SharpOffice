using System;
using System.IO;
using SharpOffice.Core.Configuration;
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
            InitializeApplication();

            //Assembly load / Choose Type
            IWindowDefinition definition;
            IConfiguration config;

            using (var window = WindowGenerator.Generate(definition, config))
            {
                window.Show();
                Application.Run();
            }
        }

        public static void InitializeApplication()
        {
            if (Platform.IsWindows)
                Application.Initialize(ToolkitType.Wpf);
            else if (Platform.IsMac)
                Application.Initialize(ToolkitType.Cocoa);
            else
                Application.Initialize(ToolkitType.Gtk);
        }
    }
}
