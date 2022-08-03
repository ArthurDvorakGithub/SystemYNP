using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemYNP.Migrations
{
    public partial class AddNewFunc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ExistInExternalApi",
                table: "YNP",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExistInLocalDb",
                table: "YNP",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExistInExternalApi",
                table: "YNP");

            migrationBuilder.DropColumn(
                name: "IsExistInLocalDb",
                table: "YNP");
        }
    }
}
