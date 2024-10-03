using spp3.Data.Models;

namespace API.Data.Models
{
    public partial class ProductToOrder
    {
        public int prId { get; set; }
        public int orId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
