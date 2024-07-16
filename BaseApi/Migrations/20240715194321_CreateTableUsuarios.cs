using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGP.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio"),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio"),
                    Login = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio"),
                    Senha = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio"),
                    UsuariosDataFim = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio"),
                    UsuariosDataInicio = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio"),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio"),
                    UsuarioCriacao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio"),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio"),
                    UsuarioAlteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio"),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "UsuariosAuditoria")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "UsuariosDataFim")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "UsuariosDataInicio");
        }
    }
}
