﻿// <auto-generated />
using DictionaryAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DictionaryAPI.Migrations
{
    [DbContext(typeof(DictionaryDB))]
    [Migration("20221121174958_ExampleSentenceSetNull")]
    partial class ExampleSentenceSetNull
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DictionaryAPI.Models.Concretes.Definition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("DefinitionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExampleSentence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WordDefinition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("WordId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.ToTable("Definitions");
                });

            modelBuilder.Entity("DictionaryAPI.Models.Concretes.Word", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("DictionaryAPI.Models.Concretes.Definition", b =>
                {
                    b.HasOne("DictionaryAPI.Models.Concretes.Word", "Word")
                        .WithMany("Definitions")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Word");
                });

            modelBuilder.Entity("DictionaryAPI.Models.Concretes.Word", b =>
                {
                    b.Navigation("Definitions");
                });
#pragma warning restore 612, 618
        }
    }
}
