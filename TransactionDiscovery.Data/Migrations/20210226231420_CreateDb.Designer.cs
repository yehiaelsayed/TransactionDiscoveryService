﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransactionDiscovery.Data.Context;

namespace TransactionDiscovery.Data.Migrations
{
    [DbContext(typeof(TransactionDiscoveryDbContext))]
    [Migration("20210226231420_CreateDb")]
    partial class CreateDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TransactionDiscovery.Data.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PublicKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecoredCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RecoredUpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("TransactionDiscovery.Data.Models.DiscoveryPatch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cursor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PulledRecordsCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("RecoredCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RecoredUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("DiscoveryPatch");
                });

            modelBuilder.Entity("TransactionDiscovery.Data.Models.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FromAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PagingToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecoredCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RecoredUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SourceAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ToAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TransactionSuccessful")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("TransactionDiscovery.Data.Models.DiscoveryPatch", b =>
                {
                    b.HasOne("TransactionDiscovery.Data.Models.Account", "Account")
                        .WithMany("DiscoveryPatchs")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("TransactionDiscovery.Data.Models.Account", b =>
                {
                    b.Navigation("DiscoveryPatchs");
                });
#pragma warning restore 612, 618
        }
    }
}