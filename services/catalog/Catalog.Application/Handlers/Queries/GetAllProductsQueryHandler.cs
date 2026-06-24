using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponseDto>>
    {
        private IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<Pagination<ProductResponseDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProducts(request.CatalogSpecsParams);
            var productResponseList = _mapper.Map<Pagination<ProductResponseDto>>(productList);

            return productResponseList;
        }
    }
}
