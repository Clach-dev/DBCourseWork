using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace spp3P.Data.Models
{
    public partial class Customer
    {
        public int CuId { get; set; }

        public string SecondName { get; set; } = null!;

        public int? age { get; set; } = null;

        public string Gender { get; set; } = null!;

        public virtual BonusCard? BonusCard { get; set; }
    }
}
