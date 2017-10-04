using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace carzz.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Toyota')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Honda')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Ford')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Nissan')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Hyundai')");

            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Camry',(SELECT Id FROM Makes WHERE Name='Toyota'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Corolla',(SELECT Id FROM Makes WHERE Name='Toyota'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Rav4',(SELECT Id FROM Makes WHERE Name='Toyota'))");

            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Civic',(SELECT Id FROM Makes WHERE Name='Honda'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('CR-V',(SELECT Id FROM Makes WHERE Name='Honda'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Accord',(SELECT Id FROM Makes WHERE Name='Honda'))");

            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Fiesta',(SELECT Id FROM Makes WHERE Name='Ford'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Focus',(SELECT Id FROM Makes WHERE Name='Ford'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Fusion',(SELECT Id FROM Makes WHERE Name='Ford'))");

            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Altima',(SELECT Id FROM Makes WHERE Name='Nissan'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Sentra',(SELECT Id FROM Makes WHERE Name='Nissan'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Maxima',(SELECT Id FROM Makes WHERE Name='Nissan'))");

            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Tucson',(SELECT Id FROM Makes WHERE Name='Hyundai'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Elantra',(SELECT Id FROM Makes WHERE Name='Hyundai'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Sonata',(SELECT Id FROM Makes WHERE Name='Hyundai'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes WHERE Name IN ('Toyota','Honda','Ford','Nissan','Hyundai')");
        }
    }
}
