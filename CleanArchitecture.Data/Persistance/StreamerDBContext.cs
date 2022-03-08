using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistance
{
    public class StreamerDBContext : DbContext
    {


        public StreamerDBContext(DbContextOptions<StreamerDBContext> options) : base(options)
        {

        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{


        //    optionsBuilder.UseSqlServer(@"Data Source=TITAN\SQLEXPRESS;Initial Catalog=Streamer;Integrated Security=True;")
        //        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
        //        .EnableSensitiveDataLogging();

        //}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "system";
                        break;

                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Streamer>()
                .HasMany(o => o.Videos)
                .WithOne( m => m.Streamer)
                .HasForeignKey( m => m.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Video>()
                .HasMany(x => x.Actors)
                .WithMany(t => t.Videos)
                .UsingEntity<VideoActor>(pt => pt.HasKey(e => new { e.ActorId, e.VideoId }));
            
        }
        public DbSet<Streamer>? Streamers { get; set; }  
        public DbSet<Video>? Videos { get; set; }   
        

    }
}
