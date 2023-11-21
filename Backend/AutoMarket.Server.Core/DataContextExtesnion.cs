using AutoMarket.Server.Core.Models;
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
            }, new ProducingCountry
            {
                Id = 3,
                Name = "Франція",
            }, new ProducingCountry
            {
                Id = 4,
                Name = "США",
            }, new ProducingCountry
            {
                Id = 5,
                Name = "Корея",
            }, new ProducingCountry
            {
                Id = 6,
                Name = "Чехія",
            }, new ProducingCountry
            {
                Id = 7,
                Name = "Італія",
            }, new ProducingCountry
            {
                Id = 8,
                Name = "Швеція",
            }, new ProducingCountry
            {
                Id = 9,
                Name = "Англія",
            }, new ProducingCountry
            {
                Id = 10,
                Name = "Іспанія",
            });

            #endregion

            #region Make Entities

            builder.Entity<Make>().HasData(new Make
            {
                Id = 1,
                Name = "BMW",
                ImagePath = "https://localhost:7119/MakeIcons\\bmw.svg",
                ProducingCountryId = 1,
            }, new Make
            {
                Id = 2,
                Name = "Toyota",
                ImagePath = "https://localhost:7119/MakeIcons\\toyota.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 3,
                Name = "Audi",
                ImagePath = "https://localhost:7119/MakeIcons\\audi.svg",
                ProducingCountryId = 1
            }, new Make
            {
                Id = 4,
                Name = "Maybach",
                ImagePath = "https://localhost:7119/MakeIcons\\maybach.svg",
                ProducingCountryId = 1
            }, new Make
            {
                Id = 5,
                Name = "Mercedes-Benz",
                ImagePath = "https://localhost:7119/MakeIcons\\mercedes benz.svg",
                ProducingCountryId = 1
            }, new Make
            {
                Id = 6,
                Name = "Opel",
                ImagePath = "https://localhost:7119/MakeIcons\\opel.svg",
                ProducingCountryId = 1
            }, new Make
            {
                Id = 7,
                Name = "Porsche",
                ImagePath = "https://localhost:7119/MakeIcons\\porsche.svg",
                ProducingCountryId = 1
            }, new Make
            {
                Id = 8,
                Name = "Smart",
                ImagePath = "https://localhost:7119/MakeIcons\\smart.svg",
                ProducingCountryId = 1
            }, new Make
            {
                Id = 9,
                Name = "Volkswagen",
                ImagePath = "https://localhost:7119/MakeIcons\\volkswagen.svg",
                ProducingCountryId = 1
            }, new Make
            {
                Id = 10,
                Name = "BMW-Alpina",
                ImagePath = "https://localhost:7119/MakeIcons\\bmw.svg",
                ProducingCountryId = 1
            }, new Make
            {
                Id = 11,
                Name = "Daihatsu",
                ImagePath = "https://localhost:7119/MakeIcons\\daihatsu.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 12,
                Name = "Honda",
                ImagePath = "https://localhost:7119/MakeIcons\\honda.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 13,
                Name = "Isuzu",
                ImagePath = "https://localhost:7119/MakeIcons\\isuzu.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 14,
                Name = "Lexus",
                ImagePath = "https://localhost:7119/MakeIcons\\lexus.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 15,
                Name = "Mazda",
                ImagePath = "https://localhost:7119/MakeIcons\\mazda.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 16,
                Name = "Mitsubishi",
                ImagePath = "https://localhost:7119/MakeIcons\\mitsubishi.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 17,
                Name = "Nissan",
                ImagePath = "https://localhost:7119/MakeIcons\\nissan.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 18,
                Name = "Subaru",
                ImagePath = "https://localhost:7119/MakeIcons\\subaru.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 19,
                Name = "Suzuki",
                ImagePath = "https://localhost:7119/MakeIcons\\suzuki.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 20,
                Name = "Acura",
                ImagePath = "https://localhost:7119/MakeIcons\\acura.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 21,
                Name = "Infiniti",
                ImagePath = "https://localhost:7119/MakeIcons\\infiniti.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 22,
                Name = "Scion",
                ImagePath = "https://localhost:7119/MakeIcons\\scion.svg",
                ProducingCountryId = 2
            }, new Make
            {
                Id = 23,
                Name = "Aixam",
                ImagePath = "https://localhost:7119/MakeIcons\\aixam.svg",
                ProducingCountryId = 3
            }, new Make
            {
                Id = 24,
                Name = "Citroen",
                ImagePath = "https://localhost:7119/MakeIcons\\citroen.svg",
                ProducingCountryId = 3
            }, new Make
            {
                Id = 25,
                Name = "Peugeot",
                ImagePath = "https://localhost:7119/MakeIcons\\peugeot.svg",
                ProducingCountryId = 3
            }, new Make
            {
                Id = 26,
                Name = "Renault",
                ImagePath = "https://localhost:7119/MakeIcons\\renault.svg",
                ProducingCountryId = 3
            }, new Make
            {
                Id = 27,
                Name = "Alpine",
                ImagePath = "https://localhost:7119/MakeIcons\\alpine.svg",
                ProducingCountryId = 3
            }, new Make
            {
                Id = 28,
                Name = "Bugatti",
                ImagePath = "https://localhost:7119/MakeIcons\\bugatti.svg",
                ProducingCountryId = 3
            }, new Make
            {
                Id = 29,
                Name = "Cadillac",
                ImagePath = "https://localhost:7119/MakeIcons\\cadillac.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 30,
                Name = "Chevrolet",
                ImagePath = "https://localhost:7119/MakeIcons\\chevrolet.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 31,
                Name = "Chrysler",
                ImagePath = "https://localhost:7119/MakeIcons\\chrysler.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 32,
                Name = "Ford",
                ImagePath = "https://localhost:7119/MakeIcons\\ford.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 33,
                Name = "Jeep",
                ImagePath = "https://localhost:7119/MakeIcons\\jeep.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 34,
                Name = "Buick",
                ImagePath = "https://localhost:7119/MakeIcons\\buick.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 35,
                Name = "Dodge",
                ImagePath = "https://localhost:7119/MakeIcons\\dodge.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 36,
                Name = "Eagle",
                ImagePath = "https://localhost:7119/MakeIcons\\eagle.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 37,
                Name = "GMC",
                ImagePath = "https://localhost:7119/MakeIcons\\gmc.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 38,
                Name = "Hummer",
                ImagePath = "https://localhost:7119/MakeIcons\\hummer.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 39,
                Name = "Lincoln",
                ImagePath = "https://localhost:7119/MakeIcons\\lincoln.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 40,
                Name = "Mercury",
                ImagePath = "https://localhost:7119/MakeIcons\\mercury.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 41,
                Name = "Oldsmobile",
                ImagePath = "https://localhost:7119/MakeIcons\\oldsmobile.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 42,
                Name = "Pontiac",
                ImagePath = "https://localhost:7119/MakeIcons\\pontiac.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 43,
                Name = "Plymouth",
                ImagePath = "https://localhost:7119/MakeIcons\\plymouth.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 44,
                Name = "Saturn",
                ImagePath = "https://localhost:7119/MakeIcons\\saturn.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 45,
                Name = "Tesla",
                ImagePath = "https://localhost:7119/MakeIcons\\tesla.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 46,
                Name = "Fisker",
                ImagePath = "https://localhost:7119/MakeIcons\\fisker.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 47,
                Name = "Ram",
                ImagePath = "https://localhost:7119/MakeIcons\\ram.svg",
                ProducingCountryId = 4
            }, new Make
            {
                Id = 48,
                Name = "Daewoo",
                ImagePath = "https://localhost:7119/MakeIcons\\daewoo.svg",
                ProducingCountryId = 5
            }, new Make
            {
                Id = 49,
                Name = "Hyundai",
                ImagePath = "https://localhost:7119/MakeIcons\\hyundai.svg",
                ProducingCountryId = 5
            }, new Make
            {
                Id = 50,
                Name = "Kia",
                ImagePath = "https://localhost:7119/MakeIcons\\kia.svg",
                ProducingCountryId = 5
            }, new Make
            {
                Id = 51,
                Name = "Genesis",
                ImagePath = "https://localhost:7119/MakeIcons\\genesis.svg",
                ProducingCountryId = 5
            }, new Make
            {
                Id = 52,
                Name = "Skoda",
                ImagePath = "https://localhost:7119/MakeIcons\\skoda.svg",
                ProducingCountryId = 6
            }, new Make
            {
                Id = 53,
                Name = "Alfa Romeo",
                ImagePath = "https://localhost:7119/MakeIcons\\alfa romeo.svg",
                ProducingCountryId = 7
            }, new Make
            {
                Id = 54,
                Name = "Ferrari",
                ImagePath = "https://localhost:7119/MakeIcons\\ferrari.svg",
                ProducingCountryId = 7
            }, new Make
            {
                Id = 55,
                Name = "Fiat",
                ImagePath = "https://localhost:7119/MakeIcons\\fiat.svg",
                ProducingCountryId = 7
            }, new Make
            {
                Id = 56,
                Name = "Lamborghini",
                ImagePath = "https://localhost:7119/MakeIcons\\lamborghini.svg",
                ProducingCountryId = 7
            }, new Make
            {
                Id = 57,
                Name = "Maserati",
                ImagePath = "https://localhost:7119/MakeIcons\\maserati.svg",
                ProducingCountryId = 7
            }, new Make
            {
                Id = 58,
                Name = "Saab",
                ImagePath = "https://localhost:7119/MakeIcons\\saab.svg",
                ProducingCountryId = 8
            }, new Make
            {
                Id = 59,
                Name = "Volvo",
                ImagePath = "https://localhost:7119/MakeIcons\\volvo.svg",
                ProducingCountryId = 8
            }, new Make
            {
                Id = 60,
                Name = "Aston Martin",
                ImagePath = "https://localhost:7119/MakeIcons\\aston martin.svg",
                ProducingCountryId = 9
            }, new Make
            {
                Id = 61,
                Name = "Bentley",
                ImagePath = "https://localhost:7119/MakeIcons\\bentley.svg",
                ProducingCountryId = 9
            }, new Make
            {
                Id = 62,
                Name = "Land Rover",
                ImagePath = "https://localhost:7119/MakeIcons\\land rover.svg",
                ProducingCountryId = 9
            }, new Make
            {
                Id = 63,
                Name = "Rolls Royce",
                ImagePath = "https://localhost:7119/MakeIcons\\rolls royce.svg",
                ProducingCountryId = 9
            }, new Make
            {
                Id = 64,
                Name = "Mini",
                ImagePath = "https://localhost:7119/MakeIcons\\mini.svg",
                ProducingCountryId = 9
            }, new Make
            {
                Id = 65,
                Name = "McLaren",
                ImagePath = "https://localhost:7119/MakeIcons\\mclaren.svg",
                ProducingCountryId = 9
            }, new Make
            {
                Id = 66,
                Name = "Seat",
                ImagePath = "https://localhost:7119/MakeIcons\\seat.svg",
                ProducingCountryId = 10
            });

            #endregion

            #region Model Entities

            builder.Entity<Model>().HasData(new Model
            {
                Id = 1,
                Name = "5 Series",
                MakeId = 1,
            }, new Model
            {
                Id = 2,
                Name = "4Runner",
                MakeId = 2,
            }, new Model
            {
                Id = 3,
                Name = "8 Series",
                MakeId = 1,
            }, new Model
            {
                Id = 4,
                Name = "X5",
                MakeId = 1,
            }, new Model
            {
                Id = 5,
                Name = "Z1",
                MakeId = 1,
            }, new Model
            {
                Id = 6,
                Name = "Z3",
                MakeId = 1,
            }, new Model
            {
                Id = 7,
                Name = "Z4",
                MakeId = 1,
            }, new Model
            {
                Id = 8,
                Name = "X3",
                MakeId = 1,
            }, new Model
            {
                Id = 9,
                Name = "M Series",
                MakeId = 1,
            }, new Model
            {
                Id = 10,
                Name = "X6",
                MakeId = 1,
            }, new Model
            {
                Id = 11,
                Name = "1 Series",
                MakeId = 1,
            }, new Model
            {
                Id = 12,
                Name = "X5 M",
                MakeId = 1,
            }, new Model
            {
                Id = 13,
                Name = "M5",
                MakeId = 1,
            }, new Model
            {
                Id = 14,
                Name = "X6 M",
                MakeId = 1,
            }, new Model
            {
                Id = 15,
                Name = "M6",
                MakeId = 1,
            }, new Model
            {
                Id = 16,
                Name = "X1",
                MakeId = 1,
            }, new Model
            {
                Id = 17,
                Name = "7 Series",
                MakeId = 1,
            }, new Model
            {
                Id = 18,
                Name = "X Series",
                MakeId = 1,
            }, new Model
            {
                Id = 19,
                Name = "6 Series Gran Coupe",
                MakeId = 1,
            }, new Model
            {
                Id = 20,
                Name = "X2",
                MakeId = 1,
            }, new Model
            {
                Id = 21,
                Name = "Z Series",
                MakeId = 1,
            }, new Model
            {
                Id = 22,
                Name = "4 Series",
                MakeId = 1,
            }, new Model
            {
                Id = 23,
                Name = "2 Series",
                MakeId = 1,
            }, new Model
            {
                Id = 24,
                Name = "3 Series GT",
                MakeId = 1,
            }, new Model
            {
                Id = 25,
                Name = "X4",
                MakeId = 1,
            }, new Model
            {
                Id = 26,
                Name = "4 Series Gran Coupe",
                MakeId = 1,
            }, new Model
            {
                Id = 27,
                Name = "i8",
                MakeId = 1,
            }, new Model
            {
                Id = 28,
                Name = "2 Series Active Tourer",
                MakeId = 1,
            }, new Model
            {
                Id = 29,
                Name = "5 Series GT",
                MakeId = 1,
            }, new Model
            {
                Id = 30,
                Name = "I3",
                MakeId = 1,
            }, new Model
            {
                Id = 31,
                Name = "M2",
                MakeId = 1,
            }, new Model
            {
                Id = 32,
                Name = "M4",
                MakeId = 1,
            }, new Model
            {
                Id = 33,
                Name = "X7",
                MakeId = 1,
            }, new Model
            {
                Id = 34,
                Name = "6 Series GT",
                MakeId = 1,
            }, new Model
            {
                Id = 35,
                Name = "X3 M",
                MakeId = 1,
            }, new Model
            {
                Id = 36,
                Name = "X4 M",
                MakeId = 1,
            }, new Model
            {
                Id = 37,
                Name = "M8",
                MakeId = 1,
            }, new Model
            {
                Id = 38,
                Name = "8 Series Gran Coupe",
                MakeId = 1,
            }, new Model
            {
                Id = 39,
                Name = "2 Series Gran Coupe",
                MakeId = 1,
            }, new Model
            {
                Id = 40,
                Name = "M8 Gran Coupe",
                MakeId = 1,
            }, new Model
            {
                Id = 41,
                Name = "2 Series Gran Tourer",
                MakeId = 1,
            }, new Model
            {
                Id = 42,
                Name = "iX3",
                MakeId = 1,
            }, new Model
            {
                Id = 43,
                Name = "iX",
                MakeId = 1,
            }, new Model
            {
                Id = 44,
                Name = "i3S",
                MakeId = 1,
            }, new Model
            {
                Id = 45,
                Name = "i4",
                MakeId = 1,
            }, new Model
            {
                Id = 46,
                Name = "i7",
                MakeId = 1,
            }, new Model
            {
                Id = 47,
                Name = "1800",
                MakeId = 1,
            }, new Model
            {
                Id = 48,
                Name = "3 Series Compact",
                MakeId = 1,
            }, new Model
            {
                Id = 49,
                Name = "E9",
                MakeId = 1,
            }, new Model
            {
                Id = 50,
                Name = "E3",
                MakeId = 1,
            }, new Model
            {
                Id = 51,
                Name = "iX1",
                MakeId = 1,
            }, new Model
            {
                Id = 52,
                Name = "XM",
                MakeId = 1,
            }, new Model
            {
                Id = 53,
                Name = "i5",
                MakeId = 1,
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
            }, new Generation
            {
                Id = 3,
                Name = "G15 (FL)",
                YearFrom = 2022,
                YearTo = 2023,
            }, new Generation
            {
                Id = 4,
                Name = "G15",
                YearFrom = 2018,
                YearTo = 2023,
            }, new Generation
            {
                Id = 5,
                Name = "E31",
                YearFrom = 1987,
                YearTo = 1999,
            }, new Generation
            {
                Id = 6,
                Name = "G05 (FL)",
                YearFrom = 2023,
                YearTo = 2023,
            }, new Generation
            {
                Id = 7,
                Name = "G05",
                YearFrom = 2018,
                YearTo = 2023,
            }, new Generation
            {
                Id = 8,
                Name = "F15",
                YearFrom = 2013,
                YearTo = 2018,
            }, new Generation
            {
                Id = 9,
                Name = "E70 (FL)",
                YearFrom = 2010,
                YearTo = 2013,
            }, new Generation
            {
                Id = 10,
                Name = "E70",
                YearFrom = 2006,
                YearTo = 2010,
            }, new Generation
            {
                Id = 11,
                Name = "E53 (FL)",
                YearFrom = 2003,
                YearTo = 2006,
            }, new Generation
            {
                Id = 12,
                Name = "E53",
                YearFrom = 2000,
                YearTo = 2003,
            }, new Generation
            {
                Id = 13,
                Name = "E36/7 (FL)",
                YearFrom = 1999,
                YearTo = 2002,
            }, new Generation
            {
                Id = 14,
                Name = "E36/7",
                YearFrom = 1995,
                YearTo = 1999,
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
            }, new ModelGeneration
            {
                ModelId = 3,
                GenerationId = 3,
            }, new ModelGeneration
            {
                ModelId = 3,
                GenerationId = 4,
            }, new ModelGeneration
            {
                ModelId = 3,
                GenerationId = 5,
            }, new ModelGeneration
            {
                ModelId = 4,
                GenerationId = 6,
            }, new ModelGeneration
            {
                ModelId = 4,
                GenerationId = 7,
            }, new ModelGeneration
            {
                ModelId = 4,
                GenerationId = 8,
            }, new ModelGeneration
            {
                ModelId = 4,
                GenerationId = 9,
            }, new ModelGeneration
            {
                ModelId = 4,
                GenerationId = 10,
            }, new ModelGeneration
            {
                ModelId = 4,
                GenerationId = 11,
            }, new ModelGeneration
            {
                ModelId = 4,
                GenerationId = 12,
            }, new ModelGeneration
            {
                ModelId = 6,
                GenerationId = 13,
            }, new ModelGeneration
            {
                ModelId = 6,
                GenerationId = 14,
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
            }, new Modification
            {
                Id = 3,
                Name = "2.8 MT(192 hp)",
                ModelId = 6,
            }, new Modification
            {
                Id = 4,
                Name = "2.8 AT (192 hp)",
                ModelId = 6,
            }, new Modification
            {
                Id = 5,
                Name = "3.0i MT (231 hp)",
                ModelId = 6,
            }, new Modification
            {
                Id = 6,
                Name = "3.0i AT (231 hp)",
                ModelId = 6,
            }, new Modification
            {
                Id = 7,
                Name = "3.2i MT (321 hp)",
                ModelId = 1,
            }, new Modification
            {
                Id = 8,
                Name = "1.9 MT(140 hp)",
                ModelId = 6,
            }, new Modification
            {
                Id = 9,
                Name = "1.9 AT (140 hp)",
                ModelId = 6,
            }, new Modification
            {
                Id = 10,
                Name = "2.0 MT (150 hp)",
                ModelId = 6,
            }, new Modification
            {
                Id = 11,
                Name = "2.2i MT (170 hp)",
                ModelId = 6,
            }, new Modification
            {
                Id = 12,
                Name = "2.2i AT (170 hp)",
                ModelId = 6,
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
