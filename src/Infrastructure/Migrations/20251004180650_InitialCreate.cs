using Infrastructure.Database;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: Schemas.Default);

        migrationBuilder.CreateTable(
            name: "Users",
            schema: Schemas.Default,
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Users", x => x.Id));

        migrationBuilder.InsertData(
            schema: Schemas.Default,
            table: "Users",
            columns: ["Id", "Email", "FirstName", "LastName", "PasswordHash"],
            values: new object[,]
            {
                { new Guid("bb831866-fd4e-4307-ad1b-af07e3286d55"), "admin@example.com", "Admin", "User", "AQAAAAEAACcQAAAAE..." },
                { new Guid("d8381c6c-7360-4a5e-b52a-deec75d6a789"), "user@example.com", "John", "Doe", "AQAAAAEAACcQAAAAE..." }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            schema: Schemas.Default,
            table: "Users",
            column: "Email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Users",
            schema: Schemas.Default);
    }
}
