﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vote.Queue.DatabaseContext;

namespace Vote.Queue.Migrations
{
    [DbContext(typeof(VoteContext))]
    [Migration("20190713180021_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("Vote.Queue.DatabaseContext.VoteContext+VoteEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CandidateId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Vote");
                });
#pragma warning restore 612, 618
        }
    }
}
