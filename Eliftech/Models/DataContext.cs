using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Eliftech.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base ("DBConnection")
        { }

        public DbSet<Company> Companies { set; get; }

    }
}