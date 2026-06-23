using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllProductTypesQueryHandler : IRequestHandler<GetAllProductTypesQuery, IList<ProductTypeResponseDto>>
    {
        private IMapper _mapper;
        private readonly ITypeRepository _typeRepository;

        public GetAllProductTypesQueryHandler(IMapper mapper, ITypeRepository typeRepository)
        {
            _mapper = mapper;
            _typeRepository = typeRepository;
        }

        public async Task<IList<ProductTypeResponseDto>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var productTypesList = await _typeRepository.GetAllTypes();
            var producTypesResponseList = _mapper.Map<IList<ProductTypeResponseDto>>(productTypesList);

            return producTypesResponseList;
        }
    }
}
