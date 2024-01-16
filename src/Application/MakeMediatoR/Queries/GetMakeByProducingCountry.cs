using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MakeMediatoR.Queries
{
    public class GetMakeByProducingCountry : IRequest<IEnumerable<Make>>
    {
        public int Id { get; set; }
    }
}
