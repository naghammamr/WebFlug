using System;
using System.ComponentModel;
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

        [DisplayName("Status")]
        public string OfferSatatus { get; set; }

        [Required(ErrorMessage = "Choose a date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliverDate { get; set; }

        // Foreign key 
        public int Order_Id { get; set; }

        [ForeignKey("Order_Id")]
        public virtual Orders orders { get; set; }
        
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser user { get; set; }

    }
}