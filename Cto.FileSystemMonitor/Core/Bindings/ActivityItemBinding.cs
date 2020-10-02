using System;

namespace Cto.FileSystemMonitor.Core.Bindings
{

    public class ActivityItemBinding
    {

        public string FileName { get; set; }

        public string ActionType { get; set; }

        public string FileLocation { get; set; }

        public DateTime ActivityTime { get; set; }

    }

}