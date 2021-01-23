namespace CodeFirstExisitngDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            Sql("INSERT INTO Categories VALUES (1, 'Ola')");
            Sql("INSERT INTO Categories VALUES (2, 'Bimpe')");
            Sql("INSERT INTO Categories VALUES (3, 'Ayo')");


        }
        
        public override void Down()
        {
            DropTable("dbo.Categories");
        }
    }
}
