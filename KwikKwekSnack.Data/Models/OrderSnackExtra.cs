using KwikKwekSnack.Data.Models.Products;

namespace KwikKwekSnack.Data.Models
{
    public class OrderSnackExtra
    {
        public int OrderSnackId { get; set; }
        public OrderSnack OrderSnack { get; set; } = null!;

        public int SnackExtraId { get; set; }
        public SnackExtra SnackExtra { get; set; } = null!;
    }
}