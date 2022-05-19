using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Products.Commands
{
    //Clase base para os comandos, por isso é uma classe abstrata
    public abstract class ProductCommand : IRequest<Product>
	{
		//Em um App mais complexo, poderia ser definido propriedades de multiplas entidades
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }
		public string Image { get; set; }
		public int CategoryId { get; set; }
	}
}
