using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.MsSql.Migrations
{
    /// <inheritdoc />
    public partial class CreateStoredProcedureGetNextDocumentNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[SP_GetNextDocumentNumber]
                    @DocumentTypeId UNIQUEIDENTIFIER
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    SELECT 
                        [o].[Id],
                        [o].[DocumentTypeId],
                        [o].[Code],
                        [o].[Prefix],
                        [o].[CurrentNumber],
                        [o].[NextNumber]
                    FROM [ODCN] AS [o] WITH (XLOCK, ROWLOCK)
                    WHERE [o].[DocumentTypeId] = @DocumentTypeId;
                    
                    RETURN 0;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[SP_GetNextDocumentNumber]");
        }
    }
}
