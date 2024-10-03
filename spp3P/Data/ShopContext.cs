using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using spp3P.Data.Models;

namespace spp3P.Data
{
    public partial class ShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BonusCard> BonusCards { get; set; }


    }
}
