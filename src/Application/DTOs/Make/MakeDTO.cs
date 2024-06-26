﻿namespace Application.DTOs.Make
{
    public class MakeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public int ProducingCountryId { get; set; }
        public List<int>? ModelsId { get; set; }
    }
}
