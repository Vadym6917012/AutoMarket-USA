﻿using Application.Common.Interfaces;
using Application.ProducingCountryMediatoR.Commands;
using Domain.Entities;
using MediatR;

namespace Application.ProducingCountryMediatoR.CommandHandler
{
    public class CreateProducingCountryHandler : IRequestHandler<CreateProducingCountry, ProducingCountry>
    {
        private readonly IRepository<ProducingCountry> _repository;

        public CreateProducingCountryHandler(IRepository<ProducingCountry> repository)
        {
            _repository = repository;
        }

        public async Task<ProducingCountry> Handle(CreateProducingCountry request, CancellationToken cancellationToken)
        {
            var entity = new ProducingCountry
            {
                Name = request.Name,
            };

            return await _repository.AddAsync(entity);
        }
    }
}
