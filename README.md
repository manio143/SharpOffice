# SharpOffice #
The purpose of this project is to create a cross-platform open-source Office suite written in C#. The idea is that it should be very modularized for easy plugin creation. Each application will be just a library that when plugged into the runtime will display all necessary features for the program while using the common code base.

I will comment my progress on my blog http://md-techblog.net.pl [Polish]

The suite will be divided into several applications:
* __SharpNotepad__ (for code and quicknotes)
* __SharpWriter__ (text document editing)
* __SharpCalc__ (calculation sheet editing)
* __SharpPresent__ (presentation editing)

The project is using Xwt for GUI rendering. On each platform it will use native solutions (WPF on Windows, GTK# on Linux, Cocoa on Mac).

### Modules ###
#### SharpOffice.Core ####
Core library which holds interfaces for editing logic and plugin support.
#### SharpOffice.Common ####
The common implementations of Core interfaces.
#### SharpOffice.Window ####
This library defines objects that will construct the applications
#### SharpOffice.Window.Runtime ####
This is the main application which given a set of libraries will create a window with all necessary controls and behaviours.