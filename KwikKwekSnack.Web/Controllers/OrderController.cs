using KwikKwekSnack.Data;
using KwikKwekSnack.Data.Models;
using KwikKwekSnack.Web.Models;
using KwikKwekSnack.Web.Utils;
using Microsoft.AspNetCore.Mvc;

namespace KwikKwekSnack.Web.Controllers;

public class OrderController : Controller
{
    [HttpGet]
    public ActionResult SnackPage()
    {
        using var ctx = new KwikKwekSnackContext();
        return View(ctx.Snack.ToList().Where(e => e.InStock));
    }

    [HttpGet]
    public ActionResult SnackExtraPage(int id, [FromQuery] int[] extras)
    {
        using var ctx = new KwikKwekSnackContext();
        var snack = ctx.Snack.Find(id);
        if (id == 0 || snack == null) return RedirectToAction("SnackPage");
        var snackExtraList = ctx.SnackExtra.ToList();
        var orderList = extras.Select(extra => new OrderSnackExtra { SnackExtraId = extra }).ToList();
        return View(new SnackExtraViewModel
        {
            SnackExtras = snackExtraList,
            OrderSnack = new OrderSnack
            {
                SnackId = id,
                OrderSnackExtra = orderList
            },
            SelectedSnack = snack,
            SelectedSnackExtras = extras.ToList()
        });
    }

    [HttpGet]
    public ActionResult AddExtraToSnack(int id, [FromQuery] int?[] extras)
    {
        return RedirectToAction(nameof(SnackExtraPage), new { id, extras });
    }

    [HttpPost]
    public ActionResult SnackExtraPage(SnackExtraViewModel snackExtraViewModel)
    {
        DataUtil.AddOrderItemToOrder(HttpContext.Session, snackExtraViewModel.OrderSnack);
        return RedirectToAction(nameof(DrinkPage));
    }

    [HttpGet]
    public ActionResult DrinkPage()
    {
        using var ctx = new KwikKwekSnackContext();
        return View(ctx.Drink.ToList().Where(e => e.InStock == true));
    }

    [HttpGet]
    public ActionResult DrinkExtraPage(int id)
    {
        using var ctx = new KwikKwekSnackContext();
        var drink = ctx.Drink.Find(id);
        if (id == 0 || drink == null) return RedirectToAction("DrinkPage");
        return View(new OrderDrink
        {
            Drink = drink,
            DrinkId = id
        });
    }

    [HttpPost]
    public ActionResult DrinkExtraPage(OrderDrink orderDrink)
    {
        DataUtil.AddOrderItemToOrder(HttpContext.Session, orderDrink);
        return RedirectToAction(nameof(CheckoutPage));
    }

    [HttpGet]
    public ActionResult CheckoutPage()
    {
        return View(new CheckOutViewModel
        {
            Order = DataUtil.GetOrCreateOrder(HttpContext.Session)
        });
    }

    [HttpGet]
    public ActionResult ConfirmOrderPage(CheckOutViewModel checkout)
    {
        var order = DataUtil.GetOrCreateOrder(HttpContext.Session);
        if (!ModelState.IsValid || order.OrderItems.Count == 0) return RedirectToAction(nameof(SnackPage));
        if (order.OrderItems.Count == 0) return View("SnackPage");
        using var ctx = new KwikKwekSnackContext();
        order.TotalPrice = order.CalculateTotalPrice();
        order.RetrievalType = checkout.RetrievalType;

        ctx.Order.Add(order);
        ctx.SaveChanges();
        DataUtil.DeleteSessionOrder(HttpContext.Session);
        return View(order);
    }
}