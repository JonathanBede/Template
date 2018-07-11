using System.Data.Entity;
using Tornado.DataAccess.Migrations;
using Tornado.Domain.Entities.Api;

namespace Tornado.DataAccess.Contexts
{
    public class MainContext : DbContext
    {
       
        public DbSet<GameEntity> GameSet { get; set; }
        

        public MainContext() : base("name=Main")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //API Mappings

            modelBuilder
                .Entity<GameEntity>()
                .ToTable("Games");
        }

    }
}
