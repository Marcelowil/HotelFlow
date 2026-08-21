using HotelFlow.Domain.Entities;
using HotelFlow.Domain.Enums;
using HotelFlow.Domain.Exceptions;
namespace HotelFlow.Application.Services
{
    public class QuartoService
    {
        private List<Quarto> quartos = new List<Quarto>();
        private List<int> numerosQuarto = new List<int>();

        public Quarto CadastrarQuarto(int numero, CategoriaQuarto categoria, int capacidade, decimal valorDiaria)
        {
            ChecarNumeroCadastrado(numero);

            Quarto quarto = Quarto.Cadastrar(numero, categoria, capacidade, valorDiaria);

            quartos.Add(quarto);
            numerosQuarto.Add(numero);

            return quarto;
        }

        public IReadOnlyList<Quarto> ObterQuartos()
        {
            return quartos;
        }

        private void ChecarNumeroCadastrado(int numero)
        {
            if (numerosQuarto.Contains(numero))
                throw new QuartoException($"Já existe um quarto com número {numero}.");
        }
    }
}
