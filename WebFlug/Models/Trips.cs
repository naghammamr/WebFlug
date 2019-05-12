using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebFlug.Models
{
    public class Trips
    {
        [Key]
        public int Trip_Id { get; set; }

        [Required(ErrorMessage = "your trip starts from where?")]
        [StringLength(70)]
        public string FromWhere { get; set; }

        [Required(ErrorMessage = "which place where are you going to?")]
        [StringLength(70)]
        public string ToWhere { get; set; }

        [Required]
        public DateTime DepartDate { get; set; }

        [Required]
        public System.DateTime TripCreationDate { get; set; }

        /// 
        /// /////////////////////////////////////
        /// 

        [Required]
        public string TicketPhoto { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string AdditionalDetails { get; set; }
    }
}