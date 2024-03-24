using Dapper;
using KafeshkaV2.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KafeshkaV2.Infrastructure;

    public class KafeshkaUserDbContext : IdentityDbContext<KafeshkaAppUser>
    {

        public KafeshkaUserDbContext(DbContextOptions<KafeshkaUserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<KafeshkaAppUser>().ToTable("AspNetUsers");
        }
    }