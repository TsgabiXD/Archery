﻿// <auto-generated />
using System;
using Archery.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Archery.Api.Migrations
{
    [DbContext(typeof(ArcheryContext))]
    partial class ArcheryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Archery.Model.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRunning")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ParcourId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParcourId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Archery.Model.Mapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Mapping");
                });

            modelBuilder.Entity("Archery.Model.Parcour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AnimalNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Parcour");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AnimalNumber = 30,
                            Location = "Kirchschlag",
                            Name = "Dinosaurier"
                        });
                });

            modelBuilder.Entity("Archery.Model.Target", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArrowCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HitArea")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MappingId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MappingId");

                    b.ToTable("Target");
                });

            modelBuilder.Entity("Archery.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Tobias",
                            LastName = "Schachner",
                            NickName = "TsgabiXD",
                            Role = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Luka",
                            LastName = "Walkner",
                            NickName = "woiges",
                            Role = "Admin"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Johannes",
                            LastName = "R�lz",
                            NickName = "JoRole",
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("IdentityUser");
                });

            modelBuilder.Entity("Archery.Model.Event", b =>
                {
                    b.HasOne("Archery.Model.Parcour", "Parcour")
                        .WithMany()
                        .HasForeignKey("ParcourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parcour");
                });

            modelBuilder.Entity("Archery.Model.Mapping", b =>
                {
                    b.HasOne("Archery.Model.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Archery.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Archery.Model.Target", b =>
                {
                    b.HasOne("Archery.Model.Mapping", null)
                        .WithMany("Target")
                        .HasForeignKey("MappingId");
                });

            modelBuilder.Entity("Archery.Model.Mapping", b =>
                {
                    b.Navigation("Target");
                });
#pragma warning restore 612, 618
        }
    }
}
