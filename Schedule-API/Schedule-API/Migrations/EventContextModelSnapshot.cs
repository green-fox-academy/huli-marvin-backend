﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScheduleAPI.Models;

namespace ScheduleAPI.Migrations
{
    [DbContext(typeof(EventContext))]
    partial class EventContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("EventType");

                    b.HasKey("EventId");

                    b.HasIndex("EventTemplateId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ScheduleAPI.Models.EventTemplate", b =>
                {
                    b.Property<int>("EventTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EventTemplateName");

                    b.HasKey("EventTemplateId");

                    b.ToTable("EventTemplates");
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
