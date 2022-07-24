using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace InfoTrack.SEOTracker.Data.Migrations
{
   public partial class init : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.CreateTable(
             name: "Trackers",
             columns: table => new
             {
                Id = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                Search = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                EngineType = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Trackers", x => x.Id);
             });

         migrationBuilder.CreateTable(
             name: "TrackerHistory",
             columns: table => new
             {
                Id = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                Ranks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                TrackerId = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_TrackerHistory", x => x.Id);
                table.ForeignKey(
                       name: "FK_TrackerHistory_Trackers_TrackerId",
                       column: x => x.TrackerId,
                       principalTable: "Trackers",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateIndex(
             name: "IX_TrackerHistory_TrackerId",
             table: "TrackerHistory",
             column: "TrackerId");
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "TrackerHistory");

         migrationBuilder.DropTable(
             name: "Trackers");
      }
   }
}
