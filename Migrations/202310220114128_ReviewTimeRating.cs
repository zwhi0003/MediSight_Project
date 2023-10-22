namespace MediSight_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewTimeRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reviews", "RewiewRating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "RewiewRating", c => c.String());
            DropColumn("dbo.Reviews", "Time");
        }
    }
}
