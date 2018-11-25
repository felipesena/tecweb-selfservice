using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Model.Migrations
{
    public partial class TableHistoricoPratoAlimentoPrato3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.CreateTable(
                name: "Pratos",
                columns: table => new
                {
                    PratoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomePrato = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pratos", x => x.PratoId);
                    table.ForeignKey(
                        name: "FK_Pratos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pratos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlimentoPratos",
                columns: table => new
                {
                    AlimentoPratoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PratoId = table.Column<int>(nullable: false),
                    ComidaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlimentoPratos", x => x.AlimentoPratoId);
                    table.ForeignKey(
                        name: "FK_AlimentoPratos_Comidas_ComidaId",
                        column: x => x.ComidaId,
                        principalTable: "Comidas",
                        principalColumn: "ComidaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlimentoPratos_Pratos_PratoId",
                        column: x => x.PratoId,
                        principalTable: "Pratos",
                        principalColumn: "PratoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoConsumos",
                columns: table => new
                {
                    HistoricoConsumoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(nullable: false),
                    PratoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoConsumos", x => x.HistoricoConsumoId);
                    table.ForeignKey(
                        name: "FK_HistoricoConsumos_Pratos_PratoId",
                        column: x => x.PratoId,
                        principalTable: "Pratos",
                        principalColumn: "PratoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricoConsumos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlimentoPratos_ComidaId",
                table: "AlimentoPratos",
                column: "ComidaId");

            migrationBuilder.CreateIndex(
                name: "IX_AlimentoPratos_PratoId",
                table: "AlimentoPratos",
                column: "PratoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoConsumos_PratoId",
                table: "HistoricoConsumos",
                column: "PratoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoConsumos_UsuarioId",
                table: "HistoricoConsumos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pratos_CategoriaId",
                table: "Pratos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pratos_UsuarioId",
                table: "Pratos",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlimentoPratos");

            migrationBuilder.DropTable(
                name: "HistoricoConsumos");

            migrationBuilder.DropTable(
                name: "Comidas");

            migrationBuilder.DropTable(
                name: "Pratos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
