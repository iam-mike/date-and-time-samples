﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Storing_Dates_and_Times_in_SQL_Server
{
    [DbContext(typeof(SessionBuilderContext))]
    [Migration("20190701112700_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SessionBuilder.Core.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abstract");

                    b.Property<TimeSpan>("Length");

                    b.Property<DateTimeOffset>("ScheduledAt");

                    b.Property<Guid?>("SpeakerId");

                    b.Property<DateTimeOffset>("SubmittedAt");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("SpeakerId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("SessionBuilder.Core.Speaker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("SessionBuilder.Core.Session", b =>
                {
                    b.HasOne("SessionBuilder.Core.Speaker")
                        .WithMany("Sessions")
                        .HasForeignKey("SpeakerId");
                });
#pragma warning restore 612, 618
        }
    }
}
