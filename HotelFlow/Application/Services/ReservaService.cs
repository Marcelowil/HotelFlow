using HotelFlow.Domain.Entities;
using HotelFlow.Domain.Enums;
using HotelFlow.Domain.Exceptions;

namespace HotelFlow.Application.Services
{
    public class ReservaService
    {
        private List<Reserva> reservas = new List<Reserva>();

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

        public Reserva CadastrarReserva(Hospede hospede, Quarto quarto, DateTime dataEntrada, DateTime dataSaida)
        {
            Reserva reserva = Reserva.Cadastrar(hospede, quarto, dataEntrada, dataSaida);

            if (!VerificarDisponibilidade(quarto, dataEntrada, dataSaida))
                throw new ReservaException("O quarto não está disponível para o período informado.");

            reservas.Add(reserva);

            return reserva;
        }

        public IReadOnlyList<Reserva> ObterReservas()
        {
            return reservas;
        }
    }
}
