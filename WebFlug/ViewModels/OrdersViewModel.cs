using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebFlug.Models;

namespace WebFlug.ViewModels
{
    public class OrdersViewModel
    {
        public Orders order { get; set; }

        public List<Offers> Offers { get; set; }

        public Offers offer { get; set; }
        
    }
}