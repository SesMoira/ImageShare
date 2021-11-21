using System;
using System.Collections.Generic;

namespace ImageShare.Models
{
    public partial class MetadataDb
    {
        public int MetadataId { get; set; }
        public int FileId { get; set; }
        public string FileTitle { get; set; }
        public string CaptureBy { get; set; }
        public DateTime? CapturedDate { get; set; }
        public string Tags { get; set; }

        public virtual FileDb File { get; set; }
    }
}
