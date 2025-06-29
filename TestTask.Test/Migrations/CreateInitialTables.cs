namespace TestTask.Test.Migrations
{
    using FluentMigrator;

    namespace TestTask.Migrations.Migrations
    {
        [Migration(2025062901)]
        public class CreateInitialTables : Migration
        {
            public override void Up()
            {
                Create.Table("Employees")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("FullName").AsString().NotNullable()
                    .WithColumn("Position").AsInt32().NotNullable()
                    .WithColumn("BirthDate").AsDate().NotNullable();

                Create.Table("Contractors")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Name").AsString().NotNullable()
                    .WithColumn("Inn").AsInt32().NotNullable()
                    .WithColumn("CuratorId").AsInt32().NotNullable().ForeignKey("Employees", "Id");

                Create.Table("Orders")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Date").AsDateTime().NotNullable()
                    .WithColumn("Amount").AsDecimal().NotNullable()
                    .WithColumn("EmployeeId").AsInt32().NotNullable().ForeignKey("Employees", "Id")
                    .WithColumn("ContractorId").AsInt32().NotNullable().ForeignKey("Contractors", "Id");
            }

            public override void Down()
            {
                Delete.Table("Orders");
                Delete.Table("Contractors");
                Delete.Table("Employees");
            }
        }
    }
}
