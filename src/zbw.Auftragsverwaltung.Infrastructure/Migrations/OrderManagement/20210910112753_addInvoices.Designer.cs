﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using zbw.Auftragsverwaltung.Infrastructure;

namespace zbw.Auftragsverwaltung.Infrastructure.Migrations.OrderManagement
{
    [DbContext(typeof(OrderManagementContext))]
    [Migration("20210910112753_addInvoices")]
    partial class addInvoices
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.Addresses.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.ArticleGroups.Entities.ArticleGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ArticlegroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArticlegroupId");

                    b.ToTable("ArticleGroups");
                });

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.Articles.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ArticleGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ArticleId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticleGroupId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.Customers.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CustomerNr")
                        .HasColumnType("int");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.Invoices.Entities.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Brutto")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Netto")
                        .HasColumnType("float");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<double>("Tax")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.Orders.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderNr")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.Positions.Entities.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid?>("ArticleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Nr")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.Addresses.Entities.Address", b =>
                {
                    b.HasOne("zbw.Auftragsverwaltung.Core.Customers.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.ArticleGroups.Entities.ArticleGroup", b =>
                {
                    b.HasOne("zbw.Auftragsverwaltung.Core.ArticleGroups.Entities.ArticleGroup", "Articlegroup")
                        .WithMany()
                        .HasForeignKey("ArticlegroupId");

                    b.Navigation("Articlegroup");
                });

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.Articles.Entities.Article", b =>
                {
                    b.HasOne("zbw.Auftragsverwaltung.Core.ArticleGroups.Entities.ArticleGroup", "ArticleGroup")
                        .WithMany()
                        .HasForeignKey("ArticleGroupId");

                    b.Navigation("ArticleGroup");
                });

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.Invoices.Entities.Invoice", b =>
                {
                    b.HasOne("zbw.Auftragsverwaltung.Core.Addresses.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.Orders.Entities.Order", b =>
                {
                    b.HasOne("zbw.Auftragsverwaltung.Core.Customers.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("zbw.Auftragsverwaltung.Core.Positions.Entities.Position", b =>
                {
                    b.HasOne("zbw.Auftragsverwaltung.Core.Articles.Entities.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId");

                    b.Navigation("Article");
                });
#pragma warning restore 612, 618
        }
    }
}