using System;
using System.Collections.Generic;

namespace ImageShare.Models
{
    public partial class UserInfo
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
    }
}
