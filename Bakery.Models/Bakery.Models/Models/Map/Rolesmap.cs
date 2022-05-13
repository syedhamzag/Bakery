using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Model.Models.Map
{
    public class Rolesmap : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("tbl_Roles");
            builder.HasKey(k => k.RoleId);

            #region Properties
            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .IsRequired(true);

            builder.Property(p => p.Description)
               .HasColumnName("Description")
               .IsRequired(false);
            #endregion

        }
    }
}
