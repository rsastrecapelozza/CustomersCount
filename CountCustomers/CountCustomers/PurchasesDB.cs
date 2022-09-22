using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CountCustomers
{
    public class PurchasesDB : DbContext
    {
        public DbSet<Purchase> Purchases { get; set; }
    }
}

