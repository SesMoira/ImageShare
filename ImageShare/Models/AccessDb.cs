using System;
using System.Collections.Generic;

namespace ImageShare.Models
{
    public partial class AccessDb
    {
        public int AccessId { get; set; }
        public string UserId { get; set; }
        public int FileId { get; set; }

        public virtual FileDb File { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
