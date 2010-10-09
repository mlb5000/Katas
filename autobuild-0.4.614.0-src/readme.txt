*******************************************************************************
Overview
*******************************************************************************

AutoBuild is a continuous testing tool for .NET that builds a project and runs the tests after every save.

An introduction to AutoBuild can be found here:

http://timross.wordpress.com/2009/08/04/continuous-testing-in-net/

*******************************************************************************
Revision History
*******************************************************************************

Version 0.4
	Tim Ross - http://timross.wordpress.com

		* Added logging for MSpec test failure output.

Version 0.3
	Tim Ross - http://timross.wordpress.com

		* Added support for Growl notifications. Thanks to Mike Wagg http://mikewagg.blogspot.com/

Version 0.2
	Tim Ross - http://timross.wordpress.com

		* Fixed an issue with the file extension filter match regex. 
		* Added logging for MSBuild errors.

Version 0.1
	Tim Ross - http://timross.wordpress.com

		* Initial version


*******************************************************************************
Known issues
*******************************************************************************

Sometimes the build will run two or three times in a row. This could be because another process keeps modifying the file.

*******************************************************************************
Building AutoBuild
*******************************************************************************

Double-click the default.build.cmd file to run the build.

*******************************************************************************
Using AutoBuild
*******************************************************************************

To try out AutoBuild against the AutoBuild codebase:

1. Double-click the autobuild.cmd file to start AutoBuild.
2. Open the AutoBuild solution and modify any .cs file. This should kick-off the build and run the tests. 
3. Mess around with the code to watch the build fail.

To use AutoBuild in your project:

1. Copy the AutoBuild output folder to a folder in your project (e.g Tools).
2. Modify the autobuild.build NAnt script to suit your project.
3. Modify the autobuild.cmd to change the parameters passed to AutoBuild.Console.exe
	a) The first parameter sets the path to watch for changes.
	b) The second parameter is the name of the build script to run.
4. Double-click the autobuild.cmd file to start AutoBuild

Please submit any issues here: http://code.google.com/p/autobuildtool/issues/list

