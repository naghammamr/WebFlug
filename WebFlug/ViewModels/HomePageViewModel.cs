using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFlug.Models;

namespace WebFlug.ViewModels
{
    public class HomePageViewModel
    {
        public Orders Orderviewmodel { get; set; }

        public List<Trips> Tripsviewmodel { get; set; }
    }
}