namespace API.Data.Models
{
    public partial class Seller
    {
        public int selId { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string? patrynomic { get; set; }
        public double salary { get; set; }
        public string phoneNumber { get; set; }
        public DateOnly? endOfContract { get; set; }

        public int osId { get; set; }

        public virtual OutletSection? OutletSection { get; set; }
        public virtual ICollection<Deal>? Deals { get; set; }

    }
}
