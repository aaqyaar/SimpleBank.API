using System;
using Microsoft.EntityFrameworkCore;
using SimpleBank.API.Entities;

namespace SimpleBank.API.DbContexts
{
	public class BranchContext: DbContext
	{
		public IConfiguration configuration;
		public DbSet<Branch> Branches { get; set; } = null!;
		public DbSet<Teller> Tellers { get; set; } = null!;

		public BranchContext(IConfiguration _config) {
			configuration = _config;
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Branch>().HasData(
				new Branch()
				{
					Id = "1",
					Address = "Juungal, Yaaqshid",
					Phone = "2011"
				},
                new Branch()
                {
                    Id = "2",
                    Address = "Juungal, Yaaqshid",
                    Phone = "2019"
                },
                new Branch()
                {
                    Id = "3",
                    Address = "Towfiiq, Yaaqshid",
                    Phone = "4080"
                }
                );
            modelBuilder.Entity<Teller>().HasData(
                new Teller()
                {
                    Id = "TEL001",
                    BranchId = "1",
                    Name = "Jaamac",
                    Phone = "0618977249"
                },
                new Teller()
                {
                    Id = "TEL002",
                    BranchId = "2",
                    Name = "Jaamac",
                    Phone = "0618977241"
                },
                new Teller()
                {
                    Id = "TEL003",
                    BranchId = "3",
                    Name = "Jaamac",
                    Phone = "0618977240"
                });
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql(configuration.GetConnectionString("DatabaseURL"));

    }

	//@"Host=localhost;Username=abdizamed;Password=aaqyaar@10;Database=simple_bank"
}

