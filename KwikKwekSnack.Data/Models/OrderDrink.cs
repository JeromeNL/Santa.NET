using KwikKwekSnack.Data.Models.Products;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KwikKwekSnack.Data.Models
{
    [Table("OrderDrink")]
    public class OrderDrink : OrderItem
    {
        [Required]
        public int DrinkId { get; set; }

        [ForeignKey("DrinkId")]
        public Drink Drink { get; set; } = null!;

        [DisplayName("Grootte")]
        public DrinkSize Size { get; set; }

        [DisplayName("IJs")]
        public bool HasIce { get; set; }

        [DisplayName("Rietje")]
        public bool HasStraw { get; set; }
    }

    public enum DrinkSize
    {
        Small,
        Medium,
        Large,
        ExtraLarge
    }
}