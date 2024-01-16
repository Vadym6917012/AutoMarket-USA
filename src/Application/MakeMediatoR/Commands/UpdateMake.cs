using Domain.Entities;
using MediatR;

namespace Application.MakeMediatoR.Commands
{
    public class UpdateMake : IRequest<Make>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImagePath { get; set; }
        public int ProducingCountryId { get; set; }
    }
}
