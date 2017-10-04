using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace carzz.Migrations
{
    public partial class SeedFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('AirBags')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('ABS')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Sun Roof')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Leather Seat')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Backup Camera')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Traction Control')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Cruise Control')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Display')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Features WHERE Name IN ('AirBags','ABS','Sun Roof','Leather Seat','Backup Camera','Traction Control','Cruise Control','Display')");
        }
    }
}
