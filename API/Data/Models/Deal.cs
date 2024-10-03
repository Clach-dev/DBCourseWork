using spp3.Data.Models;

namespace API.Data.Models
{
    public class Deal
    {
        public int dlId { get; set; }
        public DateTime dealMoment { get; set; }

        public int cuId { get; set; }
        public int prId { get; set; }
        public int selId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Seller? Seller { get; set; }
        public virtual Product? Product { get; set; }
    }
}
