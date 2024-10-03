using API.Data.Models;

namespace spp3.Data.Models
{
    public partial class Product
    {
        public int prId { get; set; }

        public string name { get; set; }

        public double? price { get; set; }

        public int? quantity { get; set; }

        public int? ptId { get; set; }

        public virtual ProductType? ProductType { get; set; }

        public virtual ICollection<ProductToOrder>? ProductsToOrders { get; set; }

        public virtual ICollection<Deal>? Deals { get; set; }

    }
}
