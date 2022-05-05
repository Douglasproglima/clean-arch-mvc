using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Resources
{
    public static class MessagesValidation
    {
        public static string msgValueInvalid = "Valor inválido para o campo {0}.";
        public static string msgFieldRequired = "O campo {0} é obrigatório.";
        public static string msgFieldMinCharacter = "O campo {0} aceita no mínimo {1} caracteres.";
        public static string msgFieldMaxCharacter = "O campo {0} aceita no máximo {1} caracteres.";
    }
}
