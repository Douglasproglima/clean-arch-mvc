using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain
{
    //Modificador sealed: Garante que essa class não poderá ser herdada
    public sealed class Category
    {
        #region Propriedades
        private readonly string msgFieldRequired = "O campo {0} é obrigatório.";
        private readonly string msgFieldMinCharacter = "O campo {0} aceita no minimo 3 caracteres.";

        //O modificador private no set das propriedades
        //Se faz necessário para garantir que os objetos da camada de domain
        //não tenha suas propriedades alteradas ou atribuidos externamente.

        public int Id { get; private set; }
        public string Name { get; private set; }

        public ICollection<Product> Products { get; set; }
        #endregion

        #region Construtores Especializados
        public Category(string name)
        {
            ValidateDomainName(name);
        }

        public Category(int id, string name)
        {
            ValidateDomainId(id);
            ValidateDomainName(name);
        }
        #endregion

        #region Métodos

        #region Validações
        private void ValidateDomainId(int id)
        {
            DomainExceptionValidation.When(id < 0, "Valor inválido para o campo Id.");
            Id = id;
        }

        private void ValidateDomainName(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), string.Format(msgFieldRequired, name));
            DomainExceptionValidation.When(name.Length < 3, string.Format(msgFieldMinCharacter, name));

            //Só atribui o valor caso atenda as regras acima.
            Name = name;
        }
        #endregion

        #region Demais Métodos
        public void Update(string name)
        { 
            ValidateDomainName(name);

        }
        #endregion

        #endregion
    }
}
