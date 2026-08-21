using HotelFlow.Domain.Entities;
using HotelFlow.Domain.Enums;
using HotelFlow.Domain.Exceptions;

namespace HotelFlow.Infrastructure.Repositories
{
    public class ReservaRepository : IRepository<Reserva>
    {
        private List<Reserva> reservas = new List<Reserva>();

        public void Salvar(Reserva reserva)
        {
            reservas.Add(reserva);
        }

        public Reserva BuscarPorId(Guid id)
        {
            return reservas.FirstOrDefault(reserva => reserva.Id == id)
                ?? throw new ReservaException($"Reserva com ID {id} não encontrada.");
        }

        public IReadOnlyList<Reserva> BuscarTodos()
        {
            return reservas;
        }

        public IReadOnlyList<Reserva> BuscarReservasPorQuarto(Quarto quarto)
        {
            return reservas.Where(reserva => reserva.Quarto.Numero == quarto.Numero
                    && !new[] { StatusReserva.Cancelada, StatusReserva.Concluida }.Contains(reserva.Status))
                    .ToList();
        }

        public IReadOnlyList<Reserva> BuscarReservasPorStatus(StatusReserva status)
        {
            return reservas.Where(reserva => reserva.Status == status).ToList();
        }

        public IReadOnlyList<Reserva> BuscarReservasPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return reservas.Where(reserva => dataFim >= reserva.DataEntrada && dataInicio <= reserva.DataSaida).ToList();
        }

        public IReadOnlyList<Reserva> BuscarReservasPorHospede(Guid hospedeId)
        {
            return reservas.Where(reserva => reserva.Hospede.Id == hospedeId).ToList();
        }

        public IReadOnlyList<Reserva> BuscarReservasPorNumeroQuarto(int numeroQuarto)
        {
            return reservas.Where(reserva => reserva.Quarto.Numero == numeroQuarto).ToList();
        }
    }
}
