using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Library.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "A Title can not exceed 200 symbols")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Published Date is required")]
        [RegularExpression("^(?:(?:31(\\/|-|\\.)(?:0?[13578]|1[02]))\\1|(?:(?:29|30)(\\/|-|\\.)(?:0?[1,3-9]|1[0-2])\\2))(?:(?:1[6-9]|[2-9]\\d)?\\d{2})$|^(?:29(\\/|-|\\.)0?2\\3(?:(?:(?:1[6-9]|[2-9]\\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\\d|2[0-8])(\\/|-|\\.)(?:(?:0?[1-9])|(?:1[0-2]))\\4(?:(?:1[6-9]|[2-9]\\d)?\\d{2})$", ErrorMessage = "Date format must be dd.mm.yyyy")]
        public DateTime PublishedDate { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(20, ErrorMessage = "An ISBN can not exceed 20 symbols")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public int AuthorId { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string AuthorName
        {
            get
            {
                return FName + " " + LName;
            }
        }
    }
}