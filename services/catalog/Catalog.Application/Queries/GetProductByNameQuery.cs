using Catalog.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Queries
{
    public class GetProductByNameQuery : IRequest<IList<ProductResponseDto>>
    {
        public string Name { get; }

        public GetProductByNameQuery(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            Name = name;
        }
    }
}
