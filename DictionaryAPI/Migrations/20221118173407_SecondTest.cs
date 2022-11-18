using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionaryAPI.Migrations
{
    /// <inheritdoc />
    public partial class SecondTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Definitions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<long>(type: "bigint", nullable: false),
                    WordDefinition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefinitionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExampleSentence = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Definitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Definitions_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Definitions_WordId",
                table: "Definitions",
                column: "WordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Definitions");
        }
    }
}
