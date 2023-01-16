﻿// <auto-generated />
using System;
using Inventory.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Inventory.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20210823095249_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Inventory.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on_utc");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid>("EntityGuid")
                        .HasColumnType("uuid")
                        .HasColumnName("entity_guid");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("Inventory.Domain.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("AmountInStock")
                        .HasColumnType("numeric")
                        .HasColumnName("amount_in_stock");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on_utc");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid>("EntityGuid")
                        .HasColumnType("uuid")
                        .HasColumnName("entity_guid");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("selling_price");

                    b.Property<string>("Status")
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("unit_price");

                    b.HasKey("Id")
                        .HasName("pk_items");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_items_category_id");

                    b.ToTable("items");
                });

            modelBuilder.Entity("Inventory.Domain.Item", b =>
                {
                    b.HasOne("Inventory.Domain.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("fk_items_categories_category_id");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Inventory.Domain.Category", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
