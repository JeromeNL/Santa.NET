using KwikKwekSnack.Data.Models;
using KwikKwekSnack.Data.Models.Products;

namespace KwikKwekSnack.Web.Models;

public class SnackExtraViewModel
{
    public Snack SelectedSnack { get; set; }
    
    public OrderSnack OrderSnack { get; set; } = null!;

    public List<SnackExtra> SnackExtras { get; set; } = new();
    
    public List<int> SelectedSnackExtras { get; set; } = new();
}