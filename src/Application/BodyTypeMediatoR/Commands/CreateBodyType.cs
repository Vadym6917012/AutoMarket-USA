using MediatR;
using Domain.Entities;

namespace Application.BodyTypeMediatoR.Commands
{
    public class CreateBodyType : IRequest<BodyType>
    {
        public string? Name { get; set; }
    }
}
