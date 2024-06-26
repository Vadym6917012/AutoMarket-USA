﻿using Application.Common.Interfaces;
using Application.FuelTypes.Queries;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.FuelTypes.QueryHandler
{
    public class GetFuelTypeByIdHandler : IRequestHandler<GetFuelTypeById, FuelType>
    {
        private readonly IRepository<FuelType> _repository;

        public GetFuelTypeByIdHandler(IRepository<FuelType> repository)
        {
            _repository = repository;
        }

        public async Task<FuelType> Handle(GetFuelTypeById request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            return entity;
        }
    }
}
