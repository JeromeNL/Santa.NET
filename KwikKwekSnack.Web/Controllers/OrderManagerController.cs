using KwikKwekSnack.Data;
using KwikKwekSnack.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikKwekSnack.Web.Controllers
{
    public class OrderManagerController : Controller
    {
        // GET: OrderController
        public ActionResult Index()
        {
            using var ctx = new KwikKwekSnackContext();
            var orders = ctx.Order.ToList();
            return View(orders);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            using var ctx = new KwikKwekSnackContext();
            var order = ctx.Order
                .Where(t => t.Id == id)
                .Include(t => t.OrderItems)
                .FirstOrDefault();
            return View(order);
        }
        
    }
}