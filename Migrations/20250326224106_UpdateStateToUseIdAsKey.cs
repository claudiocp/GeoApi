using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStateToUseIdAsKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_States_StatePostalCode",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_States",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Cities_StatePostalCode_Name",
                table: "Cities");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "States",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "StatePostalCode",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)");

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_States",
                table: "States",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId_Name",
                table: "Cities",
                columns: new[] { "StateId", "Name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_States_StateId",
                table: "Cities",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_States_StateId",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_States",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Cities_StateId_Name",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "States");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Cities");

            migrationBuilder.AlterColumn<string>(
                name: "StatePostalCode",
                table: "Cities",
                type: "nvarchar(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_States",
                table: "States",
                column: "StatePostalCode");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StatePostalCode_Name",
                table: "Cities",
                columns: new[] { "StatePostalCode", "Name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_States_StatePostalCode",
                table: "Cities",
                column: "StatePostalCode",
                principalTable: "States",
                principalColumn: "StatePostalCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
