using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesMovies.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Delete existing data
            migrationBuilder.Sql("DELETE FROM Movie");

            // Change column to float
            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "Movie",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "Movie",
                nullable: true,
                oldClrType: typeof(float));
        }
    }
}
