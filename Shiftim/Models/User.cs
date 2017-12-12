using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Shiftim.Models
{
    public class User
    {
        
        [Key]
        [Required(ErrorMessage = " *שדה חובה")]
        [DataType(DataType.EmailAddress, ErrorMessage = "אימייל לא תקין")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [EmailAddress]
        public string email { get; set; }
        
        [Required(ErrorMessage = " *שדה חובה")]
        public string password { get; set; }

        [Required(ErrorMessage = " *שדה חובה")]
        public string rank { get; set; }

   //     [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Worker worker { get; set; }

    }
}