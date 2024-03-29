﻿// <auto-generated />
using System;
using JurassicParkDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JurassicParkDB.Migrations
{
    [DbContext(typeof(DinosaurContext))]
    partial class DinosaurContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("JurassicParkDB.Dinosaur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateAcquired")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Diet")
                        .HasColumnType("text");

                    b.Property<int>("EnclosureNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Species")
                        .HasColumnType("text");

                    b.Property<decimal>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Dinosaurs");
                });
#pragma warning restore 612, 618
        }
    }
}
