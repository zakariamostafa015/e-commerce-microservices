using Catalog.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Commands
{
    public class UpdateProductCommand : IRequest<ProductResponseDto>
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? Summary { get; set; }
        public string? ImageFile { get; set; }
        public decimal Price { get; set; }
        public string BrandId { get; set; } = default!;
        public string TypeId { get; set; } = default!;
    }
}
