using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace diegomoreno.Brq.Repository.Migrations
{
    public partial class DiegoMoreno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeriesEnum = table.Column<string>(type: "varchar(3)", nullable: false),
                    FabricationYear = table.Column<int>(type: "int", nullable: false),
                    SerieYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trucks");
        }
    }
}
