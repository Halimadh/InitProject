using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Context
{
    public class DataLayerContext : DbContext
    {
        public DataLayerContext(DbContextOptions<DataLayerContext> options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
      

    }
}
