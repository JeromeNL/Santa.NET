using KwikKwekSnack.Data;
using KwikKwekSnack.Data.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace KwikKwekSnack.Web.Controllers
{
    public class SnackExtraController : Controller
    {
        // GET: SnackExtraController
        public ActionResult Index()
        {
            using var ctx = new KwikKwekSnackContext();
            var snacks = ctx.SnackExtra.ToList();
            return View(snacks);
        }

        // GET: SnackExtraController/Details/5
        public ActionResult Details(int id)
        {
            using var ctx = new KwikKwekSnackContext();
            var snack = ctx.SnackExtra.Find(id);
            return View(snack);
        }

        // GET: SnackExtraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SnackExtraController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SnackExtra model)
        {
            try
            {
                using var ctx = new KwikKwekSnackContext();
                ctx.SnackExtra.Add(model);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: SnackExtraController/Edit/5
        public ActionResult Edit(int id)
        {
            using var ctx = new KwikKwekSnackContext();
            var snack = ctx.SnackExtra.Find(id);
            return View(snack);
        }

        // POST: SnackExtraController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SnackExtra model)
        {
            try
            {
                using var ctx = new KwikKwekSnackContext();
                ctx.SnackExtra.Update(model);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}