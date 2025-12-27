using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialVehicleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Make = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Model = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Vin = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SerialNumber = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: true),
                    UserOwnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    TeamOwnerId = table.Column<int>(type: "integer", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Teams_TeamOwnerId",
                        column: x => x.TeamOwnerId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehicles_UserProfiles_UserOwnerId",
                        column: x => x.UserOwnerId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TeamOwnerId",
                table: "Vehicles",
                column: "TeamOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserOwnerId",
                table: "Vehicles",
                column: "UserOwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
