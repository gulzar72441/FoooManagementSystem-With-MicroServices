﻿// <auto-generated />
using System;
using FoodOrderingSystem.Review.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodOrderingSystem.Review.Infrastructure.Migrations
{
    [DbContext(typeof(ReviewDbContext))]
    partial class ReviewDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodOrderingSystem.Review.Domain.Entities.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EntityType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("RatingDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("FoodOrderingSystem.Review.Domain.Entities.ReportedReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("ReportedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ReportedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ReviewId");

                    b.ToTable("ReportedReviews");
                });

            modelBuilder.Entity("FoodOrderingSystem.Review.Domain.Entities.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EntityType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsAbusive")
                        .HasColumnType("bit");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("FoodOrderingSystem.Review.Domain.Entities.ReportedReview", b =>
                {
                    b.HasOne("FoodOrderingSystem.Review.Domain.Entities.Review", "Review")
                        .WithMany()
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");
                });
#pragma warning restore 612, 618
        }
    }
}
