using Microsoft.EntityFrameworkCore.Migrations;

namespace EcoPart.Web.UI.Migrations
{
    public partial class Blogdone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Body",
                table: "BlogPosts",
                newName: "Paragraph");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Paragraph",
                table: "BlogPosts",
                newName: "Body");
        }
    }
}
