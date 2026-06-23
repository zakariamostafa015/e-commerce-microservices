using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(
           IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.Id) ?? throw new KeyNotFoundException($"Product '{request.Id}' not found");
            await _productRepository.DeleteProduct(request.Id);
            return true;
        }
    }
}
