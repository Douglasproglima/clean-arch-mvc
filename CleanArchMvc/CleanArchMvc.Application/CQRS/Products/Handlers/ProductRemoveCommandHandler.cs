using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.CQRS.Products.Handlers
{
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
    {
        #region Propertits/Attributes
        private readonly IProductRepository _productRepository;
        #endregion

        #region Constructor
        public ProductRemoveCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ??
                throw new ArgumentException(nameof(productRepository));
        }
        #endregion

        #region Methods
        public async Task<Product> Handle(
            ProductRemoveCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
                throw new ApplicationException($"Erro ao deletar entidade!");

            var result = await _productRepository.RemoveAsync(product);
            return result;
        }
        #endregion
    }
}
