using HotelFlow.Domain.Entities;
using HotelFlow.Domain.Enums;
using HotelFlow.Domain.Exceptions;
using HotelFlow.Infrastructure.Repositories;
namespace HotelFlow.Application.Services
{
    public class QuartoService
    {
        private QuartoRepository repository;

        public QuartoService()
        {
            repository = new QuartoRepository();
        }

        public Quarto CadastrarQuarto(int numero, CategoriaQuarto categoria, int capacidade, decimal valorDiaria)
        {
            ChecarNumeroCadastrado(numero);

            Quarto quarto = Quarto.Cadastrar(numero, categoria, capacidade, valorDiaria);

            repository.Salvar(quarto);

            return quarto;
        }

        public IReadOnlyList<Quarto> ObterQuartos()
        {
            return repository.BuscarTodos();
        }

        private void ChecarNumeroCadastrado(int numero)
        {
            if (repository.VerificarNumeroDuplicado(numero))
                throw new QuartoException($"Já existe um quarto com número {numero}.");
        }
    }
}
