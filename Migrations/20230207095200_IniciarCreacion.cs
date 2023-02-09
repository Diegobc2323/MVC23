using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC23.Migrations
{
    /// <inheritdoc />
    public partial class IniciarCreacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomMarca = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Serie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarcaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serie", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Serie_Marcas_MarcaID",
                        column: x => x.MarcaID,
                        principalTable: "Marcas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerieID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vehiculo_Serie_SerieID",
                        column: x => x.SerieID,
                        principalTable: "Serie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoExtra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.ID);
                });

            migrationBuilder.CreateTable(
               name: "VehiculoExtras",
               columns: table => new
               {
                   ID = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   VehiculoID = table.Column<int>(type: "int", nullable: false),
                   ExtraID = table.Column<int>(type: "int", nullable: false)

               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_VehiculoExtra", x => x.ID);
                   table.ForeignKey(
                       name: "FK_VehiculoExtra_Vehiculo_VehiculoID",
                       column: x => x.VehiculoID,
                       principalTable: "Vehiculo",
                       principalColumn: "ID",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                      name: "FK_VehiculoExtra_Extra_ExtraID",
                      column: x => x.ExtraID,
                      principalTable: "Extras",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_Serie_MarcaID",
                table: "Serie",
                column: "MarcaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_SerieID",
                table: "Vehiculo",
                column: "SerieID");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoID",
                table: "VehiculoExtras",
                column: "VehiculoID");

            migrationBuilder.CreateIndex(
               name: "IX_ExtraID",
               table: "VehiculoExtras",
               column: "ExtraID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "Serie");

            migrationBuilder.DropTable(
                name: "Marcas");
        }
    }
}
