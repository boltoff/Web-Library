using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Library.Models
{
    public class Book
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedDate { get; set; }

        public string ISBN { get; set; }

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