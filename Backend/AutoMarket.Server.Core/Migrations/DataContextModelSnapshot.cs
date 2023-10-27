﻿// <auto-generated />
using System;
using AutoMarket.Server.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutoMarket.Server.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AutoMarket.Server.Core.BodyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BodyTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Седан"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Хетчбек"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Мінівен"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Купе"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Позашляховик / Кросовер"
                        });
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BodyTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FuelTypeId")
                        .HasColumnType("int");

                    b.Property<int>("GearBoxTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<int>("ModificationId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BodyTypeId");

                    b.HasIndex("FuelTypeId");

                    b.HasIndex("GearBoxTypeId");

                    b.HasIndex("ModelId");

                    b.HasIndex("ModificationId");

                    b.HasIndex("UserId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.FuelType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FuelTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Бензин"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Газ"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Газ метан / Бензин"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Газ пропан-бутан / Бензин"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Гібрид (HEV)"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Гібрид (PHEV)"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Дизель"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Електро"
                        });
                });

            modelBuilder.Entity("AutoMarket.Server.Core.GearBoxType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GearBoxes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Ручна / Механіка"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Автомат"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Типтронік"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Робот"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Варіатор"
                        });
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Generation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearFrom")
                        .HasColumnType("int");

                    b.Property<int>("YearTo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Generations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "E39",
                            YearFrom = 1995,
                            YearTo = 2000
                        },
                        new
                        {
                            Id = 2,
                            Name = "E39 [restyling]",
                            YearFrom = 2000,
                            YearTo = 2004
                        });
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Images", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePathToDisplay")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Make", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProducingCountryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProducingCountryId");

                    b.ToTable("Makes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "BMW",
                            ProducingCountryId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Toyota",
                            ProducingCountryId = 2
                        });
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MakeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.ToTable("Models");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MakeId = 1,
                            Name = "5 series"
                        },
                        new
                        {
                            Id = 2,
                            MakeId = 2,
                            Name = "4Runner"
                        });
                });

            modelBuilder.Entity("AutoMarket.Server.Core.ModelGeneration", b =>
                {
                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<int>("GenerationId")
                        .HasColumnType("int");

                    b.HasKey("ModelId", "GenerationId");

                    b.HasIndex("GenerationId");

                    b.ToTable("ModelGeneration");

                    b.HasData(
                        new
                        {
                            ModelId = 1,
                            GenerationId = 1
                        },
                        new
                        {
                            ModelId = 1,
                            GenerationId = 2
                        });
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Modification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Modifications");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.ProducingCountry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProducingCountries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Німеччина"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Японія"
                        });
                });

            modelBuilder.Entity("AutoMarket.Server.Core.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "26a4b801-1cc6-4810-8ac0-0cf05ce04951",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "829724cd-a4d6-4def-8fae-ce8ed92fc0e0",
                            Email = "admin@autospot.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@AUTOSPOT.COM",
                            NormalizedUserName = "ADMIN@AUTOSPOT.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEBz3y8AKJOExcyutAlTFT+Kqb8cItDZpkCWZqp7xBQW9hAskjjUpnyqyYW+hJaLVGA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c82c2ffb-d3ee-43fc-bb0a-a6a34d3b7072",
                            TwoFactorEnabled = false,
                            UserName = "admin@autospot.com"
                        },
                        new
                        {
                            Id = "32439914-c414-4375-bb6d-88eddfc04376",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9fbf5a65-1209-4ea0-bf4e-837feb049be3",
                            Email = "user@autospot.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@AUTOSPOT.COM",
                            NormalizedUserName = "USER@AUTOSPOT.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEOU3jTyZviFsCYwZcw11OjHde9xY4enf7J9Y8RhXQcM20LQ751WeiDRo7mdr/sfp6w==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7b134f4b-0a3b-40a8-aaf4-31d980a354ec",
                            TwoFactorEnabled = false,
                            UserName = "user@autospot.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "5163ea3d-50a4-4f0f-8b16-ba60df61a9ed",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "40bca976-415a-4db0-a65f-aedbb14532e2",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "26a4b801-1cc6-4810-8ac0-0cf05ce04951",
                            RoleId = "5163ea3d-50a4-4f0f-8b16-ba60df61a9ed"
                        },
                        new
                        {
                            UserId = "32439914-c414-4375-bb6d-88eddfc04376",
                            RoleId = "5163ea3d-50a4-4f0f-8b16-ba60df61a9ed"
                        },
                        new
                        {
                            UserId = "32439914-c414-4375-bb6d-88eddfc04376",
                            RoleId = "40bca976-415a-4db0-a65f-aedbb14532e2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Car", b =>
                {
                    b.HasOne("AutoMarket.Server.Core.BodyType", "BodyType")
                        .WithMany("Cars")
                        .HasForeignKey("BodyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoMarket.Server.Core.FuelType", "FuelType")
                        .WithMany("Cars")
                        .HasForeignKey("FuelTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoMarket.Server.Core.GearBoxType", "GearBoxType")
                        .WithMany("Cars")
                        .HasForeignKey("GearBoxTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoMarket.Server.Core.Model", "Model")
                        .WithMany("Cars")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoMarket.Server.Core.Modification", "Modification")
                        .WithMany("Cars")
                        .HasForeignKey("ModificationId")
                        .IsRequired();

                    b.HasOne("AutoMarket.Server.Core.User", "User")
                        .WithMany("Cars")
                        .HasForeignKey("UserId");

                    b.Navigation("BodyType");

                    b.Navigation("FuelType");

                    b.Navigation("GearBoxType");

                    b.Navigation("Model");

                    b.Navigation("Modification");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Images", b =>
                {
                    b.HasOne("AutoMarket.Server.Core.Car", "Car")
                        .WithMany("Images")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Make", b =>
                {
                    b.HasOne("AutoMarket.Server.Core.ProducingCountry", "ProducingCountry")
                        .WithMany("Makes")
                        .HasForeignKey("ProducingCountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProducingCountry");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Model", b =>
                {
                    b.HasOne("AutoMarket.Server.Core.Make", "Make")
                        .WithMany("Models")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Make");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.ModelGeneration", b =>
                {
                    b.HasOne("AutoMarket.Server.Core.Generation", "Generation")
                        .WithMany("ModelGenerations")
                        .HasForeignKey("GenerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoMarket.Server.Core.Model", "Model")
                        .WithMany("ModelGenerations")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Generation");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Modification", b =>
                {
                    b.HasOne("AutoMarket.Server.Core.Model", "Model")
                        .WithMany("Modifications")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AutoMarket.Server.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AutoMarket.Server.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoMarket.Server.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AutoMarket.Server.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AutoMarket.Server.Core.BodyType", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Car", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.FuelType", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.GearBoxType", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Generation", b =>
                {
                    b.Navigation("ModelGenerations");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Make", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Model", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("ModelGenerations");

                    b.Navigation("Modifications");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.Modification", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.ProducingCountry", b =>
                {
                    b.Navigation("Makes");
                });

            modelBuilder.Entity("AutoMarket.Server.Core.User", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
