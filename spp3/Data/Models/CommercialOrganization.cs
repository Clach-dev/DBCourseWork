namespace API.Data.Models
{
    public partial class CommercialOrganization
    {
        public int coId { get; set; }
        public string organizationName { get; set; }
        public virtual ICollection<TradeOutlet>? TradeOutlets { get; set; }

    }
}
