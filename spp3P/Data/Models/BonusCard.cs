using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace spp3P.Data.Models
{
    public partial class BonusCard
    {
        public int BcId { get; set; }

        public string Number { get; set; } = null!;

        public float Discount { get; set; }

        public int? CuId { get; set; }

        public virtual Customer? Cu { get; set; }
    }
}
