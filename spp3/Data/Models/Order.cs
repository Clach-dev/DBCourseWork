namespace API.Data.Models
{
    public partial class Order
    {
        public int orId { get; set; }
        public int orderNumber { get; set; }
        public string orderStatus { get; set; }
        public int quantity { get; set; }

        public virtual ICollection<ProductToOrder>? ProductsToOrders { get; set; }
        public virtual ICollection<OrderToSupplier>? OrdersToSuppliers { get; set; }
    }
}
