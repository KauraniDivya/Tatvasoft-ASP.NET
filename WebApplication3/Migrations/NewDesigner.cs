﻿// <auto-generated />
using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication3.EFCore;

#nullable disable

namespace Books.Migrations
{
    [DbContext(typeof(EF_DataContext))]
    [Migration("20240528083720_new")]
    partial class @new
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Books.Book", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                b.Property<string>("Author")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("Genre")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("Id");

                b.ToTable("Books");
            });

            modelBuilder.Entity("ForgotPassword", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("text");

                b.Property<DateTime>("RequestDateTime")
                    .HasColumnType("timestamp with time zone");

                b.Property<int>("TempId")
                    .HasColumnType("integer");

                b.Property<int>("UserId")
                    .HasColumnType("integer");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("ForgotPasswords");
            });

            modelBuilder.Entity("User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                b.Property<string>("EmailAddress")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("PhoneNumber")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("UserFullName")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("UserType")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("Id");

                b.ToTable("Users");
            });

            modelBuilder.Entity("ForgotPassword", b =>
            {
                b.HasOne("User", "User")
                    .WithMany("ForgotPasswords")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("User");
            });

            modelBuilder.Entity("User", b =>
            {
                b.Navigation("ForgotPasswords");
            });
#pragma warning restore 612, 618
        }
    }
}