using HotelFlow.Domain.Entities;
using HotelFlow.Domain.Enums;
using HotelFlow.Domain.Exceptions;

namespace HotelFlow
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var h = Hospede.Registrar("Marcelo William", "431265585588", "marcelo@teste.com", "(11)4002-8922");
                var q = Quarto.Cadastrar(55, CategoriaQuarto.Luxo, 1, new decimal(274.90));
                var r = Reserva.Cadastrar(h, q, new DateTime(2026, 08, 19), new DateTime(2026, 08, 20));

                Console.WriteLine(r.Id);
                Console.WriteLine(r.Hospede.Nome);
                Console.WriteLine(r.Quarto.Numero);
                Console.WriteLine(r.DataEntrada);
                Console.WriteLine(r.DataSaida);
                Console.WriteLine(r.ValorDiaria);
                Console.WriteLine(r.Status.Descricao());
                Console.WriteLine(r.QuantidadeDiarias());
                Console.WriteLine(r.ValorTotal());
            }catch(ReservaException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}