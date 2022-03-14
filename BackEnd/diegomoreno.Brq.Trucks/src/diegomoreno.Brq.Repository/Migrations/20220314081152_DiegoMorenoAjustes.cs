using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace diegomoreno.Brq.Repository.Migrations
{
    public partial class DiegoMorenoAjustes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSeries = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FabricationYear = table.Column<int>(type: "int", nullable: false),
                    SerieYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trucks_Series_IdSeries",
                        column: x => x.IdSeries,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("2a0a4549-c1c2-472a-8639-1365446ac22f"), "FM" });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("6cb23935-2cdc-488c-96bf-61fb8f90ea7d"), "FH" });

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_IdSeries",
                table: "Trucks",
                column: "IdSeries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Series");
        }
    }
}
