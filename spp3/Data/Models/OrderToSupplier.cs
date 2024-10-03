namespace API.Data.Models
{
    public partial class OrderToSupplier
    {
        public int orId { get; set; }
        public int suId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Supplier? Supplier { get; set; }
    }
}
