using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDoseToOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Dose",
                table: "PrescriptionMedicaments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "PrescriptionMedicaments",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 2, 1 },
                column: "Dose",
                value: null);

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 6, 26, 0, 21, 41, 297, DateTimeKind.Local).AddTicks(5210), new DateTime(2023, 7, 3, 0, 21, 41, 297, DateTimeKind.Local).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 6, 26, 0, 21, 41, 297, DateTimeKind.Local).AddTicks(5254), new DateTime(2023, 7, 10, 0, 21, 41, 297, DateTimeKind.Local).AddTicks(5255) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Dose",
                table: "PrescriptionMedicaments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "PrescriptionMedicaments",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 2, 1 },
                column: "Dose",
                value: 0);

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
    }
}
