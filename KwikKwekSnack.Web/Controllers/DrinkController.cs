using KwikKwekSnack.Data;
using KwikKwekSnack.Data.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace KwikKwekSnack.Web.Controllers
{
    public class DrinkController : Controller
    {
        // GET: DrinkController
        public ActionResult Index()
        {
            using var ctx = new KwikKwekSnackContext();
            var drinks = ctx.Drink.ToList();
            return View(drinks);
        }

        // GET: DrinkController/Details/5
        public ActionResult Details(int id)
        {
            using var ctx = new KwikKwekSnackContext();
            var drink = ctx.Drink.Find(id);
            return View(drink);
        }

        // GET: DrinkController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DrinkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Drink model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);
                using var ctx = new KwikKwekSnackContext();
                ctx.Drink.Add(model);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                return View(model);
            }
        }


        // GET: DrinkController/Edit/5
        public ActionResult Edit(int id)
        {
            using var ctx = new KwikKwekSnackContext();
            var drink = ctx.Drink.Find(id);
            return View(drink);
        }

        // POST: DrinkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Drink model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);
                using var ctx = new KwikKwekSnackContext();
                ctx.Drink.Update(model);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: DrinkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Drink model)
        {
            using var ctx = new KwikKwekSnackContext();
            ctx.Drink.Remove(model);
            ctx.SaveChanges();
            return View("Index", ctx.Drink.ToList());
        }
    }
}