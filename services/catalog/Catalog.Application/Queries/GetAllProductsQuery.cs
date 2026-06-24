using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Queries
{
    public class GetAllProductsQuery : IRequest<Pagination<ProductResponseDto>>
    {
        public CatalogSpecsParams CatalogSpecsParams { get; set; }

        public GetAllProductsQuery(CatalogSpecsParams catalogSpecsParams)
        {
            CatalogSpecsParams = catalogSpecsParams;
        }
    }
}
