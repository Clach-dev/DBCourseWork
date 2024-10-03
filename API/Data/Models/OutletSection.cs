namespace API.Data.Models
{
    public partial class OutletSection
    {
        public int osId { get; set; }

        public string sectionName { get; set; }

        public int? sectionFloor { get; set; }

        public string? sectionHall { get; set; }

        public int toId { get; set; }

        public virtual TradeOutlet? TradeOutlet { get; set; }
        public virtual SectionManager? SectionManager { get; set; }
        public virtual ICollection<Seller>? Sellers { get; set; }
    }
}
