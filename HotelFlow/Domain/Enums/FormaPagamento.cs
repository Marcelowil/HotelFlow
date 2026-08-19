using System.Runtime.CompilerServices;

namespace HotelFlow.Domain.Enums
{
    public enum FormaPagamento
    {
        Dinheiro,
        Pix,
        CartaoCredito,
        CartaoDebito
    }

    public static class FormaPagamentoExtensions
    {
        public static String Descricao(this FormaPagamento formaPagamento)
        {
            return formaPagamento switch
            {
                FormaPagamento.Dinheiro => "Dinheiro",
                FormaPagamento.Pix => "Pix",
                FormaPagamento.CartaoCredito => "Cartão de Crédito",
                FormaPagamento.CartaoDebito => "Cartão de Débito"
            };
        }
    }
}
