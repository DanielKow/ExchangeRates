﻿// <auto-generated />
using ExchangeRatesSource.InfrastructureLayer.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Migrator.Migrations
{
    [DbContext(typeof(ExchangeRateContext))]
    [Migration("20220430211700_RemoveTypeFromExchangeRate")]
    partial class RemoveTypeFromExchangeRate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ExchangeRatesSource.DomainLayer.ExchangeRate", b =>
                {
                    b.Property<string>("Currency")
                        .HasColumnType("text");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Currency");

                    b.ToTable("ExchangeRate");
                });
#pragma warning restore 612, 618
        }
    }
}
