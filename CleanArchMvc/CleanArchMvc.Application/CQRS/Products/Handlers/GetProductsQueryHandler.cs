using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Application.CQRS.Products.Queries;

namespace CleanArchMvc.Application.CQRS.Products.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        #region Propertits/Attributes
        private readonly IProductRepository _productRepository;
        #endregion

        #region Constructor
        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            //Operador Null-Coalescing: Se productRepository for null, retorna a exception.
            _productRepository = productRepository ??
                throw new ArgumentException(nameof(productRepository));
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<Product>> Handle(
            GetProductsQuery request,
            CancellationToken cancellationToken)
        {
            return await _productRepository.GetProductsAsync();
        }
        #endregion
    }
}
