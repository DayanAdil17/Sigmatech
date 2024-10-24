using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sigmatech.Settings
{
    public class MailSettings
    {
        [Required]
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        public int Port { get; set; }
    }
}