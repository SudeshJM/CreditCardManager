using Card.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Card.DataAccess.Configuration
{
    class CardEntityConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> categoryConfig)
        {
            categoryConfig.HasKey(c => c.Id);
            categoryConfig.Property(c => c.Id);
            categoryConfig.Property<String>("Name").IsRequired().HasMaxLength(100);
            categoryConfig.Property<String>("CardNumber").IsRequired().HasMaxLength(19);
            categoryConfig.Property<Decimal>("Limit").IsRequired();
            categoryConfig.Property<Decimal>("Balance");
        }

    }
}
