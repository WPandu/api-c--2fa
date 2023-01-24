using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API2FA.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "VarChar(20)", nullable: false),
                    email = table.Column<string>(type: "VarChar(255)", nullable: false),
                    password = table.Column<string>(type: "VarChar(255)", nullable: false),
                    name = table.Column<string>(type: "VarChar(255)", nullable: false),
                    google2fasecret = table.Column<string>(name: "google_2fa_secret", type: "VarChar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
