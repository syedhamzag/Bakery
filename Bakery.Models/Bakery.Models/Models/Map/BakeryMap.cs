using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Model.Models.Map
{
    internal class BakeryMap : IEntityTypeConfiguration<Bakerys>
    {
        public void Configure(EntityTypeBuilder<Bakerys> builder)
        {
            builder.ToTable("tbl_Bakery");
            builder.HasKey(k => k.BakeryId);

            #region Properties
            builder.Property(p => p.BakeryId)
                .HasColumnName("BakeryId")
                .IsRequired(true);

            builder.Property(p => p.Name)
               .HasColumnName("Name");

            builder.Property(p => p.Description)
                .HasColumnName("Description");

            builder.Property(p => p.Address)
                .HasColumnName("Address");

            builder.Property(p => p.City)
                .HasColumnName("City");

            builder.Property(p => p.Contact)
                .HasColumnName("Contact");
            #endregion

            #region Relationship
            #endregion
        }
    }
}
