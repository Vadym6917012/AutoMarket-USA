using System.ComponentModel.DataAnnotations;

namespace AutoMarket.Server.Shared.DTOs.Car
{
    public class CarCreateDTO
    {
        public CarCreateDTO() 
        {
            Year = DateTime.Now.Year;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Модель є обов'язковим полем.")]
        public int ModelId { get; set; }

        [Required(ErrorMessage = "Покоління є обов'язковим полем.")]
        public int GenerationId { get; set; }

        [Required(ErrorMessage = "Модифікація є обов'язковим полем.")]
        public int? ModificationId { get; set; }


        [RegularExpression("^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "VIN повинен бути рядком з 17 символів, що містить великі літери (крім I, O, Q) та цифри.")]
        public string? VIN { get; set; }

        [Required(ErrorMessage = "Тип кузова є обов'язковим полем.")]
        public int BodyTypeId { get; set; }

        [Required(ErrorMessage = "Тип коробки передач є обов'язковим полем.")]
        public int GearBoxTypeId { get; set; }

        [Required(ErrorMessage = "Тип приводу є обов'язковим полем.")]
        public int DriveTrainId { get; set; }

        [Required(ErrorMessage = "Технічний стан є обов'язковим полем.")]
        public int TechnicalConditionId { get; set; }

        [Required(ErrorMessage = "Тип палива є обов'язковим полем.")]
        public int FuelTypeId { get; set; }

        [Range(1000, int.MaxValue, ErrorMessage = "Рік повинен бути чотирицифровим числом.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Ціна є обов'язковим полем.")]
        [RegularExpression("^\\d+$", ErrorMessage = "Ціна не може бути від'ємна")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Пробіг є обов'язковим полем.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Пробіг не може бути від'ємним")]
        public int Mileage { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }
    }
}
