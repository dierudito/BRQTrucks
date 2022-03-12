﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using diegomoreno.Brq.Repository.Contexts.Entity;

#nullable disable

namespace diegomoreno.Brq.Repository.Migrations
{
    [DbContext(typeof(TrucksDbContext))]
    [Migration("20220312200619_DiegoMoreno")]
    partial class DiegoMoreno
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.1.22076.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("diegomoreno.Brq.domain.Entities.Truck", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FabricationYear")
                        .HasColumnType("int");

                    b.Property<int>("SerieYear")
                        .HasColumnType("int");

                    b.Property<string>("SeriesEnum")
                        .IsRequired()
                        .HasColumnType("varchar(3)");

                    b.HasKey("Id");

                    b.ToTable("Trucks", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
