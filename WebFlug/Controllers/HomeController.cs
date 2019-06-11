using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebFlug.Models;

namespace WebFlug.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Order
        public IEnumerable<Orders> GetOrders()
        {
            var orders = db.orders.Where(x => x.OrderSatatus == "Requested").ToList();
            return orders;
        }

        public ActionResult Index()
        {
            var orders = GetOrders();
            
            return View(orders);
        }

        public ActionResult blocked()
        {
            return View();
        }
    }
}