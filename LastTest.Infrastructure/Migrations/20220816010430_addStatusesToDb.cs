using Microsoft.EntityFrameworkCore.Migrations;

namespace LastTest.Infrastructure.Migrations
{
    public partial class addStatusesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Statuses] (Description) VALUES ('SinReservar')");
            migrationBuilder.Sql("INSERT INTO [Statuses] (Description) VALUES ('Solicitado')");
            migrationBuilder.Sql("INSERT INTO [Statuses] (Description) VALUES ('Reservado')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
