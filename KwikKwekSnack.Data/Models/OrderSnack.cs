using KwikKwekSnack.Data.Models.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KwikKwekSnack.Data.Models
{
    [Table("OrderSnack")]
    public class OrderSnack : OrderItem
    {
        [Required]
        public int SnackId { get; set; }

        [ForeignKey("SnackId")]
        public Snack Snack { get; set; } = null!;

        public virtual List<OrderSnackExtra> OrderSnackExtra { get; set; } = new();
    }
}