using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerServiceTemplate.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Weathers
            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                });

            //Send Email Notif
            migrationBuilder.CreateTable(
                name: "Ntf",
                columns: table => new
                {
                 Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 SMTP_Host = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                 SMTP_Port = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                 SMTP_Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                 SMTP_Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                 Sender_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notif", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //Weathers
            migrationBuilder.DropTable(
                name: "Weathers");

            //Send Email Notif
            migrationBuilder.DropTable(
                name: "Ntf");
        }
    }
}
