using HotelFlow.Application.Services;
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
                var service = new ReservaService();

                var h = Hospede.Registrar("Marcelo", "4312658899", "marcelo@teste.com", "11-4002-8922");
                var q = Quarto.Cadastrar(57, CategoriaQuarto.Suite, 2, new decimal(311.90));
                var r = service.CadastrarReserva(h, q, new DateTime(2026, 08, 20), new DateTime(2026, 08, 25));
                Console.WriteLine(r.Id);
                Console.WriteLine(r.DataEntrada);
                Console.WriteLine(r.DataSaida);
                Console.WriteLine(r.ValorDiaria);
                Console.WriteLine(r.Status.Descricao());
                Console.WriteLine(r.Hospede.Nome);
                Console.WriteLine(r.Quarto.Numero);
                Console.WriteLine(r.Quarto.Categoria.Descricao());

                Console.WriteLine();
                var r2 = service.CadastrarReserva(h, q, new DateTime(2026, 08, 25), new DateTime(2026, 08, 28));
                Console.WriteLine(r2.Id);
                Console.WriteLine(r2.DataEntrada);
                Console.WriteLine(r2.DataSaida);
                Console.WriteLine(r2.ValorDiaria);
                Console.WriteLine(r2.Status.Descricao());
                Console.WriteLine(r2.Hospede.Nome);
                Console.WriteLine(r2.Quarto.Numero);
                Console.WriteLine(r2.Quarto.Categoria.Descricao());

            }
            catch (ReservaException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}