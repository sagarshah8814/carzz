using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace carzz.Migrations
{
    public partial class VehicleAddProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vehicles");
        }
    }
}
