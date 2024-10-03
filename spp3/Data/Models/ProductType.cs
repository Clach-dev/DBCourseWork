namespace spp3.Data.Models
{
    public partial class ProductType
    {
        public int ptId { get; set; }
        public string name { get; set; }
        public int? ageLimit { get; set; } = 0;
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
