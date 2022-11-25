﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WePromoLink.Data;

#nullable disable

namespace WePromoLink.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WePromoLink.HitAffiliateModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AffiliateLinkModelId")
                        .HasColumnType("int");

                    b.Property<int>("Counter")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FirstHitAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("GeolocatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Geolocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsGeolocated")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastHitAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("MapImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Origin")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AffiliateLinkModelId");

                    b.ToTable("HitAffiliates");
                });

            modelBuilder.Entity("WePromoLink.Models.AffiliateLinkModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Available")
                        .HasPrecision(10, 8)
                        .HasColumnType("decimal(10,8)");

                    b.Property<string>("BTCAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmailModelId")
                        .HasColumnType("int");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Group")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastClick")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("SponsoredLinkModelId")
                        .HasColumnType("int");

                    b.Property<decimal>("Threshold")
                        .HasPrecision(10, 8)
                        .HasColumnType("decimal(10,8)");

                    b.Property<decimal>("TotalEarned")
                        .HasPrecision(10, 8)
                        .HasColumnType("decimal(10,8)");

                    b.Property<decimal>("TotalWithdraw")
                        .HasPrecision(10, 8)
                        .HasColumnType("decimal(10,8)");

                    b.HasKey("Id");

                    b.HasIndex("EmailModelId");

                    b.HasIndex("SponsoredLinkModelId");

                    b.ToTable("AffiliateLinks");
                });

            modelBuilder.Entity("WePromoLink.Models.EmailModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("WePromoLink.Models.PaymentTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AffiliateLinkId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 8)
                        .HasColumnType("decimal(10,8)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeposit")
                        .HasColumnType("bit");

                    b.Property<int?>("SponsoredLinkId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PaymentTransactions");
                });

            modelBuilder.Entity("WePromoLink.Models.SponsoredLinkModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Budget")
                        .HasPrecision(10, 8)
                        .HasColumnType("decimal(10,8)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("EPM")
                        .HasPrecision(10, 8)
                        .HasColumnType("decimal(10,8)");

                    b.Property<int>("EmailModelId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastClick")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastShared")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RemainBudget")
                        .HasPrecision(10, 8)
                        .HasColumnType("decimal(10,8)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmailModelId");

                    b.ToTable("SponsoredLinks");
                });

            modelBuilder.Entity("WePromoLink.HitAffiliateModel", b =>
                {
                    b.HasOne("WePromoLink.Models.AffiliateLinkModel", "AffiliateLink")
                        .WithMany()
                        .HasForeignKey("AffiliateLinkModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AffiliateLink");
                });

            modelBuilder.Entity("WePromoLink.Models.AffiliateLinkModel", b =>
                {
                    b.HasOne("WePromoLink.Models.EmailModel", "Email")
                        .WithMany()
                        .HasForeignKey("EmailModelId");

                    b.HasOne("WePromoLink.Models.SponsoredLinkModel", "SponsoredLink")
                        .WithMany()
                        .HasForeignKey("SponsoredLinkModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Email");

                    b.Navigation("SponsoredLink");
                });

            modelBuilder.Entity("WePromoLink.Models.SponsoredLinkModel", b =>
                {
                    b.HasOne("WePromoLink.Models.EmailModel", "Email")
                        .WithMany()
                        .HasForeignKey("EmailModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Email");
                });
#pragma warning restore 612, 618
        }
    }
}
