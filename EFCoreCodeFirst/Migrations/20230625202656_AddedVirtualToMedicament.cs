using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class AddedVirtualToMedicament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 6, 25, 22, 26, 56, 106, DateTimeKind.Local).AddTicks(4241), new DateTime(2023, 7, 2, 22, 26, 56, 106, DateTimeKind.Local).AddTicks(4280) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 6, 25, 22, 26, 56, 106, DateTimeKind.Local).AddTicks(4284), new DateTime(2023, 7, 9, 22, 26, 56, 106, DateTimeKind.Local).AddTicks(4285) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 6, 25, 21, 43, 4, 257, DateTimeKind.Local).AddTicks(7422), new DateTime(2023, 7, 2, 21, 43, 4, 257, DateTimeKind.Local).AddTicks(7464) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 6, 25, 21, 43, 4, 257, DateTimeKind.Local).AddTicks(7469), new DateTime(2023, 7, 9, 21, 43, 4, 257, DateTimeKind.Local).AddTicks(7470) });
        }
    }
}
