using Card.Domain.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Card.DataAccess.Configuration
{
    public class CardDbContext :DbContext
    {
        internal DbSet<CreditCard> CreditCard { get; set; }

        /// <summary>
        /// The entity framework db context.
        /// </summary>
        /// <param name="options">The db context options.</param>
        /// <param name="env">Current hosted environment.</param>
        public CardDbContext(DbContextOptions options, IHostingEnvironment env) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
