using Catalog.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Queries
{
    public class GetAllProductTypesQuery : IRequest<IList<ProductTypeResponseDto>>
    {
    }
}
