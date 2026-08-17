using HotelFlow.Domain.Entities;
using HotelFlow.Domain.Enums;

namespace HotelFlow.Application.Services
{
    public class ReservaService
    {
        public List<Reserva> reservas = new List<Reserva>();

        public bool VerificarDisponibilidade(Quarto quarto, DateTime dataEntrada, DateTime dataSaida)
        {
            List<Reserva> reservasPorQuarto = reservas
                .Where(reserva => reserva.Quarto.Numero == quarto.Numero && !new[] { StatusReserva.Cancelada, StatusReserva.Concluida }.Contains(reserva.Status))
                .ToList();

                foreach (Reserva reserva in reservasPorQuarto)
                {
                    bool periodoDisponivel = dataEntrada.Date >= reserva.DataSaida.Date || dataSaida.Date <= reserva.DataEntrada.Date;

                    if (!periodoDisponivel)
                        return false;
                }

            return true;
        }
    }
}
