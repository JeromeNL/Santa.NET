using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit.Sdk;

namespace KwikKwekSnack.Data.Models.Products
{
    public class SnackExtra
    {
        [Column(nameof(SnackExtra) + "Id")]
        public int Id { get; set; }


        [DisplayName("Naam")]
        [Required(ErrorMessage = "Je moet een naam opgeven (lengte: 1-50)")]
        public string Name { get; set; } = null!;
        
        [DisplayName("Prijs")]
        [Range(0.0, 99.99, ErrorMessage = "Dit is geen geldige prijs (€0,01 - €99,99)")]
        [Required(ErrorMessage = "Je moet een prijs opgeven (€0,01 - €99,99)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Dit is geen geldige prijs (€0,01 - €999,99)")]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal Price { get; set; }
        
        public virtual ICollection<OrderSnackExtra> OrderSnackExtra { get; set; } = null!;
        
    }
}