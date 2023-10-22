namespace MediSight_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "ReviewText", c => c.String());
            AddColumn("dbo.Reviews", "ReviewRating", c => c.Int(nullable: false));
            DropColumn("dbo.Reviews", "RewiewText");
            DropColumn("dbo.Reviews", "RewiewRating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "RewiewRating", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "RewiewText", c => c.String());
            DropColumn("dbo.Reviews", "ReviewRating");
            DropColumn("dbo.Reviews", "ReviewText");
        }
    }
}
