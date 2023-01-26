using Microsoft.EntityFrameworkCore.Migrations;

namespace Vaultex.DataAccess.Migrations
{
    public partial class CreateDatabaseAndTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    PK_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganisationNumber = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.PK_Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    OrganisationNumber = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    OrganisationName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    AddressLine4 = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Town = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.OrganisationNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Organisations");
        }
    }
}
