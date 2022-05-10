using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleanArchMvc.Domain.Resources;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    //Modificador sealed: Garante que essa class não poderá ser herdada
    public sealed class Product : EntityBase
    {
        #region Propriedades
        //O modificador private no set das propriedades
        //Se faz necessário para garantir que os objetos da camada de domain
        //não tenha suas propriedades alteradas ou atribuidos externamente.

        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        #endregion

        #region Construtores Especializados
        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            ValidateDomainId(id);
            ValidateDomain(name, description, price, stock, image);
        }
        #endregion

        #region Métodos

        #region Validações
        private void ValidateDomainId(int id)
        {
            DomainExceptionValidation.When(id < 0, string.Format(MessagesValidation.msgValueInvalid, "Id"));
            Id = id;
        }

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(
                string.IsNullOrEmpty(name),
                string.Format(MessagesValidation.msgFieldRequired, "Nome")
            );

            DomainExceptionValidation.When(
                name.Length < 3,
                string.Format(MessagesValidation.msgFieldMinCharacter, "Nome", 3)
            );

            DomainExceptionValidation.When(
                string.IsNullOrEmpty(description),
                string.Format(MessagesValidation.msgFieldRequired, "Descrição")
            );

            DomainExceptionValidation.When(
                description.Length < 5,
                string.Format(MessagesValidation.msgFieldMinCharacter, "Descrição", 5)
            );

            DomainExceptionValidation.When(
                price < 0,
                string.Format(MessagesValidation.msgValueInvalid, "Preço")
            );

            DomainExceptionValidation.When(stock < 0,
                string.Format(MessagesValidation.msgValueInvalid, "Estoque"));

            DomainExceptionValidation.When(image.Length > 250,
                string.Format(MessagesValidation.msgFieldMaxCharacter, "Imagem", 250));

            //Só atribui o valor caso atenda as regras acima.
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        }
        #endregion

        #region Demais Métodos
        public void Update(int categoryId, string name, string description, decimal price, int stock, string image)
        {
            CategoryId = categoryId;
            ValidateDomain(name, description, price, stock, image);
        }
        #endregion

        #endregion
    }
}
