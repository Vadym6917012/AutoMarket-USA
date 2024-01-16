using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GenerationMediatoR.Queries
{
    public class GetGenerationsByModel : IRequest<IEnumerable<Generation>>
    {
        public int Id { get; set; }
    }
}
