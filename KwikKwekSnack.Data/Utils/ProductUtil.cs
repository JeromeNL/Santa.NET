using KwikKwekSnack.Data.Models;
using KwikKwekSnack.Data.Models.Products;
using System.Collections.Generic;
using System.Linq;

namespace KwikKwekSnack.Data.Utils;

public class ProductUtil
{
    public static Product? GetProductFromOrderItem(OrderItem orderItem)
    {
        using var ctx = new KwikKwekSnackContext();
        Product? product = orderItem switch// Get product from snack/drink
        {
            OrderSnack item => ctx.Snack.Find(item.SnackId),
            OrderDrink item => ctx.Drink.Find(item.DrinkId),
            _ => null
        };
        return product;
    }

    public static decimal CalculateProductPrice(OrderItem orderItem)
    {
        return orderItem switch
        {
            OrderDrink orderDrink => CalculateDrinkPrice(orderDrink),
            OrderSnack orderSnack => CalculateSnackPrice(orderSnack),
            _ => 0m
        };
    }

    private static decimal CalculateDrinkPrice(OrderDrink orderDrink)
    {
        var total = 0m;
        using var ctx = new KwikKwekSnackContext();
        var drinkPrice = ctx.Drink.Find(orderDrink.DrinkId)!.Price;
        switch (orderDrink.Size) {
            case DrinkSize.Small:
                total += drinkPrice;
                break;
            case DrinkSize.Medium:
                total += drinkPrice * 1.25m;
                break;
            case DrinkSize.Large:
                total += drinkPrice * 1.50m;
                break;
            case DrinkSize.ExtraLarge:
                total += drinkPrice * 1.75m;
                break;
        }
        if (orderDrink.HasIce) total += 0.15m;
        if (orderDrink.HasStraw) total += 0.1m;
        return decimal.Round(total * orderDrink.Amount, 2);
    }

    private static decimal CalculateSnackPrice(OrderSnack orderSnack)
    {
        using var ctx = new KwikKwekSnackContext();
        var snackPrice = ctx.Snack.Find(orderSnack.SnackId)!.Price;
        var snackExtraPrice = orderSnack.OrderSnackExtra.Sum(extra => ctx.SnackExtra.Find(extra.SnackExtraId)!.Price);
        return decimal.Round((snackPrice + snackExtraPrice) * orderSnack.Amount, 2);
    }

    public static string GenerateProductDescription(OrderItem item)
    {
        var descItems = new List<string>();
        switch (item) {
            case OrderSnack snack:
            {
                using var ctx = new KwikKwekSnackContext();
                descItems.AddRange(snack.OrderSnackExtra.Select(orderSnack => ctx.SnackExtra.Find(orderSnack.SnackExtraId)!.Name));
                break;
            }
            case OrderDrink drink:
                descItems.Add(drink.Size.ToString());
                if (drink.HasIce) descItems.Add("Ijs");
                if (drink.HasStraw) descItems.Add("Rietje");
                break;
        }
        return string.Join(", ", descItems);
    }
}