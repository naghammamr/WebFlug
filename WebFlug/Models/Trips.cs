using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required(ErrorMessage = "Choose a date")]
        [DataType(DataType.Date)]
        public DateTime DepartDate { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime TripCreationDate { get; set; }

        /// 
        /// /////////////////////////////////////
        /// 

        public string TicketPhoto { get; set; }

        public string AdditionalDetails { get; set; }

        // Foreign key 
        public String UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser user { get; set; }
    }
}