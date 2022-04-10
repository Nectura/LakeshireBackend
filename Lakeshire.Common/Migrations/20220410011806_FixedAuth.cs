using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lakeshire.Common.Migrations
{
    public partial class FixedAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccountServiceAuths",
                table: "UserAccountServiceAuths");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "UserAccountServiceAuths",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserAccountServiceAuths",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccountServiceAuths",
                table: "UserAccountServiceAuths",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountServiceAuths_UserId",
                table: "UserAccountServiceAuths",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccountServiceAuths",
                table: "UserAccountServiceAuths");

            migrationBuilder.DropIndex(
                name: "IX_UserAccountServiceAuths_UserId",
                table: "UserAccountServiceAuths");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserAccountServiceAuths");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "UserAccountServiceAuths",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccountServiceAuths",
                table: "UserAccountServiceAuths",
                columns: new[] { "UserId", "RefreshToken" });
        }
    }
}
