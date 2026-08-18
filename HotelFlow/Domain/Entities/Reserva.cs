using HotelFlow.Domain.Enums;
using HotelFlow.Domain.Exceptions;

namespace HotelFlow.Domain.Entities
{
    public class Reserva
    {
        public Guid Id { get; private set; }
        public Hospede Hospede { get; private set; }
        public Quarto Quarto { get; private set; }
        public DateTime DataEntrada { get; private set; }
        public DateTime DataSaida { get; private set; }
        public decimal ValorDiaria { get; private set; }
        public StatusReserva Status { get; private set; }

        private Reserva(Hospede hospede, Quarto quarto, DateTime dataEntrada, DateTime dataSaida, StatusReserva status)
        {
            ValidarHospede(hospede);
            ValidarQuarto(quarto);
            ValidarPeriodoReserva(dataEntrada, dataSaida);

            Id = Guid.NewGuid();
            Hospede = hospede;
            Quarto = quarto;
            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
            ValorDiaria = quarto.ValorDiaria;
            Status = status;
        }

        public static Reserva Cadastrar(Hospede hospede, Quarto quarto, DateTime dataEntrada, DateTime dataSaida)
        {
            return new Reserva(hospede, quarto, dataEntrada, dataSaida, StatusReserva.Pendente);
        }

        public int QuantidadeDiarias()
        {
            TimeSpan diarias = DataSaida.Date - DataEntrada.Date;

            return diarias.Days;
        }

        public decimal ValorTotal()
        {
            return decimal.Multiply(QuantidadeDiarias(), ValorDiaria);
        }

        public void Confirmar()
        {
            if (Status != StatusReserva.Pendente)
                throw new ReservaException($"Não é possível confirmar uma reserva {Status.Descricao().ToLower()}.");

            Status = StatusReserva.Confirmada;
        }

        public void Cancelar()
        {
            if (Status == StatusReserva.Cancelada || Status == StatusReserva.Concluida)
                throw new ReservaException($"A reserva já está {Status.Descricao().ToLower()}.");
            
            Status = StatusReserva.Cancelada;
        }

        public void Concluir()
        {
            if(Status != StatusReserva.Confirmada)
                throw new ReservaException($"Não é possível concluir uma reserva {Status.Descricao().ToLower()}.");

            Status = StatusReserva.Concluida;
        }

        private void ValidarHospede(Hospede hospede)
        {
            if (hospede == null)
                throw new ReservaException("Hóspede inválido");
        }

        private void ValidarQuarto(Quarto quarto)
        {
            if (quarto == null)
                throw new ReservaException("Quarto inválido");
        }

        private void ValidarPeriodoReserva(DateTime dataEntrada, DateTime dataSaida)
        {
            DateTime hoje = DateTime.Now.Date;
            ValidarDataEntrada(dataEntrada, dataSaida, hoje);
            ValidarDataSaida(dataSaida, dataEntrada, hoje);
        }

        private void ValidarDataEntrada(DateTime dataEntrada, DateTime dataSaida, DateTime hoje)
        {
            bool dataEntradaPosteriorSaida = DateTime.Compare(dataEntrada.Date, dataSaida.Date) >= 0;
            bool dataEntradaAnteriorHoje = DateTime.Compare(dataEntrada.Date, hoje) < 0;

            if (dataEntradaPosteriorSaida)
                throw new ReservaException("O período da reserva é inválido");

            if (dataEntradaAnteriorHoje)
                throw new ReservaException("Data de entrada no passado");
        }

        private void ValidarDataSaida(DateTime dataSaida, DateTime dataEntrada, DateTime hoje)
        {
            bool dataSaidaNaoPosteriorEntrada = DateTime.Compare(dataSaida.Date, dataEntrada.Date) <= 0;
            bool dataSaidaIgualHoje = DateTime.Compare(dataSaida.Date, hoje) == 0;

            if (dataSaidaNaoPosteriorEntrada)
                throw new ReservaException("A data de saída deve ser posterior à data de entrada.");

            if (dataSaidaIgualHoje)
                throw new ReservaException("Data de saída inválida");
        }
    }
}
