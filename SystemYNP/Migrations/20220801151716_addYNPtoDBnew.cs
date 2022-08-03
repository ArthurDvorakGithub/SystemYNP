using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemYNP.Migrations
{
    public partial class addYNPtoDBnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "YNP",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "YNP");
        }
    }
}
