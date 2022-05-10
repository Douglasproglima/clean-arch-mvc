using System.Collections.Generic;
using CleanArchMvc.Domain.Validation;
using CleanArchMvc.Domain.Resources;

namespace CleanArchMvc.Domain.Entities
{
    //Modificador sealed: Garante que essa class não poderá ser herdada
    public sealed class Category : EntityBase
    {
        #region Propriedades
        //O modificador private no set das propriedades
        //Se faz necessário para garantir que os objetos da camada de domain
        //não tenha suas propriedades alteradas ou atribuidos externamente.
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
            DomainExceptionValidation.When(
                id < 0, 
                string.Format(MessagesValidation.msgValueInvalid, "Id")
            );
            
            Id = id;
        }

        private void ValidateDomainName(string name)
        {
            DomainExceptionValidation.When(
                string.IsNullOrEmpty(name), 
                string.Format(MessagesValidation.msgFieldRequired, "Nome")
            );
            
            DomainExceptionValidation.When(
                name.Length < 3, 
                string.Format(MessagesValidation.msgFieldMinCharacter, "Nome", 3)
            );

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
