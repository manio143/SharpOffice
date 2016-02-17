# SharpOffice #
The purpose of this project is to create a cross-platform open-source Office suite written in C#.

The suite will be divided into several applications:
* __SharpWriter__ (text document editing)
* __SharpCalc__ (calculation sheet editing)
* __SharpPresent__ (presentation editing)

The project is using Xwt for GUI rendering.

### Modules ###
#### SharpOffice.Core ####
Core library which holds editing logic and plugin support.
#### SharpOffice.Window ####
This library defines objects that will construct the applications
#### SharpOffice.Window.Runtime ####
This is the main application which given a set of libraries will create a window with all necessary controls and behaviours.