# .NET Framework Console Application With TopShelf as a Folder Listener Example

This example demonstrates a way of turning a .NET Framework Console Application in to a microservice (A service that can be configured via services.msc Console Snap In)

The key elements to this example are;

- The TopShelf NuGet Package
- Adding some keys to the App.config
- Creating a ServiceConfig POCO class
- Creating a Service class to initiate the FileSystemWatcher

Microservices created in .NET Framework are very powerful and configurable. Rather than checking a folder at intervals, this service can respond immediately when teh file is saved / copied to the folder taht will be watched.

After the code is working on your computer it is recommended that you build the project and then in the bin\Debug folder of this project copy the files to the server which will be running the service application in to an D:\Installs\ServiceName\1.0 folder

Once the files are on the server at D:\Installs\ServiceName\1.0, copy the "SampleServiceInstallScript.ps1" to D:\Installs\ServiceName\ServiceInstall.ps1. Edit the file so that the file names are correct for your application. This example also assumes that the microservice will be running as a Windows service account. Specify DOMAIN\Username as appropriate with the appropriate password in the $params variable.

Assuming everything works out the service should start up. On the application server check services.msc console snap in and ensure that the service appears, and is in a Started / Automatic state. Right-click on the service to make the necessary adjustments (for example you may need to right-click on the service and select "Start")

Whenever you have a service running on a remote computer and it needs to be updated an example SampleServiceUpdateScript has been provided. We recommen that once the service is working correctly on the Development server that you do a right-click and Build in visual studio.

Then copy the files to the destination server such that it will be available in a folder such as D:\Installs\ServiceName\1.1

Once the files are in that folder copy the SampleServiceUpdate.ps1 file to D:\Installs\ServiceName\ServiceUpdate1.1.ps1

Edit the D:\Installs\ServiceName\ServiceUpdate1.1.ps1 file to have the correct values for the specified variables.

Edit the D:\Installs\ServiceName\1.1\ServiceName.exe.config file and ensure that the AppSettings and connection strings are appropriate for the environment that you are targetting.

Once the config has been verified and the ServiceUpdate file has been corrected execute the PowerShell script and the service should stop. Have the new files copied over, and then restarted.

Check the services.msc console snap-in and ensure that the service is still running.