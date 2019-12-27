using Microsoft.EntityFrameworkCore.Migrations;

namespace EKS_database_web.Data.Songs.Migrations
{
    public partial class verse_sequence_number : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "seqenceNumber",
                table: "verses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "seqenceNumber",
                table: "verses");
        }
    }
}
