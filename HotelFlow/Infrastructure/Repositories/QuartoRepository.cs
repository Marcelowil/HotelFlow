using HotelFlow.Domain.Entities;
using HotelFlow.Domain.Exceptions;

namespace HotelFlow.Infrastructure.Repositories
{
    public class QuartoRepository : IRepository<Quarto>
    {
        private List<Quarto> quartos = new List<Quarto>();

        public void Salvar(Quarto quarto)
        {
            quartos.Add(quarto);
        }

        public Quarto BuscarPorId(Guid id)
        {
            return quartos.FirstOrDefault(quarto => quarto.Id == id) 
                ?? throw new QuartoException($"Quarto com ID {id} não encontrado");
        }

        public IReadOnlyList<Quarto> BuscarTodos()
        {
            return quartos;
        }

        public bool VerificarNumeroDuplicado(int numero)
        {
            var numerosQuarto = quartos.Select(quarto => quarto.Numero).ToList();

            return numerosQuarto.Contains(numero);
        }
    }
}
