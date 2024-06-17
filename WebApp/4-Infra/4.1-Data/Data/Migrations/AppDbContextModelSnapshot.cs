﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Documento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Observacao")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("ReasonInactivation")
                        .HasColumnType("longtext");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Documento");
                });

            modelBuilder.Entity("Domain.DocumentoAnexo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<byte[]>("Arquivo")
                        .HasColumnType("longblob");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<long>("DocumentoId")
                        .HasColumnType("bigint");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DocumentoId");

                    b.ToTable("DocumentoAnexo");
                });

            modelBuilder.Entity("Domain.DocumentoAnexo", b =>
                {
                    b.HasOne("Domain.Documento", "Documento")
                        .WithMany("Anexos")
                        .HasForeignKey("DocumentoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Documento");
                });

            modelBuilder.Entity("Domain.Documento", b =>
                {
                    b.Navigation("Anexos");
                });
#pragma warning restore 612, 618
        }
    }
}
