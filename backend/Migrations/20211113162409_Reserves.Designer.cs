﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Data;

namespace backend.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211113162409_Reserves")]
    partial class Reserves
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("backend.Models.Bedroom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApartmentNumber")
                        .HasColumnType("int")
                        .HasColumnName("apartment_number");

                    b.HasKey("Id");

                    b.ToTable("Bedrooms");
                });

            modelBuilder.Entity("backend.Models.Reserve", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BedroomId")
                        .HasColumnType("int")
                        .HasColumnName("bedroom_id");

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime2")
                        .HasColumnName("check_in");

                    b.Property<DateTime?>("CheckOut")
                        .HasColumnType("datetime2")
                        .HasColumnName("check_out");

                    b.Property<int>("HotelGuestId")
                        .HasColumnType("int")
                        .HasColumnName("hotel_guest_id");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BedroomId");

                    b.HasIndex("UserId");

                    b.ToTable("Reserves");
                });

            modelBuilder.Entity("backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<bool>("Manager")
                        .HasColumnType("bit")
                        .HasColumnName("manager");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasColumnName("surname");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("backend.Models.Reserve", b =>
                {
                    b.HasOne("backend.Models.Bedroom", "Bedroom")
                        .WithMany("Reserves")
                        .HasForeignKey("BedroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.User", "User")
                        .WithMany("Reserves")
                        .HasForeignKey("UserId");

                    b.Navigation("Bedroom");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Models.Bedroom", b =>
                {
                    b.Navigation("Reserves");
                });

            modelBuilder.Entity("backend.Models.User", b =>
                {
                    b.Navigation("Reserves");
                });
#pragma warning restore 612, 618
        }
    }
}
