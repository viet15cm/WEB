namespace WEBAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slfsf3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        IDDE = c.String(nullable: false, maxLength: 5, fixedLength: true, unicode: false),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.IDDE);
            
            AddColumn("dbo.Employees", "IDDE", c => c.String(maxLength: 5, fixedLength: true, unicode: false));
            CreateIndex("dbo.Employees", "IDDE");
            AddForeignKey("dbo.Employees", "IDDE", "dbo.Departments", "IDDE");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "IDDE", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "IDDE" });
            DropColumn("dbo.Employees", "IDDE");
            DropTable("dbo.Departments");
        }
    }
}
