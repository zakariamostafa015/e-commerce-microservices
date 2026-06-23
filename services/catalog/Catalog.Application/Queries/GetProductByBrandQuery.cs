using Catalog.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Queries
{
    public class GetProductByBrandQuery : IRequest<IList<ProductResponseDto>>
    {
        public string Name { get; }

        public GetProductByBrandQuery(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            Name = name;
        }
    }
}
