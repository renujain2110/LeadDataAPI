using LeadDataAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadDataAPI.Helpers
{
    /// <summary>
    /// Data context class for handling in memory data
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Lead> Leads { get; set; }
    }
}
