namespace Tornado.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
           CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Type = c.String(),
                        PercentBandMin = c.Double(nullable: false),
                        PercentBandMax = c.Double(nullable: false),
                        AddWordsBand1 = c.Int(),
                        AddWordsBand2 = c.Int(),
                        ReEnterPositionMin = c.Int(),
                        ReEnterPositionMax = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Games");
        }
    }
}
