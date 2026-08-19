using HotelFlow.Domain.Enums;
using HotelFlow.Domain.Exceptions;

namespace HotelFlow.Domain.Entities
{
    public class Quarto
    {
        public Guid Id { get; private set; }
        public int Numero { get; private set; }
        public CategoriaQuarto Categoria { get; private set; }
        public int Capacidade { get; private set; }
        public decimal ValorDiaria { get; private set; }
        public StatusQuarto Status { get; private set; }

        private Quarto(int numero, CategoriaQuarto categoria, int capacidade, decimal valorDiaria, StatusQuarto status)
        {
            ValidarNumero(numero);
            ValidarCapacidade(capacidade);
            ValidarCategoria(categoria);
            ValidarValorDiaria(valorDiaria);

            Id = Guid.NewGuid();
            Numero = numero;
            Categoria = categoria;
            Capacidade = capacidade;
            ValorDiaria = valorDiaria;
            Status = status;
        }

        public static Quarto Cadastrar(int numero, CategoriaQuarto categoria, int capacidade, decimal valorDiaria)
        {
            return new Quarto(numero, categoria, capacidade, valorDiaria, StatusQuarto.Disponivel);
        }

        public void EntrarEmManutenção()
        {
            if (Status != StatusQuarto.Disponivel)
                throw new QuartoException($"Não é possível colocar o quarto em manutenção no status atual.");

            Status = StatusQuarto.EmManutencao;
        }

        public void FinalizarManutencao()
        {
            if (Status != StatusQuarto.EmManutencao)
                throw new QuartoException($"Não é possível finalizar manutenção de um quarto {Status.Descricao().ToLower()}.");

            Status = StatusQuarto.Disponivel;
        }

        public void Ocupar()
        {
            if (Status != StatusQuarto.Disponivel)
                throw new QuartoException($"Não é possível ocupar um quarto {Status.Descricao().ToLower()}");

            Status = StatusQuarto.Ocupado;
        }

        public void Desocupar()
        {
            if (Status != StatusQuarto.Ocupado)
                throw new QuartoException($"Não é possível desocupar um quarto {Status.Descricao().ToLower()}");

            Status = StatusQuarto.Disponivel;
        }

        private void ValidarNumero(int numero)
        {
            if (numero <= 0)
                throw new QuartoException("Número inválido");
        }

        private void ValidarCapacidade(int capacidade)
        {
            if (capacidade <= 0)
                throw new QuartoException("Capacidade inválida");
        }

        private void ValidarValorDiaria(decimal valorDiaria)
        {
            if (valorDiaria <= decimal.Zero)
                throw new QuartoException("Valor da diária inválido");
        }

        private void ValidarCategoria(CategoriaQuarto categoria)
        {
            if (!Enum.IsDefined(typeof(CategoriaQuarto), categoria))
                throw new QuartoException("Categoria inválida");
        }
    }
}
