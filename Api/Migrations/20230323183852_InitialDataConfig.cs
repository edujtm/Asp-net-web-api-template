using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Asp_net_web_api_template.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "Color",
                table: "Vehicles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Vehicles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Customers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBirth",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "DateBirth", "Details", "Email", "Gender", "Name" },
                values: new object[,]
                {
                    { new Guid("20c11b5c-243c-4e41-acff-a2f1072aee2f"), "Av. Nevaldo Rocha, 3775 - Tirol, Natal - RN, 59015-450", new DateTime(2000, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI", "cricrana@email.com", 1, "Cricrana" },
                    { new Guid("2c52045b-b323-424e-bf04-c27dcc2e0a33"), "Campus Universitário - Lagoa Nova, Natal - RN, 59078-970", new DateTime(1990, 12, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI", "fulano@email.com", 0, "Fulano" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleId", "Category", "Color", "DailyRentalFee", "Manufacturer", "Mileage", "Model" },
                values: new object[,]
                {
                    { new Guid("3462f9e4-2bda-4d44-af37-5521a76f1419"), 3, 3, 256.0, "Fiat", 35000, "Fiat Doblo" },
                    { new Guid("acf49e7d-4d0c-4ea0-b3cc-9aee88b88a7a"), 2, 4, 118.34999999999999, "Fiat", 60000, "Fiat Siena" },
                    { new Guid("afb387cd-9ca2-4fe6-aff2-a2163c92cb59"), 2, 0, 156.0, "Hyundai", 17500, "HB20" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "CustomerId", "From", "PaymentReceived", "Status", "To", "VehicleId" },
                values: new object[,]
                {
                    { new Guid("9987b490-5b67-41b8-a9d6-9cc45b50481d"), new Guid("2c52045b-b323-424e-bf04-c27dcc2e0a33"), new DateTime(2019, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), true, 2, new DateTime(2019, 6, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new Guid("acf49e7d-4d0c-4ea0-b3cc-9aee88b88a7a") },
                    { new Guid("bd8512a0-dfc9-4572-b9b6-7e73db3b8c23"), new Guid("20c11b5c-243c-4e41-acff-a2f1072aee2f"), new DateTime(2023, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(1, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3462f9e4-2bda-4d44-af37-5521a76f1419") },
                    { new Guid("c3e363e2-9550-4d5e-8b33-de3a23f69884"), new Guid("2c52045b-b323-424e-bf04-c27dcc2e0a33"), new DateTime(2022, 12, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), true, 2, new DateTime(2022, 12, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("afb387cd-9ca2-4fe6-aff2-a2163c92cb59") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("9987b490-5b67-41b8-a9d6-9cc45b50481d"));

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("bd8512a0-dfc9-4572-b9b6-7e73db3b8c23"));

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("c3e363e2-9550-4d5e-8b33-de3a23f69884"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("20c11b5c-243c-4e41-acff-a2f1072aee2f"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("2c52045b-b323-424e-bf04-c27dcc2e0a33"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "VehicleId",
                keyValue: new Guid("3462f9e4-2bda-4d44-af37-5521a76f1419"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "VehicleId",
                keyValue: new Guid("acf49e7d-4d0c-4ea0-b3cc-9aee88b88a7a"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "VehicleId",
                keyValue: new Guid("afb387cd-9ca2-4fe6-aff2-a2163c92cb59"));

            migrationBuilder.DropColumn(
                name: "DateBirth",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
