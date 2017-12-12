using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;


namespace Shiftim.Models
{
    public class Worker
    {
    //    public int WORKER_ID { get; set; }

        [Required(ErrorMessage = " *שדה חובה")]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "תעודת זהות חייבת להכיל 9 ספרות")]
        public string id { get; set; }

        [Required(ErrorMessage = " *שדה חובה")]
        [StringLength(20,MinimumLength =2, ErrorMessage = "שם פרטי חייב להכיל בין 2 ל20 תווים")]
        public string firstName { get; set; }

        [Required(ErrorMessage = " *שדה חובה")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "שם משפחה חייב להכיל בין 2 ל20 תווים")]
        public string lastName { get; set; }

        [Required(ErrorMessage = " *שדה חובה")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "כתובת חייבת להכיל בין 2 ל40 תווים")]
        public string address { get; set; }

        [Required(ErrorMessage = " *שדה חובה")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "טלפון רק מספרים")]
        public string phone { get; set; }
        
        [Key]
        [Required(ErrorMessage = " *שדה חובה")]
        [DataType(DataType.EmailAddress,ErrorMessage ="אימייל לא תקין")]
        [EmailAddress]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string email { get; set; }

        [Required(ErrorMessage = " *שדה חובה")]
        public string role { get; set; }


        public virtual User user1 { get; set; }
        public override string ToString()
        {
            return firstName + " " + lastName + "\n" +id +" " +address + " " + phone + " " + email + " "  + " " + role  ;
        }

    }
}