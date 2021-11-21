using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageShare.Models
{
    public partial class FileDb
    {
        public FileDb()
        {
            AccessDb = new HashSet<AccessDb>();
            MetadataDb = new HashSet<MetadataDb>();
        }

        public int FileId { get; set; }
        [DisplayName("Title")]
        public string FileTitle { get; set; }
        [DisplayName("Captured By")]
        public string FileCapturedBy { get; set; }
        [DisplayName("Date Captured")]
        public DateTime? FileCapturedDate { get; set; }
        [DisplayName("Geolocation")]
        public string FileGeolocation { get; set; }
        [DisplayName("URL")]
        public string FileUrl { get; set; }
        [DisplayName("Tags")]
        public string FileTags { get; set; }
        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<AccessDb> AccessDb { get; set; }
        public virtual ICollection<MetadataDb> MetadataDb { get; set; }
    }
}
