using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFlug.Models
{
    public class Offers
    {
        [Key]
        public int Offer_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TravellerReward { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime CreationDate { get; set; }

        [StringLength(20)]
        public string OfferSatatus { get; set; }

        [Required(ErrorMessage = "Choose a date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliverDate { get; set; }

        // Foreign key 
        public int Order_Id { get; set; }

        [ForeignKey("Order_Id")]
        public virtual Orders orders { get; set; }
        
        public String UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser user { get; set; }

    }
}