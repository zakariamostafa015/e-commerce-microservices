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
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponseDto>>
    {
        private IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetProductByNameQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }


        public async Task<IList<ProductResponseDto>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProductsByName(request.Name);
            var productResponseList = _mapper.Map<IList<ProductResponseDto>>(productList);

            return productResponseList;
        }
    }
}
