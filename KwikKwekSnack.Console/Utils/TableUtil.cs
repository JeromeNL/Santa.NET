using KwikKwekSnack.Data.Models;

namespace KwikKwekSnack.Console.Utils;

public static class TableUtil
{
    public static List<Order?[]> CombineOrderListsToList(List<Order> list1, List<Order> list2, List<Order> list3)
    {
        var newList = new List<Order?[]>();
        var highestCount = Math.Max(list1.Count, Math.Max(list2.Count, list3.Count));

        for (var i = 0; i < highestCount; i++)
        {
            newList.Add(new[]
            {
                list1.Count > i ? list1[i] : null,
                list2.Count > i ? list2[i] : null,
                list3.Count > i ? list3[i] : null
            });
        }

        return newList;
    }
}