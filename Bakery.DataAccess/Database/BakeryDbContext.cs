using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DataAccess.Database
{
    public class BakeryDbContext : DbContext
    {
        public BakeryDbContext(DbContextOptions<BakeryDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.Load("Bakery.Model").GetTypes().
              Where(type => !string.IsNullOrEmpty(type.Namespace)).
              Where(type => type.GetInterface(typeof(IEntityTypeConfiguration<>).FullName!) != null);

            foreach (var type in typesToRegister)
            {
                dynamic? configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);

            }
            base.OnModelCreating(modelBuilder);

        }
    }
}
