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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponseDto>
    {
        private IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ITypeRepository _typeRepository;

        public UpdateProductCommandHandler(
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
        public async Task<ProductResponseDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.Id) ?? throw new KeyNotFoundException($"Product '{request.Id}' not found");
            var brand = await _brandRepository.GetBrandById(request.BrandId) ?? throw new KeyNotFoundException(
                    $"Brand with id '{request.BrandId}' was not found.");
            var type = await _typeRepository.GetTypeById(request.TypeId) ?? throw new KeyNotFoundException(
                    $"Type with id '{request.TypeId}' was not found.");

            //var existingProduct =
            //    await _productRepository.GetProductByName(request.Name);

            //if (existingProduct is not null)
            //    throw new InvalidOperationException(
            //        $"Product '{request.Name}' already exists.");

            product.Name = request.Name;
            product.Description = request.Description;
            product.Summary = request.Summary;
            product.ImageFile = request.ImageFile;
            product.Price = request.Price;
            product.Brand = brand;
            product.Type = type;

            await _productRepository.UpdateProduct(product);

            return _mapper.Map<ProductResponseDto>(product);
        }
    }
}
