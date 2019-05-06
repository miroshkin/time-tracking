﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TimeTrackingService.Models;

namespace TimeTrackingService.Migrations
{
    [DbContext(typeof(TimeTrackingServiceContext))]
    [Migration("20190506091932_AddedTablesMigration")]
    partial class AddedTablesMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TimeTrackingService.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TimeTrackingService.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ProjectId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TimeTrackingService.Models.TimeRegistration", b =>
                {
                    b.Property<int>("TimeRegistrationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("Duration");

                    b.Property<int?>("ProjectId");

                    b.Property<int>("WorkTypeId");

                    b.HasKey("TimeRegistrationId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("WorkTypeId");

                    b.ToTable("TimeRegistrations");
                });

            modelBuilder.Entity("TimeTrackingService.Models.WorkType", b =>
                {
                    b.Property<int>("WorkTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.HasKey("WorkTypeId");

                    b.ToTable("WorkTypes");
                });

            modelBuilder.Entity("TimeTrackingService.Models.Project", b =>
                {
                    b.HasOne("TimeTrackingService.Models.Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("TimeTrackingService.Models.TimeRegistration", b =>
                {
                    b.HasOne("TimeTrackingService.Models.Project")
                        .WithMany("TimeRegistrations")
                        .HasForeignKey("ProjectId");

                    b.HasOne("TimeTrackingService.Models.WorkType", "WorkType")
                        .WithMany("TimeRegistrations")
                        .HasForeignKey("WorkTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}