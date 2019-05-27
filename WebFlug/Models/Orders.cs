using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebFlug.Models
{
    //[Bind(Exclude = "Order_Id")]
    public class Orders
    {
        [Key]
        public int Order_Id { get; set; }

        [DisplayName("Order Code")]
        public string OrderNumber { get; set; }

        [Required(ErrorMessage = "Product Name is Required!")]
        [StringLength(150)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Details is Required!")]
        public string ProductDetails { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 10.000")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 30, ErrorMessage = "Quantity must be between 1 and 30")]
        public int ProductQuantity { get; set; }

        [DisplayName("Item Picture")]
        public string ProductImage { get; set; }

        public string ProductLink { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        public float ProductWeight { get; set; }

        [Required(ErrorMessage = "Choose a place please")]
        public string Deliverfrom { get; set; }

        [Required(ErrorMessage = "Enter a city please")]
        public string DeliverTo { get; set; }

        [Required(ErrorMessage = "Choose a date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliverDate { get; set; }

        public string AdditionalDetails { get; set; }

        [DisplayName("Order Status")]
        public string OrderSatatus { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime CreationDate { get; set; }

        /// 
        /// /////////////////////////////////////
        /// 

        [EmailAddress]
        public string Email { get; set; }

    }
}