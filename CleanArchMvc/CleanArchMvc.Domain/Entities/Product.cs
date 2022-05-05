using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities
{
    //Modificador sealed: Garante que essa class não poderá ser herdada
    public sealed class Product
    {
        #region Propriedades
        //O modificador private no set das propriedades
        //Se faz necessário para garantir que os objetos da camada de domain
        //não tenha suas propriedades alteradas ou atribuidos externamente.

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }
        #endregion

        #region Construtores Especializados

        #endregion

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
