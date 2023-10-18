using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Server.Core
{
    public static class DataContextExtesnion
    {
        public static void Seed(this ModelBuilder builder)
        {
            #region Identity Entities

            string ADMIN_ROLE_ID = Guid.NewGuid().ToString();
            string USER_ROLE_ID = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ADMIN_ROLE_ID,
                Name = "Admin",
                NormalizedName = "ADMIN",
            }, new IdentityRole
            {
                Id = USER_ROLE_ID,
                Name = "User",
                NormalizedName = "USER"
            });

            string ADMIN_ID = Guid.NewGuid().ToString();
            string USER_ID = Guid.NewGuid().ToString();

            var admin = new User
            {
                Id = ADMIN_ID,
                UserName = "admin@autospot.com",
                Email = "admin@autospot.com",
                EmailConfirmed = true,
                NormalizedEmail = "admin@autospot.com".ToUpper(),
                NormalizedUserName = "admin@autospot.com".ToUpper(),
            };
            var user = new User
            {
                Id = USER_ID,
                UserName = "user@autospot.com",
                Email = "user@autospot.com",
                EmailConfirmed = true,
                NormalizedEmail = "user@autospot.com".ToUpper(),
                NormalizedUserName = "user@autospot.com".ToUpper(),
            };

            PasswordHasher<User> hasher = new PasswordHasher<User>();

            admin.PasswordHash = hasher.HashPassword(admin, "admin$Password1");
            user.PasswordHash = hasher.HashPassword(user, "user$Password1");

            builder.Entity<User>().HasData(admin, user);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID,
            }, new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = USER_ID,
            }, new IdentityUserRole<string>
            {
                RoleId = USER_ROLE_ID,
                UserId = USER_ID,
            });

            #endregion

            #region BodyTypes Entities

            builder.Entity<BodyType>().HasData(new BodyType
            {
                Id = 1,
                Name = "Седан",
            }, new BodyType
            {
                Id = 2,
                Name = "Хетчбек"
            }, new BodyType
            {
                Id = 3,
                Name = "Мінівен"
            }, new BodyType
            {
                Id = 4,
                Name = "Купе"
            }, new BodyType
            {
                Id = 5,
                Name = "Позашляховик / Кросовер"
            });

            #endregion

            #region Fuel Type Entities

            builder.Entity<FuelType>().HasData(new FuelType
            {
                Id = 1,
                Name = "Бензин"
            }, new FuelType
            {
                Id = 2,
                Name = "Газ"
            }, new FuelType
            {
                Id = 3,
                Name = "Газ метан / Бензин"
            }, new FuelType
            {
                Id = 4,
                Name = "Газ пропан-бутан / Бензин"
            }, new FuelType
            {
                Id = 5,
                Name = "Гібрид (HEV)"
            }, new FuelType
            {
                Id = 6,
                Name = "Гібрид (PHEV)"
            }, new FuelType
            {
                Id = 7,
                Name = "Дизель"
            }, new FuelType
            {
                Id = 8,
                Name = "Електро"
            });

            #endregion

            #region Gear Box Entities

            builder.Entity<GearBoxType>().HasData(new GearBoxType
            {
                Id = 1,
                Name = "Ручна / Механіка"
            }, new GearBoxType
            {
                Id = 2,
                Name = "Автомат"
            }, new GearBoxType
            {
                Id = 3,
                Name = "Типтронік"
            }, new GearBoxType
            {
                Id = 4,
                Name = "Робот"
            }, new GearBoxType
            {
                Id = 5,
                Name = "Варіатор"
            });

            #endregion

            #region Producing Country Entities

            builder.Entity<ProducingCountry>().HasData(new ProducingCountry
            {
                Id = 1,
                Name = "Німеччина",
            }, new ProducingCountry
            {
                Id = 2,
                Name = "Японія",
            });

            #endregion

            #region Make Entities

            builder.Entity<Make>().HasData(new Make
            {
                Id = 1,
                Name = "BMW",
                ProducingCountryId = 1,
            }, new Make
            {
                Id = 2,
                Name = "Toyota",
                ProducingCountryId = 2
            });

            #endregion

            #region Model Entities

            builder.Entity<Model>().HasData(new Model
            {
                Id = 1,
                Name = "5 series",
                MakeId = 1,
            }, new Model
            {
                Id = 2,
                Name = "4Runner",
                MakeId = 2,
            });

            #endregion

            #region Generation Entities

            builder.Entity<Generation>().HasData(new Generation
            {
                Id = 1,
                Name = "E39",
                YearFrom = 1995,
                YearTo = 2000,
            }, new Generation
            {
                Id = 2,
                Name = "E39 [restyling]",
                YearFrom = 2000,
                YearTo = 2004,
            });

            #endregion

            #region ModelGeneration Entities

            builder.Entity<ModelGeneration>().HasData(new ModelGeneration
            {
                ModelId = 1,
                GenerationId = 1,
            }, new ModelGeneration
            {
                ModelId = 1,
                GenerationId = 2,
            });

            #endregion
        }
    }
}
