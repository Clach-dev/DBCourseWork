namespace API.Data.Models
{
    public partial class Supplier
    {
        public int suId { get; set; }
        public string supplierName { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }

        public virtual ICollection<OrderToSupplier>? OrdersToSuppliers { get; set; }
    }
}
