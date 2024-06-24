﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleRental.Core.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class AddBirthDateToDeliverers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "birth_date",
                table: "deliverers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birth_date",
                table: "deliverers");
        }
    }
}
