using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aw_s4t1.Migrations
{
    /// <inheritdoc />
    public partial class NuevosAtributos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormaPago",
                table: "Ventas",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnidadMedida",
                table: "Productos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Descuento",
                table: "DetallesVenta",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormaPago",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "UnidadMedida",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Categorias");
        }
    }
}
