using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymScheduling.Migrations
{
    /// <inheritdoc />
    public partial class _002_Add_createAt_Student : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedulings_ClassSessions_ClassSessionId",
                table: "Schedulings");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedulings_Students_StudentId",
                table: "Schedulings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedulings",
                table: "Schedulings");

            migrationBuilder.RenameTable(
                name: "Schedulings",
                newName: "Schedullings");

            migrationBuilder.RenameIndex(
                name: "IX_Schedulings_StudentId_ClassSessionId",
                table: "Schedullings",
                newName: "IX_Schedullings_StudentId_ClassSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedulings_ClassSessionId",
                table: "Schedullings",
                newName: "IX_Schedullings_ClassSessionId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedullings",
                table: "Schedullings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedullings_ClassSessions_ClassSessionId",
                table: "Schedullings",
                column: "ClassSessionId",
                principalTable: "ClassSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedullings_Students_StudentId",
                table: "Schedullings",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedullings_ClassSessions_ClassSessionId",
                table: "Schedullings");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedullings_Students_StudentId",
                table: "Schedullings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedullings",
                table: "Schedullings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Schedullings",
                newName: "Schedulings");

            migrationBuilder.RenameIndex(
                name: "IX_Schedullings_StudentId_ClassSessionId",
                table: "Schedulings",
                newName: "IX_Schedulings_StudentId_ClassSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedullings_ClassSessionId",
                table: "Schedulings",
                newName: "IX_Schedulings_ClassSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedulings",
                table: "Schedulings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedulings_ClassSessions_ClassSessionId",
                table: "Schedulings",
                column: "ClassSessionId",
                principalTable: "ClassSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedulings_Students_StudentId",
                table: "Schedulings",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
