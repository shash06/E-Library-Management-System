namespace ELMS_WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDefaultColumnNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Name", c => c.String());
            AddColumn("dbo.User", "Address", c => c.String());
            AddColumn("dbo.User", "Interest", c => c.String());
            AddColumn("dbo.User", "Bank_Name", c => c.String());
            AddColumn("dbo.User", "Credit_Card", c => c.Long());
            AddColumn("dbo.User", "Subscription", c => c.Boolean());
            AddColumn("dbo.User", "Admin", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Admin");
            DropColumn("dbo.User", "Subscription");
            DropColumn("dbo.User", "Credit_Card");
            DropColumn("dbo.User", "Bank_Name");
            DropColumn("dbo.User", "Interest");
            DropColumn("dbo.User", "Address");
            DropColumn("dbo.User", "Name");
        }
    }
}
