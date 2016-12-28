using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EventAppCore.Models;

namespace EventAppCore.Migrations
{
    [DbContext(typeof(MainContext))]
    partial class MainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("EventAppCore.Models.Event", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CanceledReason");

                    b.Property<string>("CreatedById");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<DateTimeOffset>("EndTime");

                    b.Property<bool>("IsCanceled");

                    b.Property<bool>("IsPrivate");

                    b.Property<string>("LocationId");

                    b.Property<string>("MainImageUrl");

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventAppCore.Models.Location", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("CreatedById");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("EventAppCore.Models.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BelongsToId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Device");

                    b.Property<bool>("Revoked");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.HasIndex("BelongsToId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("EventAppCore.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessCode");

                    b.Property<int>("AccessLevel");

                    b.Property<DateTimeOffset>("BirthDate");

                    b.Property<string>("ConfirmationToken");

                    b.Property<bool>("Confirmed");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(200);

                    b.Property<string>("FirstName");

                    b.Property<string>("ForgottenPasswordToken");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("ProfilePictureUrl");

                    b.Property<string>("SaltAndHash");

                    b.Property<string>("Ssn");

                    b.Property<bool>("Suspended");

                    b.Property<string>("SystemNotes");

                    b.Property<string>("Username")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EventAppCore.Models.UserEvent", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("EventId");

                    b.Property<string>("Note");

                    b.Property<int>("Role");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("UserEvents");
                });

            modelBuilder.Entity("EventAppCore.Models.Event", b =>
                {
                    b.HasOne("EventAppCore.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("EventAppCore.Models.Location", "Location")
                        .WithMany("Events")
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("EventAppCore.Models.Location", b =>
                {
                    b.HasOne("EventAppCore.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });

            modelBuilder.Entity("EventAppCore.Models.RefreshToken", b =>
                {
                    b.HasOne("EventAppCore.Models.User", "BelongsTo")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("BelongsToId");
                });

            modelBuilder.Entity("EventAppCore.Models.UserEvent", b =>
                {
                    b.HasOne("EventAppCore.Models.Event", "Event")
                        .WithMany("UserEvents")
                        .HasForeignKey("EventId");

                    b.HasOne("EventAppCore.Models.User", "User")
                        .WithMany("UserEvents")
                        .HasForeignKey("UserId");
                });
        }
    }
}
