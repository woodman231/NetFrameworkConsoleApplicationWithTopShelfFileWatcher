using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFrameworkConsoleApplicationWithTopShelfFileWatcher
{
    public class ServiceConfig
    {
        public string ServiceName { get; set; }
        public string ServiceDisplayName { get; set; }
        public string ServiceDescription { get; set; }
        public string FolderToWatch { get; set; }
        public string ArchiveFolder { get; set; }
    }
}
