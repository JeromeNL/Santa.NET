using KwikKwekSnack.Data.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KwikKwekSnack.Data.Models
{
    public class Order
    {
        [Column(nameof(Order) + "Id")]
        [DisplayName("Bestelnummer")]
        public int Id { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderStatus Status { get; set; }

        [DisplayName("Hier opeten of meenemen")]
        public RetrievalType RetrievalType { get; set; }

        [DisplayName("Totaal prijs")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [DisplayName("Bestel tijdstip")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal CalculateTotalPrice()
        {
            var total = OrderItems.Sum(ProductUtil.CalculateProductPrice);
            return decimal.Round(total, 2);
        }
    }

    public enum OrderStatus
    {
        InWachtrij,
        WordtBereid,
        Gereed
    }

    public enum RetrievalType
    {
        Afhalen,
        HierOpeten
    }
}