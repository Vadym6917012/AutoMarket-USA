﻿using Application.Common.Interfaces;
using Application.ProducingCountries.Queries;
using MediatR;

namespace Application.ProducingCountries.QueryHandler
{
    public class GetAllProducingCountriesHandler : IRequestHandler<GetAllProducingCountries, IEnumerable<ProducingCountry>>
    {
        private readonly IRepository<ProducingCountry> _repository;

        public GetAllProducingCountriesHandler(IRepository<ProducingCountry> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProducingCountry>> Handle(GetAllProducingCountries request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
