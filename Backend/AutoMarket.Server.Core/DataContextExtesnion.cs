using AutoMarket.Server.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Server.Core
{
    public static class DataContextExtesnion
    {
        public static void Seed(this ModelBuilder builder)
        {
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

            #region Modification Entity

            builder.Entity<Modification>().HasData(new Modification
            {
                Id = 1,
                Name = "520i AT (150 hp)",
                ModelId = 1,
            }, new Modification
            {
                Id = 2,
                Name = "520i MT (150 hp)",
                ModelId = 1,
            });

            #endregion

            #region DriveTrain Entity

            builder.Entity<DriveTrain>().HasData(new DriveTrain
            {
                Id = 1,
                Name = "Передній"
            }, new DriveTrain
            {
                Id = 2,
                Name = "Задній"
            }, new DriveTrain
            {
                Id = 3,
                Name = "Повний"
            });

            #endregion

            #region TechnicalCondition Entity

            builder.Entity<TechnicalCondition>().HasData(new TechnicalCondition
            {
                Id = 1,
                Name = "Повністю непошкоджене",
                Description = "Пошкодження або раніше відремонтовані пошкодження відсутні"
            }, new TechnicalCondition
            {
                Id = 2,
                Name = "Професійно відремонтовані пошкодження",
                Description = "Пошкодження усунуті, не потребує ремонту"
            }, new TechnicalCondition
            {
                Id = 3,
                Name = "Не відремонтовані пошкодження",
                Description = "Після ДТП, сліди граду, пошкодження кузова, несправність рульового управління, коробки передач, осей і т.д."
            }, new TechnicalCondition
            {
                Id = 4,
                Name = "Не на ходу / На запчастини",
                Description = "Через ДТП, пожежу, несправності двигуна і т.д."
            });

            #endregion
        }
    }
}
