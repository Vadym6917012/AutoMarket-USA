﻿using Application.Common.Interfaces;
using Application.ProducingCountries.Queries;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.ProducingCountries.QueryHandler
{
    public class GetProducingCountryByIdHandler : IRequestHandler<GetProducingCountryById, ProducingCountry>
    {
        private readonly IRepository<ProducingCountry> _repository;

        public GetProducingCountryByIdHandler(IRepository<ProducingCountry> repository)
        {
            _repository = repository;
        }

        public async Task<ProducingCountry> Handle(GetProducingCountryById request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, "Такої країни не було знайдено");

            return entity;
        }
    }
}
