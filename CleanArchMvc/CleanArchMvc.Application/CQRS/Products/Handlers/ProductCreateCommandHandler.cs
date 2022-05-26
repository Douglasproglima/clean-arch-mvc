using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.CQRS.Products.Handlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        #region Propertits/Attributes
        private readonly IProductRepository _productRepository;
        #endregion

        #region Constructor
        public ProductCreateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region Methods
        public async Task<Product> Handle(
            ProductCreateCommand request,
            CancellationToken cancellationToken)
        {
            var product = new Product(
                request.Name, 
                request.Description, 
                request.Price, 
                request.Stock, 
                request.Image
            );

            if (product == null)
                throw new ApplicationException($"Erro ao criar entidade!");

            product.CategoryId = request.CategoryId;
            return await _productRepository.CreateAsync(product);
        }
        #endregion
    }
}
