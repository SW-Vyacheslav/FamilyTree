using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyTree.Infrastructure.Migrations
{
    public partial class AddedVideoThumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PreviewImageData",
                table: "Videos",
                type: "image",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<string>(
                name: "PreviewImageFormat",
                table: "Videos",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewImageData",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "PreviewImageFormat",
                table: "Videos");
        }
    }
}
