using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Andon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    warnCount = table.Column<int>(type: "int", nullable: false),
                    alarmCount = table.Column<int>(type: "int", nullable: false),
                    entityId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Andon", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "NodeList",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hierarchyDefinitionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hierarchyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parentEntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeList", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NodeList_entityId",
                table: "NodeList",
                column: "entityId",
                unique: true,
                filter: "[entityId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Andon");

            migrationBuilder.DropTable(
                name: "NodeList");
        }
    }
}
