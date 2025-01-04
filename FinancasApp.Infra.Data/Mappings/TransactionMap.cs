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
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("TRANSACTION");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
               .HasColumnName("DESCRIPTION");

            builder.Property(x => x.DataTransaction)
                .HasColumnName("DATATRANSACTION")
                .HasColumnType("date");

            builder.Property(x => x.Value)
                .HasColumnName("VALUE")
                .HasColumnType("decimal(10, 2)");

            builder.Property(x => x.Type)
                .HasColumnName("TYPE")
                .IsRequired();

            builder.Property(x => x.CategoryId)
                .HasColumnName("CATEGORYID")
                .IsRequired();

            builder.Property(x => x.UserId)
                .HasColumnName("USERID")
                .IsRequired();

            #region RELAÇÕES
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
