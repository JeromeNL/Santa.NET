using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace KwikKwekSnack.Data.Models.Products
{
    public abstract class Product
    {
        [Required]
        [Column(nameof(Product) + "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Je moet een naam opgeven (lengte: 1-50)")]
        [MaxLength(50)]
        [DisplayName("Naam")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Je moet een beschrijving opgeven (lengte: 1-500)")]
        [MaxLength(500)]
        [DisplayName("Beschrijving")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Je moet een URL opgeven.")]
        [RegularExpression(@"(https:\/\/)([^\s([""<,>/]*)(\/)[^\s["",><]*(.png|.jpg)(\?[^\s["",><]*)?", ErrorMessage = "Deze URL gaat niet werken..")]
        [DisplayName("Afbeelding")]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = "Je moet een prijs opgeven.")]
        [Range(0.0, 999.99, ErrorMessage = "Dit is geen geldige prijs (€0,01 - €999,99)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Dit is geen geldige prijs (€0,01 - €999,99)")]
        [DisplayName("Prijs")]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        [DisplayName("Voorraadstatus")]
        public bool InStock { get; set; }
    }
}