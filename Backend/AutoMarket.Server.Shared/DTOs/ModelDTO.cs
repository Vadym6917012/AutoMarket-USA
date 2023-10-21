﻿namespace AutoMarket.Server.Shared.DTOs
{
    public class ModelDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MakeId { get; set; }
        public ICollection<string>? Generations { get; set; }
    }
}
