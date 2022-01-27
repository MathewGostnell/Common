using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.Data.Migrations
{
    public partial class v001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Node",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeName = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    DisplayName = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Node", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NodeType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    RegularExpression = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateDefinitionEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    NodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateDefinitionEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateDefinitionEntity_Node_NodeId",
                        column: x => x.NodeId,
                        principalSchema: "dbo",
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateDefinitionEntity_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalSchema: "dbo",
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Node_CodeName",
                schema: "dbo",
                table: "Node",
                column: "CodeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NodeType_Name",
                schema: "dbo",
                table: "NodeType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NodeType_RegularExpression",
                schema: "dbo",
                table: "NodeType",
                column: "RegularExpression",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Template_Name",
                schema: "dbo",
                table: "Template",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemplateDefinitionEntity_NodeId",
                table: "TemplateDefinitionEntity",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateDefinitionEntity_TemplateId",
                table: "TemplateDefinitionEntity",
                column: "TemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NodeType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TemplateDefinitionEntity");

            migrationBuilder.DropTable(
                name: "Node",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Template",
                schema: "dbo");
        }
    }
}
