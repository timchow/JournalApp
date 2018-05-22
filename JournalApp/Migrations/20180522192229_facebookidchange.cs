using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JournalApp.Migrations
{
    public partial class facebookidchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "SocialId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<long>(
                name: "FacebookId",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
