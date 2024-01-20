using MediatR;

namespace Application.Generations.Queries
{
    public class GetAllGenerations : IRequest<IEnumerable<Generation>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
    }
}
