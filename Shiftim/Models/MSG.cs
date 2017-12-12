using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shiftim.Models
{
    public class MSG
    {
        public string email { get; set; }
        [Key]
        public string msg { get; set; }
        public DateTime date { get; set; }
    }
}