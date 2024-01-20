using MediatR;

namespace Application.BodyTypes.Queries
{
    public class GetBodyTypeById : IRequest<BodyType>
    {
        public int Id { get; set; }
    }
}
