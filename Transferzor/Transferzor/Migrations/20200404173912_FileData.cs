using Microsoft.EntityFrameworkCore.Migrations;

namespace Transferzor.Migrations
{
    public partial class FileData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileSendData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderEmail = table.Column<string>(nullable: true),
                    ReceiverEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSendData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileStorageData",
                columns: table => new
                {
                    FileSendDateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileSendDataId = table.Column<int>(nullable: true),
                    FileUri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStorageData", x => x.FileSendDateId);
                    table.ForeignKey(
                        name: "FK_FileStorageData_FileSendData_FileSendDataId",
                        column: x => x.FileSendDataId,
                        principalTable: "FileSendData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileStorageData_FileSendDataId",
                table: "FileStorageData",
                column: "FileSendDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileStorageData");

            migrationBuilder.DropTable(
                name: "FileSendData");
        }
    }
}
