using KwikKwekSnack.Data;
using KwikKwekSnack.Data.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace KwikKwekSnack.Web.Controllers
{
    public class SnackController : Controller
    {
        // GET: SnackController
        public ActionResult Index()
        {
            using var ctx = new KwikKwekSnackContext();
            var snacks = ctx.Snack.ToList();
            return View(snacks);
        }

        // GET: SnackController/Details/5
        public ActionResult Details(int id)
        {
            using var ctx = new KwikKwekSnackContext();
            var snack = ctx.Snack.Find(id);
            return View(snack);
        }

        // GET: SnackController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SnackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Snack model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);
                using var ctx = new KwikKwekSnackContext();
                ctx.Snack.Add(model);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SnackController/Edit/5
        public ActionResult Edit(int id)
        {
            using var ctx = new KwikKwekSnackContext();
            var snack = ctx.Snack.Find(id);
            return View(snack);
        }

        // POST: SnackController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Snack model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new KwikKwekSnackContext())
                {
                    ctx.Snack.Update(model);
                    ctx.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
}