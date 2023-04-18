using System.ComponentModel.DataAnnotations.Schema;

namespace KwikKwekSnack.Data.Models
{
    public abstract class OrderItem
    {
        [Column(nameof(OrderItem) + "Id")]
        public int Id { get; set; }

        public Order Order { get; set; } = null!;

        public int Amount { get; set; } = 1;
    }
}