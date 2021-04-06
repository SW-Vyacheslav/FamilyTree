using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyTree.Infrastructure.Migrations
{
    public partial class AddedDataBlockImagesAndVideos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataBlockImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<int>(nullable: false),
                    DataBlockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataBlockImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataBlockImages_DataBlocks_DataBlockId",
                        column: x => x.DataBlockId,
                        principalTable: "DataBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataBlockImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataBlockVideos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoId = table.Column<int>(nullable: false),
                    DataBlockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataBlockVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataBlockVideos_DataBlocks_DataBlockId",
                        column: x => x.DataBlockId,
                        principalTable: "DataBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataBlockVideos_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataBlockImages_DataBlockId",
                table: "DataBlockImages",
                column: "DataBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_DataBlockImages_ImageId",
                table: "DataBlockImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_DataBlockVideos_DataBlockId",
                table: "DataBlockVideos",
                column: "DataBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_DataBlockVideos_VideoId",
                table: "DataBlockVideos",
                column: "VideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataBlockImages");

            migrationBuilder.DropTable(
                name: "DataBlockVideos");
        }
    }
}
