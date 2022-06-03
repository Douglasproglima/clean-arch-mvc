using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Application.CQRS.Products.Queries;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        #region Propriedades/Atributos
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        #endregion

        #region Construtor
        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        #endregion

        #region métodos Sobrescritos
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsQuery = new GetProductsQuery();

            if (productsQuery is null)
                throw new Exception($"Não foi possível carregar a entidade!");

            var result = await _mediator.Send(productsQuery);

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productyQueryById = new GetProductByIdQuery(id.Value);

            if(productyQueryById == null)
                throw new Exception($"Não foi possível carregar a entidade de código {id.Value}!");

            var result = await _mediator.Send(productyQueryById);

            return _mapper.Map<ProductDTO>(result);
        }

        /*
        public async Task<ProductDTO> GetProductCategory(int? id)
        {
            return await this.GetById(id);
        }
        */

        public async Task Add(ProductDTO productDto)
        {
            //Necessário converter o obj ProductDTO para ProductCreateCommand
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDto);
            await _mediator.Send(productCreateCommand);
        }

        public async Task Update(ProductDTO productDto)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDto);
            await _mediator.Send(productUpdateCommand);
        }

        public async Task Remove(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);

            if (productRemoveCommand == null)
                throw new Exception($"Não foi possível remover a entidade de código {id.Value}!");

            await _mediator.Send(productRemoveCommand);
        }
        #endregion
    }
}
