namespace MediSight_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewUpdateConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "ReviewText", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "ReviewText", c => c.String());
        }
    }
}
