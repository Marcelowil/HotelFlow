using HotelFlow.Domain.Entities;
using HotelFlow.Domain.Enums;
using HotelFlow.Domain.Exceptions;
using HotelFlow.Infrastructure.Repositories;

namespace HotelFlow.Application.Services
{
    public class ReservaService
    {
        private ReservaRepository repository;

        public ReservaService()
        {
            repository = new ReservaRepository();
        }

        public bool VerificarDisponibilidade(Quarto quarto, DateTime dataEntrada, DateTime dataSaida)
        {
            IReadOnlyList<Reserva> reservasPorQuarto = repository.BuscarReservasPorQuarto(quarto);

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

            repository.Salvar(reserva);

            return reserva;
        }

        public IReadOnlyList<Reserva> ObterReservas()
        {
            return repository.BuscarTodos();
        }

        public IReadOnlyList<Reserva> ObterReservasPorStatus(StatusReserva status)
        {
            return repository.BuscarReservasPorStatus(status);
        }

        public IReadOnlyList<Reserva> ObterReservasPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            if (dataInicio >= dataFim)
                throw new ReservaException("A data de início tem que menor que da data final");

            return repository.BuscarReservasPorPeriodo(dataInicio, dataFim);
        }

        public IReadOnlyList<Reserva> ObterReservasPorHospede(Guid hospedeId)
        {
            return repository.BuscarReservasPorHospede(hospedeId);
        }

        public IReadOnlyList<Reserva> ObterReservasPorNumeroQuarto(int numeroQuarto)
        {
            return repository.BuscarReservasPorNumeroQuarto(numeroQuarto);
        }

        public Reserva RealizarPagamento(Guid id, decimal valor, DateTime dataPagamento, FormaPagamento formaPagamento)
        {
            Reserva reserva = BuscarReservaPorId(id);

            Pagamento pagamento = Pagamento.Registrar(valor, dataPagamento, formaPagamento);

            reserva.RegistrarPagamento(pagamento);

            return reserva;
        }

        public IReadOnlyList<Pagamento> ConsultarPagamentos(Guid id)
        {
            Reserva reserva = BuscarReservaPorId(id);

            return reserva.ObterPagamentos();
        }

        public Reserva ConfirmarReserva(Guid id)
        {
            Reserva reserva = BuscarReservaPorId(id);

            reserva.Confirmar();

            return reserva;
        }

        public Reserva CancelarReserva(Guid id)
        {
            Reserva reserva = BuscarReservaPorId(id);

            reserva.Cancelar();

            return reserva;
        }

        public Reserva ConcluirReserva(Guid id)
        {
            Reserva reserva = BuscarReservaPorId(id);

            reserva.Concluir();

            return reserva;
        }

        public Reserva RealizarCheckIn(Guid id)
        {
            Reserva reserva = BuscarReservaPorId(id);

            if (reserva.Status != StatusReserva.Confirmada)
                throw new ReservaException($"Não é possível fazer check-in em uma reserva com status {reserva.Status.Descricao().ToLower()}.");

            reserva.Quarto.Ocupar();

            return reserva;
        }

        public Reserva RealizarCheckOut(Guid id)
        {
            Reserva reserva = BuscarReservaPorId(id);

            if (reserva.Status != StatusReserva.Confirmada)
                throw new ReservaException($"Não é possível fazer check-out em uma reserva com status {reserva.Status.Descricao().ToLower()}.");

            reserva.Quarto.Desocupar();
            reserva.Concluir();

            return reserva;
        }

        private Reserva BuscarReservaPorId(Guid id)
        {
            return repository.BuscarPorId(id);
        }
    }
}
