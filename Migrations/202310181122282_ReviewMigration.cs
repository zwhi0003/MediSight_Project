namespace MediSight_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        RewiewText = c.String(),
                        RewiewRating = c.String(),
                    })
                .PrimaryKey(t => t.ReviewId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reviews");
        }
    }
}
