using Application.Common.Interfaces;
using Application.MakeMediatoR.Queries;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MakeMediatoR.QueryHandler
{
    public class GetMakeByProducingCountryHandler : IRequestHandler<GetMakeByProducingCountry, IEnumerable<Make>>
    {
        private readonly IMakeRepository _repository;

        public GetMakeByProducingCountryHandler(IMakeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Make>> Handle(GetMakeByProducingCountry request, CancellationToken cancellationToken)
        {
            return _repository.GetMakeByCountry(request.Id);
        }
    }
}
