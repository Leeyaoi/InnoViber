﻿// <auto-generated />
using System;
using InnoViber.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InnoViber.DAL.Migrations
{
    [DbContext(typeof(ViberContext))]
    [Migration("20240618074727_AddedChatOwner")]
    partial class AddedChatOwner
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InnoViber.DAL.Models.ChatEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("UserEntityId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("InnoViber.DAL.Models.MessageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("InnoViber.DAL.Models.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ChatEntityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ChatEntityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InnoViber.DAL.Models.ChatEntity", b =>
                {
                    b.HasOne("InnoViber.DAL.Models.UserEntity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InnoViber.DAL.Models.UserEntity", null)
                        .WithMany("Chats")
                        .HasForeignKey("UserEntityId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("InnoViber.DAL.Models.MessageEntity", b =>
                {
                    b.HasOne("InnoViber.DAL.Models.ChatEntity", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InnoViber.DAL.Models.UserEntity", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InnoViber.DAL.Models.UserEntity", b =>
                {
                    b.HasOne("InnoViber.DAL.Models.ChatEntity", null)
                        .WithMany("Users")
                        .HasForeignKey("ChatEntityId");
                });

            modelBuilder.Entity("InnoViber.DAL.Models.ChatEntity", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("InnoViber.DAL.Models.UserEntity", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
