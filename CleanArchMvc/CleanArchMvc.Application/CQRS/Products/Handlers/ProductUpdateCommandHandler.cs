using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.CQRS.Products.Handlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
    {
        #region Propertits/Attributes
        private readonly IProductRepository _productRepository;
        #endregion

        #region Constructor
        public ProductUpdateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ??
                throw new ArgumentException(nameof(productRepository));
        }
        #endregion

        #region Methods
        public async Task<Product> Handle(
            ProductUpdateCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
                throw new ApplicationException($"Erro ao atualizar entidade!");

            product.Update(
                request.CategoryId,
                request.Name,
                request.Description,
                request.Price,
                request.Stock,
                request.Image
            );

            return await _productRepository.UpdateAsync(product);
        }
        #endregion
    }
}
