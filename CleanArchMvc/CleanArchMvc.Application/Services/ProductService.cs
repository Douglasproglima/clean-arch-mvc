using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        #region Propriedades/Atributos
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Construtor
        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;

            //Operador Null-Coalescing: Se productRepository for null, retorna a exception.
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));
        }
        #endregion

        #region métodos Sobrescritos
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> GetProductCategory(int? id)
        {
            var product = await _productRepository.GetProductsCategoryAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task Add(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.CreateAsync(product);
        }

        public async Task Update(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateAsync(product);
        }

        public async Task Remove(int? id)
        {
            var product = _productRepository.GetByIdAsync(id).Result;
            await _productRepository.RemoveAsync(product);
        }
        #endregion
    }
}
