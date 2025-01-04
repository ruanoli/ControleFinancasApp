using FinancasApp.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Infra.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USER");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Password)
                .HasColumnName("PASSWORD")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ProfileId)
                .HasColumnName("PROFILEID")
                .IsRequired();

            #region
            builder.HasOne(x => x.Profile) //usuário tem um perfil. 1x1
                .WithMany(x => x.Users) //perfil pode ter muiltos usuários
                .HasForeignKey(x => x.ProfileId); //chave estrangeira
            #endregion
        }
    }
}
