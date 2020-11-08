namespace WEBAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sf2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 10, fixedLength: true, unicode: false),
                        Name = c.String(maxLength: 30),
                        Age = c.Int(nullable: false),
                        Addrees = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
