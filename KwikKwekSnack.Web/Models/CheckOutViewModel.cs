using KwikKwekSnack.Data.Models;

namespace KwikKwekSnack.Web.Models
{
    public class CheckOutViewModel
    {
        public RetrievalType RetrievalType { get; set; }
        public Order? Order { get; set; } 
        public List<RetrievalType> retrievalTypes = Enum.GetValues(typeof(RetrievalType))
                            .Cast<RetrievalType>()
                            .ToList();
    }
}
