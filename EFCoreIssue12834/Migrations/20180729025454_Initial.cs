using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EFCoreIssue12834.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SecondaryEntities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Type = table.Column<string>(nullable: false),
                    PrimaryEntityId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryEntities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Type = table.Column<string>(nullable: false),
                    SecondaryEntityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimaryEntities_SecondaryEntities_SecondaryEntityId",
                        column: x => x.SecondaryEntityId,
                        principalTable: "SecondaryEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryEntities_SecondaryEntityId",
                table: "PrimaryEntities",
                column: "SecondaryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryEntities_PrimaryEntityId",
                table: "SecondaryEntities",
                column: "PrimaryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecondaryEntities_PrimaryEntities_PrimaryEntityId",
                table: "SecondaryEntities",
                column: "PrimaryEntityId",
                principalTable: "PrimaryEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrimaryEntities_SecondaryEntities_SecondaryEntityId",
                table: "PrimaryEntities");

            migrationBuilder.DropTable(
                name: "SecondaryEntities");

            migrationBuilder.DropTable(
                name: "PrimaryEntities");
        }
    }
}
