using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Library.Models
{
    public class Author
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name can not exceed 50 symbols")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name can not exceed 50 symbols")]
        public string LName { get; set; }

        public string FullName {
            get
            {
                return FName + " " + LName;
            }
        }
    }
}