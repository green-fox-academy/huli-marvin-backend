﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScheduleAPI.Models;

namespace ScheduleAPI.Migrations
{
    [DbContext(typeof(EventContext))]
    [Migration("20180911095144_TestSeed")]
    partial class TestSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ScheduleAPI.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EventTemplateId");

                    b.Property<string>("EventType");

                    b.HasKey("EventId");

                    b.HasIndex("EventTemplateId");

                    b.ToTable("Events");

                    b.HasData(
                        new { EventId = 1, EventTemplateId = 1, EventType = "Social" },
                        new { EventId = 2, EventTemplateId = 1, EventType = "Social" },
                        new { EventId = 3, EventTemplateId = 2, EventType = "Meeting" },
                        new { EventId = 4, EventTemplateId = 3, EventType = "Social" },
                        new { EventId = 5, EventTemplateId = 2, EventType = "Meeting" }
                    );
                });

            modelBuilder.Entity("ScheduleAPI.Models.EventTemplate", b =>
                {
                    b.Property<int>("EventTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EventTemplateName");

                    b.HasKey("EventTemplateId");

                    b.ToTable("EventTemplates");

                    b.HasData(
                        new { EventTemplateId = 1, EventTemplateName = "t0" },
                        new { EventTemplateId = 2, EventTemplateName = "t1" },
                        new { EventTemplateId = 3, EventTemplateName = "t2" }
                    );
                });

            modelBuilder.Entity("ScheduleAPI.Models.Event", b =>
                {
                    b.HasOne("ScheduleAPI.Models.EventTemplate", "EventTemplate")
                        .WithMany("Events")
                        .HasForeignKey("EventTemplateId");
                });
#pragma warning restore 612, 618
        }
    }
}
