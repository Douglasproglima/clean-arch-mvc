using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Application.CQRS.Products.Queries;

namespace CleanArchMvc.Application.CQRS.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        #region Propertits/Attributes
        private readonly IProductRepository _productRepository;
        #endregion

        #region Constructor
        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ??
                throw new ArgumentException(nameof(productRepository));
        }
        #endregion

        #region Methods
        public async Task<Product> Handle(
            GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _productRepository.GetByIdAsync(request.Id);
        }
        #endregion
    }
}
