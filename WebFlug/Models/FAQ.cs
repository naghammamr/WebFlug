using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebFlug.Models
{
    public class FAQ
    {
        [Key]
        public int FAQ_ID { get; set; }

        [Required(ErrorMessage = "provide THE QUESTION please...")]
        [DisplayName("Question")]
        public string Question { get; set; }

        [Required(ErrorMessage = "provide an ANSWER please...")]
        [DisplayName("Answer")]
        public string Answer { get; set; }
    }
}