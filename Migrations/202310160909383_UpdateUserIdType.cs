namespace MediSight_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserIdType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "UserId", c => c.Int(nullable: false));
        }
    }
}
