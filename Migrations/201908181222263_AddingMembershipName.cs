namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMembershipName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "MembershipName", c => c.String(nullable: false, maxLength: 255));
            Sql("Update dbo.MembershipTypes set MembershipName = 'Pay as you Go' where Id = 1");
            Sql("Update dbo.MembershipTypes set MembershipName = 'Weekly' where Id = 2");
            Sql("Update dbo.MembershipTypes set MembershipName = 'Monthly' where Id = 3");
            Sql("Update dbo.MembershipTypes set MembershipName = 'Yearly' where Id = 4");

        }

        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "MembershipName");
        }
    }
}
