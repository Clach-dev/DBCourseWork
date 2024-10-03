namespace API.Data.Models
{
    public partial class TradeOutlet
    {
        public int toId { get; set; }

        public string outletName { get; set; }

        public string? outletType { get; set; }

        public double? size { get; set; }

        public double? rent { get; set; }

        public int? counters { get; set; }

        public int coId { get; set; }

        public virtual CommercialOrganization? CommercialOrganization { get; set; }

        public virtual ICollection<OutletSection>? OutletSections { get; set; }
    }
}
