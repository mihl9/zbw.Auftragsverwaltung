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
    [Migration("20210905192027_AddedArticle")]
    partial class AddedArticle
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
#pragma warning restore 612, 618
        }
    }
}
