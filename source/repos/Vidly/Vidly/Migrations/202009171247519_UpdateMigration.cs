namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Movies", "ReleasedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Movies", "NuumberInStock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "NuumberInStock", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "ReleasedDate", c => c.DateTime());
            AlterColumn("dbo.Movies", "DateAdded", c => c.DateTime());
            DropColumn("dbo.Movies", "NumberInStock");
        }
    }
}
