using HotelFlow.Domain.Enums;
using HotelFlow.Domain.Exceptions;

namespace HotelFlow.Domain.Entities
{
    public class Pagamento
    {
        public Guid Id { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataPagamento { get; private set; }
        public FormaPagamento MetodoPagamento { get; private set; }

        private Pagamento(decimal valor, DateTime dataPagamento, FormaPagamento formaPagamento)
        {
            ValidarValor(valor);
            ValidarFormaPagamento(formaPagamento);

            Id = Guid.NewGuid();
            Valor = valor;
            DataPagamento = dataPagamento;
            MetodoPagamento = formaPagamento;
        }

        public static Pagamento Registrar(decimal valor, DateTime dataPagamento, FormaPagamento formaPagamento)
        {
            return new Pagamento(valor, dataPagamento, formaPagamento);
        }

        private void ValidarValor(decimal valor)
        {
            if (valor <= decimal.Zero)
                throw new PagamentoException("Valor de pagamento inválido.");
        }

        private void ValidarFormaPagamento(FormaPagamento formaPagamento)
        {
            if (!Enum.IsDefined(typeof(FormaPagamento), formaPagamento))
                throw new PagamentoException("Forma de pagamento inválida.");
        }
    }
}
