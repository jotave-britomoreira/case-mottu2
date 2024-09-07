using System;

namespace NovoCaseMottu
{
    public class ValidacaoConserto
    {
        public static int ValidarId(string input)
        {
            if (int.TryParse(input, out int id))
            {
                return id;
            }
            else
            {
                throw new ArgumentException("ID inválido. Digite novamente.");
            }
        }

        public static int ValidarComplexidade(string input)
        {
            if (int.TryParse(input, out int complexidade) && complexidade >= 1 && complexidade <= 3)
            {
                return complexidade;
            }
            else
            {
                throw new ArgumentException("Complexidade inválida. Digite um número entre 1 e 3.");
            }
        }

        public static int ValidarTipoConserto(string input)
        {
            if (int.TryParse(input, out int tipoConserto) && tipoConserto >= 1 && tipoConserto <= 5)
            {
                return tipoConserto;
            }
            else
            {
                throw new ArgumentException("Tipo de conserto inválido. Digite um número entre 1 e 5.");
            }
        }
    }
}
