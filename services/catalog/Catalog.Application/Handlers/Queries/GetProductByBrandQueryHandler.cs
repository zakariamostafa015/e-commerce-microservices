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
    public class GetProductByBrandQueryHandler : IRequestHandler<GetProductByBrandQuery, IList<ProductResponseDto>>
    {
        private IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetProductByBrandQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponseDto>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProductsByBrand(request.Name);
            var productResponseList = _mapper.Map<IList<ProductResponseDto>>(productList);

            return productResponseList;
        }
    }
}
