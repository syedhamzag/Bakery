using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Model.Models.Map
{
    public class UsersMap : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("tbl_Users");
            builder.HasKey(k => k.UserId);

            #region Properties
            builder.Property(p => p.Name)
                .HasColumnName("Name");

            builder.Property(p => p.Cell)
                .HasColumnName("Cell");

            builder.Property(p => p.Password)
                .HasColumnName("Password");

            builder.Property(p => p.Email)
                .HasColumnName("Email");

            builder.Property(p => p.RoleId)
                .HasColumnName("RoleId");

            builder.Property(p => p.IsActive)
                .HasColumnName("IsActive");
            #endregion

            #region Relationship
            builder.HasOne(p => p.Role)
                 .WithMany(p => p.User)
                 .HasForeignKey(p => p.RoleId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
