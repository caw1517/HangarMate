using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class CompanyToTeamRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Companies_CompanyId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_CompanyId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "CompanyRole",
                table: "UserProfiles",
                newName: "TeamRole");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Companies",
                newName: "TeamName");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_TeamId",
                table: "UserProfiles",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Companies_TeamId",
                table: "UserProfiles",
                column: "TeamId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Companies_TeamId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_TeamId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "TeamRole",
                table: "UserProfiles",
                newName: "CompanyRole");

            migrationBuilder.RenameColumn(
                name: "TeamName",
                table: "Companies",
                newName: "CompanyName");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "UserProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CompanyId",
                table: "UserProfiles",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Companies_CompanyId",
                table: "UserProfiles",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
