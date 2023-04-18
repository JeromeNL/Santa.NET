using ConsoleTables;
using KwikKwekSnack.Data;
using KwikKwekSnack.Data.Models;

namespace KwikKwekSnack.Console.Utils;

public class OrderUtil
{
    public static List<Order?[]> RequestOrdersToCombinedList(KwikKwekSnackContext ctx)
    {
        // Retrieve all the orders
        var placedOrders = ctx.Order
            .Where(o => o.Status == OrderStatus.InWachtrij)
            .OrderBy(o => o.OrderDate)// Newest orders at the bottom
            .Take(6)// Limit to 6 orders 
            .ToList();
        var preparingOrder = ctx.Order
            .Where(o => o.Status == OrderStatus.WordtBereid)
            .OrderBy(o => o.OrderDate)
            .ToList();
        var readyOrders = ctx.Order
            .Where(o => o.Status == OrderStatus.Gereed)
            .OrderByDescending(o => o.OrderDate)// Newest orders at the top
            .Take(1)
            .ToList();

        // Combine orders for table
        return TableUtil.CombineOrderListsToList(placedOrders, preparingOrder, readyOrders);
    }

    public static void PrintOrderStatus(List<Order?[]> combinedList)
    {
        // Configure table
        const string extraWhiteSpace = "   ";
        var table = new ConsoleTable
        {
            Columns =
            {
                OrderStatus.InWachtrij + extraWhiteSpace,
                OrderStatus.WordtBereid + extraWhiteSpace,
                OrderStatus.Gereed
            },
            Options = { EnableCount = false }
        };

        // Fill table
        foreach (var order in combinedList){
            table.AddRow(order[0]?.Id, order[1]?.Id, order[2]?.Id);
        }

        // Print table
        table.Write(Format.Minimal);
    }

    public static void ReplaceOrdersAtTick(KwikKwekSnackContext ctx)
    {
        // Get oldest order in 'Wordtbereid' (or null)
        var orderReady = ctx.Order.Where(o => o.Status == OrderStatus.WordtBereid)
            .OrderBy(o => o.OrderDate)
            .FirstOrDefault();

        // set order to next step (Gereed)
        if (orderReady != null){
            orderReady.Status = OrderStatus.Gereed;
            ctx.Update(orderReady);
            ctx.SaveChanges();
        }

        // Get current order in 'Wachtrij' or null
        var oldestOrder = ctx.Order.Where(o => o.Status == OrderStatus.InWachtrij)
            .OrderBy(o => o.OrderDate)
            .FirstOrDefault();

        // set order to next step (WordtBereid)
        if (oldestOrder != null){
            oldestOrder.Status = OrderStatus.WordtBereid;
            ctx.Update(oldestOrder);
            ctx.SaveChanges();
        }
    }
}