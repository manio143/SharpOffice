# SharpOffice (discontinued) #
The purpose of this project is to create a cross-platform open-source Office suite written in C#. The idea is that it should be very modularized for easy plugin creation. Each application will be just a library that when plugged into the runtime will display all necessary features for the program while using the common code base.

Windows | Linux
--------|-------
[![Build status](https://ci.appveyor.com/api/projects/status/qebehe9amlk2jt4c/branch/master?svg=true)](https://ci.appveyor.com/project/manio143/sharpoffice/branch/master) | [![Build Status](https://travis-ci.org/manio143/SharpOffice.svg?branch=master)](https://travis-ci.org/manio143/SharpOffice)

I will comment my progress on my blog http://md-techblog.net.pl [Polish]

The suite will be divided into several applications:
* __SharpNote__ (for code and quicknotes)
* __SharpWriter__ (text document editing)
* __SharpCalc__ (calculation sheet editing)
* __SharpPresent__ (presentation editing)

### Modules ###

#### SharpOffice.Core ####
Core library which holds interfaces for editing logic and plugin support.
As well as the common implementations of core interfaces.

#### SharpOffice.Runtime.* ####
Executables that discover application modules and plugins, and based on certain implementations and definitions generate GUI.

#### SharpOffice._Application_ ####
Assemblies that define applications via controls set, behaviours, etc.

### Building ###

	git clone https://github.com/manio143/SharpOffice
	cd SharpOffice
	nuget restore
	msbuild 

### Running ###
To run an application you have to copy its libraries into the output folder of a desired runtime. So for example

	cp SharpOffice.SharpNote/bin/Debug/SharpOffice.SharpNote.dll SharpOffice.Runtime.Eto/bin/Debug

And then you go to the destination folder and run

	SharpOffice.Runtime.Eto.exe SharpOffice.SharpNote

Where the argument is the assembly name of the application assembly.
