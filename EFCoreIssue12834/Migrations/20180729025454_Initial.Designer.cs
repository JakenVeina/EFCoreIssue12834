﻿// <auto-generated />
using System;
using EFCoreIssue12834;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EFCoreIssue12834.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20180729025454_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("EFCoreIssue12834.MyPrimaryEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("SecondaryEntityId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SecondaryEntityId");

                    b.ToTable("PrimaryEntities");
                });

            modelBuilder.Entity("EFCoreIssue12834.MySecondaryEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("PrimaryEntityId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PrimaryEntityId");

                    b.ToTable("SecondaryEntities");
                });

            modelBuilder.Entity("EFCoreIssue12834.MyPrimaryEntity", b =>
                {
                    b.HasOne("EFCoreIssue12834.MySecondaryEntity", "SecondaryEntity")
                        .WithMany()
                        .HasForeignKey("SecondaryEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EFCoreIssue12834.MySecondaryEntity", b =>
                {
                    b.HasOne("EFCoreIssue12834.MyPrimaryEntity", "PrimaryEntity")
                        .WithMany()
                        .HasForeignKey("PrimaryEntityId");
                });
#pragma warning restore 612, 618
        }
    }
}