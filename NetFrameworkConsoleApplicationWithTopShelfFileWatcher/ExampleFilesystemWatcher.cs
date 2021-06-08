using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NetFrameworkConsoleApplicationWithTopShelfFileWatcher
{
    public class ExampleFilesystemWatcher
    {
        private readonly ServiceConfig _serviceConfig;
        private FileSystemWatcher _fileSystemWatcher;

        public ExampleFilesystemWatcher(ServiceConfig serviceConfig)
        {
            this._serviceConfig = serviceConfig;
        }

        private void ProcessExistingFilesAndStartListening()
        {
            Console.WriteLine($"Processing Files in {_serviceConfig.FolderToWatch} on start up...");
            string[] files = Directory.GetFiles(_serviceConfig.FolderToWatch);
            foreach(string file in files)
            {
                ProcessFile(file);
            }

            _fileSystemWatcher = new FileSystemWatcher(_serviceConfig.FolderToWatch);
            _fileSystemWatcher.IncludeSubdirectories = false;
            _fileSystemWatcher.EnableRaisingEvents = true;
            _fileSystemWatcher.Created += FileSystemWatcher_Created;
            _fileSystemWatcher.Error += FileSystemWatcher_Error;
        }

        private void ProcessFile(string fileName)
        {
            Console.WriteLine($"Processing {fileName}...");

            // Rest of the code to process the file goes here..

            ArchiveFile(fileName);
        }

        void ArchiveFile(string fileName)
        {            
            var fileInfo = new FileInfo(fileName);
            var newPath = Path.Combine(_serviceConfig.ArchiveFolder, fileInfo.Name); // You may want to mainpulate the filename to have a different date or version number if the source file does not have any date or time information in the original file name itself
            Console.WriteLine($"Moving file {fileInfo.Name} to {_serviceConfig.ArchiveFolder}");
            fileInfo.CopyTo(newPath, true);
            fileInfo.Delete();
        }

        void ResetFileSystemWatcherEvents()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
            int iMaxAttempts = 120;
            int iTimeOut = 30000;
            int i = 0;
            while (_fileSystemWatcher.EnableRaisingEvents == false && i < iMaxAttempts)
            {
                i += 1;
                try
                {
                    Console.WriteLine("Attempting to re-enable file system watcher");
                    _fileSystemWatcher.EnableRaisingEvents = true;

                    Console.WriteLine("File System Watcher Restarted");

                    ProcessExistingFilesAndStartListening();
                    i = 0;
                }
                catch
                {
                    Console.WriteLine($"Unable to restart watching service. Will try again in 30 seconds. Attempt {i} of 120");
                    _fileSystemWatcher.EnableRaisingEvents = false;
                    System.Threading.Thread.Sleep(iTimeOut);
                }
            }
        }

        

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Task.Delay(5000).Wait(); // Wait 5 seconds for the file to completely save / copy before processing it
            ProcessFile(e.FullPath);
        }

        private void FileSystemWatcher_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.ToString());
            ResetFileSystemWatcherEvents();
        }

        public bool Start()
        {
            ProcessExistingFilesAndStartListening();

            return true;
        }

        public bool Stop()
        {
            _fileSystemWatcher.Dispose();
            return true;
        }

        public bool Pause()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
            return true;
        }

        public bool Continue()
        {
            _fileSystemWatcher.EnableRaisingEvents = true;
            return true;
        }

        /*
        private void SendMessage(MailMessage mailMessage)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }
        */
    }
}
