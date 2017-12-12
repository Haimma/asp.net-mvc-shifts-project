using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shiftim.Models
{
    public class Shifts
    {
        [Key]       
        [Required(ErrorMessage = " *שדה חובה")]
        [DataType(DataType.EmailAddress, ErrorMessage = "אימייל לא תקין")]
        [EmailAddress]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string email { get; set; }


        [Required(ErrorMessage = " *שדה חובה")]
        [DataType(DataType.Date,ErrorMessage = "תאריך לא תקין" )]
        public DateTime date { get; set; }


        [Required(ErrorMessage = " *שדה חובה")]
        public string day { get; set; }

        [Required(ErrorMessage = " *שדה חובה")]
        public string myShift { get; set; }

        public string comments { get; set; }

        public string approve { get; set; }


    }
}