﻿using ISH.Data.Authentication;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ISH.Data.Orders
{
    public class Order : BaseEntity
    {
        /**
         * Order serves as the "aggregate" for an order, more specifically, something like a header for the invoice (and it should be the header)
         */

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderNumber { get; set; }
        
        [Required]
        public int TotalPrice { get; set; }
        
        public User OrderedBy { get; set; }
    }

    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasIndex(x => x.OrderNumber).IsUnique();
            builder.HasOne(x => x.OrderedBy).WithMany();
        }
    }
}
