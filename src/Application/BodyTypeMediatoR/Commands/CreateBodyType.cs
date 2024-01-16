using Domain.Entities;
using MediatR;

namespace Application.BodyTypeMediatoR.Commands
{
    public class CreateBodyType : IRequest<BodyType>
    {
        public string? Name { get; set; }
    }
}
