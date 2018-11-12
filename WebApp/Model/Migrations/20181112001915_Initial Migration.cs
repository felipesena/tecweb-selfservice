using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Model.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(nullable: false),
                    Perfil = table.Column<string>(nullable: false),
                    Provedor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Comidas",
                columns: table => new
                {
                    ComidaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    QuantidadeBase = table.Column<int>(nullable: false),
                    Unidade = table.Column<string>(nullable: true),
                    QuantidadeCalorias = table.Column<double>(nullable: true),
                    QuantidadeFibra = table.Column<double>(nullable: true),
                    UnidadeFibra = table.Column<string>(nullable: true),
                    QuantidadeFerro = table.Column<double>(nullable: true),
                    UnidadeFerro = table.Column<string>(nullable: true),
                    QuantidadePotassio = table.Column<double>(nullable: true),
                    UnidadePotassio = table.Column<string>(nullable: true),
                    QuantidadeZinco = table.Column<double>(nullable: true),
                    UnidadeZinco = table.Column<string>(nullable: true),
                    QuantidadeCarboidrato = table.Column<double>(nullable: true),
                    UnidadeCarboidrato = table.Column<string>(nullable: true),
                    QuantidadeFosforo = table.Column<double>(nullable: true),
                    UnidadeFosforo = table.Column<string>(nullable: true),
                    QuantidadeSodio = table.Column<double>(nullable: true),
                    UnidadeSodio = table.Column<string>(nullable: true),
                    QuantidadeProteina = table.Column<double>(nullable: true),
                    UnidadeProteina = table.Column<string>(nullable: true),
                    QuantidadeLipidio = table.Column<double>(nullable: true),
                    UnidadeLipidio = table.Column<string>(nullable: true),
                    CategoriaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comidas", x => x.ComidaId);
                    table.ForeignKey(
                        name: "FK_Comidas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comidas_CategoriaId",
                table: "Comidas",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comidas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
