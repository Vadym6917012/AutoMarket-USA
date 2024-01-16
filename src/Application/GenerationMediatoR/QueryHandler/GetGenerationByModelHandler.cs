﻿using Application.Common.Interfaces;
using Application.GenerationMediatoR.Queries;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GenerationMediatoR.QueryHandler
{
    public class GetGenerationByModelHandler : IRequestHandler<GetGenerationsByModel, IEnumerable<Generation>>
    {
        private readonly IGenerationRepository _repository;

        public GetGenerationByModelHandler(IGenerationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Generation>> Handle (GetGenerationsByModel request, CancellationToken cancellationToken)
        {
            return await _repository.GetGenerationsByModel(request.Id);
        }
    }
}
