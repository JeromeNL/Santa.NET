using KwikKwekSnack.Data.Models;
using Newtonsoft.Json;

namespace KwikKwekSnack.Web.Utils;

public class DataUtil
{
    private const string _sessionOrderKey = "order";
    private readonly static JsonSerializerSettings _settings = new() { TypeNameHandling = TypeNameHandling.All };

    public static Order GetOrCreateOrder(ISession httpContextSession)
    {
        var orderString = httpContextSession.GetString(_sessionOrderKey);
        return orderString == null ? new Order() : JsonConvert.DeserializeObject<Order>(orderString, _settings)!;
    }

    public static void AddOrderItemToOrder(ISession httpContextSession, OrderItem orderItem)
    {
        var order = GetOrCreateOrder(httpContextSession);
        order.OrderItems.Add(orderItem);
        var serialize = JsonConvert.SerializeObject(order, _settings);
        httpContextSession.SetString(_sessionOrderKey, serialize);
    }

    public static void DeleteSessionOrder(ISession httpContextSession)
    {
        httpContextSession.Remove(_sessionOrderKey);
    }
}