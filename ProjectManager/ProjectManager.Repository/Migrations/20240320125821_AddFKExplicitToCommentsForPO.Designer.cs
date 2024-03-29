﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManager.DbContexts;

#nullable disable

namespace ProjectManager.Repository.Migrations
{
    [DbContext(typeof(ProjectManagerContext))]
    [Migration("20240320125821_AddFKExplicitToCommentsForPO")]
    partial class AddFKExplicitToCommentsForPO
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.25");

            modelBuilder.Entity("ProjectManager.Repository.Entities.Comments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CommentArea")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectObjectId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProjectObjectId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.ProjectObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Assignee")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectObjectTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SprintNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StatusId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectObjectTypeId");

                    b.HasIndex("StatusId");

                    b.ToTable("ProjectObject");
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.ProjectObjectHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectObjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectObjectId");

                    b.ToTable("ProjectObjectHistory");
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.ProjectObjectRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectObjectId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RelatedObjectId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RelationTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectObjectId");

                    b.HasIndex("RelatedObjectId");

                    b.HasIndex("RelationTypeId", "ProjectObjectId", "RelatedObjectId")
                        .IsUnique()
                        .HasDatabaseName("UniqueConstraintRelation_Index");

                    b.ToTable("ProjectObjectRelation");
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.ProjectObjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ProjectObjectType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Epic"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Feature"
                        },
                        new
                        {
                            Id = 3,
                            Type = "Story"
                        },
                        new
                        {
                            Id = 4,
                            Type = "Task"
                        },
                        new
                        {
                            Id = 5,
                            Type = "Bug"
                        });
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.RelationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RelationType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Related"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Parent"
                        },
                        new
                        {
                            Id = 3,
                            Type = "Child"
                        });
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "To Do"
                        },
                        new
                        {
                            Id = 2,
                            Type = "In Progress"
                        },
                        new
                        {
                            Id = 3,
                            Type = "Closed"
                        },
                        new
                        {
                            Id = 4,
                            Type = "Abandoned"
                        });
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.Comments", b =>
                {
                    b.HasOne("ProjectManager.Repository.Entities.ProjectObject", "ProjectObject")
                        .WithMany("Comments")
                        .HasForeignKey("ProjectObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectObject");
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.ProjectObject", b =>
                {
                    b.HasOne("ProjectManager.Repository.Entities.Project", "Project")
                        .WithMany("ProjectObjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Repository.Entities.ProjectObjectType", "ProjectObjectType")
                        .WithMany()
                        .HasForeignKey("ProjectObjectTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Repository.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("ProjectObjectType");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.ProjectObjectHistory", b =>
                {
                    b.HasOne("ProjectManager.Repository.Entities.ProjectObject", "ProjectObject")
                        .WithMany("ProjectObjectHistory")
                        .HasForeignKey("ProjectObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectObject");
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.ProjectObjectRelation", b =>
                {
                    b.HasOne("ProjectManager.Repository.Entities.ProjectObject", "ProjectObject")
                        .WithMany("ProjectObjectRelations")
                        .HasForeignKey("ProjectObjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectManager.Repository.Entities.ProjectObject", "RelatedObject")
                        .WithMany("RelatedProjectObjectRelations")
                        .HasForeignKey("RelatedObjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectManager.Repository.Entities.RelationType", "RelationType")
                        .WithMany()
                        .HasForeignKey("RelationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectObject");

                    b.Navigation("RelatedObject");

                    b.Navigation("RelationType");
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.Project", b =>
                {
                    b.Navigation("ProjectObjects");
                });

            modelBuilder.Entity("ProjectManager.Repository.Entities.ProjectObject", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ProjectObjectHistory");

                    b.Navigation("ProjectObjectRelations");

                    b.Navigation("RelatedProjectObjectRelations");
                });
#pragma warning restore 612, 618
        }
    }
}
