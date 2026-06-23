using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponseDto>
    {
        private IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ITypeRepository _typeRepository;

        public CreateProductCommandHandler(
            IMapper mapper,
            IProductRepository productRepository,
            IBrandRepository brandRepository,
            ITypeRepository typeRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
        }
        public async Task<ProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetBrandById(request.BrandId) ?? throw new KeyNotFoundException(
                    $"Brand with id '{request.BrandId}' was not found.");
            var type = await _typeRepository.GetTypeById(request.TypeId) ?? throw new KeyNotFoundException(
                    $"Type with id '{request.TypeId}' was not found.");

            //var existingProduct =
            //    await _productRepository.GetProductByName(request.Name);

            //if (existingProduct is not null)
            //    throw new InvalidOperationException(
            //        $"Product '{request.Name}' already exists.");

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Summary = request.Summary,
                ImageFile = request.ImageFile,
                Price = request.Price,
                Brand = brand,
                Type = type
            };

            var createdProduct =
                await _productRepository.CreateProduct(product);

            return _mapper.Map<ProductResponseDto>(createdProduct);
        }
    }
}
