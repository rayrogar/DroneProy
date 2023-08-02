﻿// <auto-generated />
using System;
using Dispatch_Controller_RESTapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dispatch_Controller_RESTapi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230726073032_updateModel")]
    partial class updateModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("Dispatch_Controller_RESTapi.Models.DroneEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BateryState")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DroneId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DroneModelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Serial")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("StateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WeigthLimit")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DroneModelId");

                    b.HasIndex("StateId");

                    b.ToTable("Drones");
                });

            modelBuilder.Entity("Dispatch_Controller_RESTapi.Models.DroneMedicationsEntity", b =>
                {
                    b.Property<int>("DroneId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MedicationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DroneId", "MedicationId");

                    b.HasIndex("MedicationId");

                    b.ToTable("DroneMedicationsEntity");
                });

            modelBuilder.Entity("Dispatch_Controller_RESTapi.Models.MedicationEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("DroneEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Weigth")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("DroneEntityId");

                    b.ToTable("Medications");
                });

            modelBuilder.Entity("Dispatch_Controller_RESTapi.Models.ModelEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DroneModel");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Lightweight"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Middleweight"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cruiserweight"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Heavyweight"
                        });
                });

            modelBuilder.Entity("Dispatch_Controller_RESTapi.Models.StateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("States");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "IDLE"
                        },
                        new
                        {
                            Id = 2,
                            Name = "LOADING"
                        },
                        new
                        {
                            Id = 3,
                            Name = "LOADED"
                        },
                        new
                        {
                            Id = 4,
                            Name = "DELIVERING"
                        },
                        new
                        {
                            Id = 5,
                            Name = "DELIVERED"
                        },
                        new
                        {
                            Id = 6,
                            Name = "RETURNING"
                        });
                });

            modelBuilder.Entity("Dispatch_Controller_RESTapi.Models.DroneEntity", b =>
                {
                    b.HasOne("Dispatch_Controller_RESTapi.Models.ModelEntity", "DroneModel")
                        .WithMany()
                        .HasForeignKey("DroneModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dispatch_Controller_RESTapi.Models.StateEntity", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DroneModel");

                    b.Navigation("State");
                });

            modelBuilder.Entity("Dispatch_Controller_RESTapi.Models.DroneMedicationsEntity", b =>
                {
                    b.HasOne("Dispatch_Controller_RESTapi.Models.DroneEntity", "Drone")
                        .WithMany()
                        .HasForeignKey("DroneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dispatch_Controller_RESTapi.Models.MedicationEntity", "Medication")
                        .WithMany()
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drone");

                    b.Navigation("Medication");
                });

            modelBuilder.Entity("Dispatch_Controller_RESTapi.Models.MedicationEntity", b =>
                {
                    b.HasOne("Dispatch_Controller_RESTapi.Models.DroneEntity", null)
                        .WithMany("Carga")
                        .HasForeignKey("DroneEntityId");
                });

            modelBuilder.Entity("Dispatch_Controller_RESTapi.Models.DroneEntity", b =>
                {
                    b.Navigation("Carga");
                });
#pragma warning restore 612, 618
        }
    }
}
