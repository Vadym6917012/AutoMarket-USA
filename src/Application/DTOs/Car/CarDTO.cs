﻿namespace Application.DTOs.Car
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string? MakeName { get; set; }
        public string? ModelName { get; set; }
        public string? GenerationName { get; set; }
        public string? ModificationName { get; set; }
        public string? VIN { get; set; }
        public string? BodyTypeName { get; set; }
        public string? GearBoxTypeName { get; set; }
        public string? DriveTrainName { get; set; }
        public string? TechnicalConditionName { get; set; }
        public string? FuelTypeName { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public string? Description { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public List<string>? ImagesPath { get; set; }
        public bool? IsAdvertisementApproved { get; set; } = false;
        public DateTime? DateCreated { get; set; } = DateTime.UtcNow;
    }
}
