namespace API.Data.Models
{
    public partial class SectionManager
    {
        public int smId { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string? patrynomic { get; set; }
        public double? salary { get; set; }
        public string phoneNumber { get; set; }
        public double? experience { get; set; }
        public int osId { get; set; }
        public virtual OutletSection? OutletSection { get; set; }
    }
}
