using Microsoft.EntityFrameworkCore;
using SignalR.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>()
                .Property(m => m.SenderId)
                .IsRequired();

            builder.Entity<Message>()
                .Property(m => m.RecipientId)
                .IsRequired();

            builder.Entity<Message>()
               .Property(m => m.Content)
               .IsRequired();
        }
    }
}
